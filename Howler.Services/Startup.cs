// <copyright file="Startup.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Threading.Tasks;
    using Howler.Database;
    using Howler.Services.Hubs;
    using Howler.Services.InteractionServices;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;

    /// <summary>
    /// A startup configuration class for the ASP.NET host builder.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The supplied configuration.</param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// Gets the configuration data.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add
        /// services to the container.
        /// </summary>
        /// <param name="services">The service collection DI container.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
                {
                    options.AddPolicy("defaultPolicy", builder =>
                    {
                    builder.WithOrigins(
                        "http://localhost:8000",
                        "https://localhost:8001",
                        "https://192.168.0.131:8001")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                    });
                });

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme =
                        JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme =
                        JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.Authority = this.Configuration["JWT:Authority"];
                    x.Audience = this.Configuration["JWT:Audience"];
                    x.RequireHttpsMetadata = false;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateIssuer = true,
                        ValidateIssuerSigningKey = true,
                    };
                    x.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request
                                .Query["access_token"];

                            if (!string.IsNullOrEmpty(accessToken) &&
                                context.HttpContext.Request.Path
                                    .StartsWithSegments("/howler"))
                            {
                                context.Token = accessToken;
                            }

                            return Task.CompletedTask;
                        },
                    };
                });
            services.AddSingleton<IDatabaseClient, CassandraClient>();
            services.AddScoped<
                ISpaceInteractionService,
                SpaceInteractionService>();
            services.AddScoped<
                IFederationDatabaseContext,
                FederationDatabaseContext>();
            services.AddScoped<
                IAuthorizationService,
                FederatedAuthorizationService>();
            services.AddHttpContextAccessor();
            services.AddControllers();
            services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc(
                        "v1",
                        new OpenApiInfo
                        {
                            Title = "Howler.Services",
                            Version = "v1",
                        });
                    c.AddSecurityDefinition(
                        "Bearer",
                        new OpenApiSecurityScheme
                        {
                            Description = "Bearer [access_token]",
                            Name = "Authorization",
                            In = ParameterLocation.Header,
                            Type = SecuritySchemeType.ApiKey,
                            Scheme = "Bearer",
                        });
                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer",
                                },
                                Scheme = "oauth2",
                                Name = "Bearer",
                                In = ParameterLocation.Header,
                            },
                            new List<string>()
                        },
                    });
                    var xmlFile = Assembly
                        .GetExecutingAssembly()
                        .GetName()
                        .Name +
                        ".xml";
                    var xmlPath = Path.Combine(
                        AppContext.BaseDirectory,
                        xmlFile);
                    c.IncludeXmlComments(xmlPath);
                });
            services.AddSignalR();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to
        /// configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application builder to configure.</param>
        /// <param name="env">The host environment.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint(
                "/swagger/v1/swagger.json",
                "Howler.Services v1"));
            app.UseAuthentication();
            app.UseRouting();
            app.UseCors("defaultPolicy");
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<HowlerHub>("/howler");
            });
        }
    }
}
