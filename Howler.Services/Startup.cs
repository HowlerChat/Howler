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
    using System.Net.Http;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using Howler.Database;
    using Howler.Database.Config;
    using Howler.Database.Core;
    using Howler.Database.Indexer;
    using Howler.Services.Attachments;
    using Howler.Services.Authorization;
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
    using Newtonsoft.Json;

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
                        builder.WithOrigins(
                            "http://localhost:8000",
                            "https://app.howler.chat")
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials());
                });
            this.ConfigureAuthorization(services);
            this.ConfigureDatabase(services);
            this.ConfigureAttachmentStore(services);
            services.AddScoped<
                ISpaceInteractionService,
                SpaceInteractionService>();
            services.AddScoped<
                IFederationInteractionService,
                FederationInteractionService>();
            services.AddHttpContextAccessor();
            services.AddControllers();
            this.ConfigureSwagger(services);
            services.AddSignalR(o =>
            {
                o.EnableDetailedErrors = true;
            });
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
            app.UseSwaggerUI(c =>
            {
                c.OAuth2RedirectUrl("http://localhost:5000/" +
                    "swagger/oauth2-redirect.html");
                c.OAuthClientId("6b75ooll3b86ugauhu22vj39ra");
                c.SwaggerEndpoint(
                "/swagger/v1/swagger.json",
                "Howler.Services v1");
            });
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

        private void ConfigureAuthorization(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(x =>
                {
                    var key = JsonConvert.DeserializeObject<
                        Dictionary<string, Dictionary<string, string>[]>>(
                            new HttpClient().GetAsync(
                                this.Configuration["JWT:Authority"] +
                                "/.well-known/jwks.json").Result.Content
                                .ReadAsStringAsync().Result)["keys"][0];
                    x.Authority = this.Configuration["JWT:Authority"];
                    x.Audience = this.Configuration["JWT:Audience"];
                    x.RequireHttpsMetadata = false;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        IssuerSigningKey = new RsaSecurityKey(
                            new System.Security.Cryptography.RSAParameters
                            {
                                Exponent = Convert.FromBase64String(key["e"]),
                                Modulus = Convert.FromBase64String(key["n"]
                                    .Replace("-", "+")
                                    .Replace("_", "/")
                                    .PadRight(
                                        key["n"].Length +
                                        (key["n"].Length % 4), '=')),
                            }),
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
        }

        private void ConfigureDatabase(IServiceCollection services)
        {
            services.AddSingleton<IDatabaseClient, CassandraClient>();
            services.AddScoped<
                ICoreDatabaseContext,
                CoreDatabaseContext>();
            services.AddScoped<
                IIndexerDatabaseContext,
                IndexerDatabaseContext>();
            services.AddScoped<
                IConfigDatabaseContext,
                ConfigDatabaseContext>();
            services.AddScoped<
                IAuthorizationService,
                FederatedAuthorizationService>();
        }

        private void ConfigureAttachmentStore(IServiceCollection services)
        {
            var environment = Environment
                .GetEnvironmentVariable("HOWLER_ENVIRONMENT");

            if (environment == "dev")
            {
                services.AddScoped<
                    IAttachmentService,
                    FileAttachmentService>();
            }
            else
            {
                services.AddScoped<
                    IAttachmentService,
                    S3AttachmentService>();
            }
        }

        private void ConfigureSwagger(IServiceCollection services)
        {
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
                        Type = SecuritySchemeType.ApiKey,
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Description = "Bearer [access_token]",
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
                var xmlFile = Assembly.GetExecutingAssembly().GetName()
                    .Name + ".xml";
                var xmlPath = Path.Combine(
                    AppContext.BaseDirectory,
                    xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }
    }
}
