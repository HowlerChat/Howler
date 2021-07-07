// <copyright file="Startup.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.AuthGateway
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using Amazon.KeyManagementService;
    using Howler.AuthGateway.CryptographyProviders;
    using Howler.AuthGateway.SigningAlgorithms;
    using Howler.Database;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;

    /// <summary>
    /// The configuration bootstrap class.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/>
        /// class.
        /// </summary>
        /// <param name="configuration">The application configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Gets the application configuration.
        /// </summary>
        public static IConfiguration? Configuration { get; private set; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to
        /// add services to the container.
        /// </summary>
        /// <param name="services">
        /// The dependency collection container to configure.
        /// </param>
        public void ConfigureServices(IServiceCollection services)
        {
            var environment = Environment
                .GetEnvironmentVariable("HOWLER_ENVIRONMENT");

            services.AddCors(options =>
                {
                    options.AddPolicy("defaultPolicy", builder =>
                    {
                    builder.WithOrigins(
                        "http://localhost:8000",
                        "https://localhost:8001")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                    });
                });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(x =>
                {
                    x.Authority = Configuration?["JWT:Authority"];
                    x.Audience = Configuration?["JWT:Audience"];
                    x.RequireHttpsMetadata = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateIssuer = true,
                        ValidateIssuerSigningKey = true,
                    };
                });
            services.AddSingleton<IDatabaseClient, CassandraClient>();

            if (environment == "prod")
            {
                services.AddScoped<IAmazonKeyManagementService,
                    AmazonKeyManagementServiceClient>();
                services.AddScoped<IRSAProvider, KMSRSAProvider>();
            }
            else
            {
                services.AddScoped<IRSAProvider, ConfigurationRSAProvider>();
            }

            services.AddScoped<ISigningAlgorithm, RS256SigningAlgorithm>();
            services.AddScoped<
                IFederationDatabaseContext,
                FederationDatabaseContext>();
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
                        "oauth2",
                        new OpenApiSecurityScheme
                        {
                            Type = SecuritySchemeType.OAuth2,
                            Flows = new OpenApiOAuthFlows
                            {
                                Implicit = new OpenApiOAuthFlow()
                                {
                                    TokenUrl =
                                        new Uri("https://auth.howler.chat/" +
                                            "oauth2/token"),
                                    AuthorizationUrl =
                                        new Uri("https://auth.howler.chat/" +
                                            "oauth2/authorize"),
                                    Scopes = new Dictionary<string, string>
                                    {
                                        { "openid", "User Profile" },
                                        { "email", "email" },
                                        { "profile", "profile" },
                                    },
                                },
                            },
                        });
                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "oauth2",
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

        /// <summary>
        /// This method gets called by the runtime. Use this method to
        /// configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="env">The host environment.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHttpsRedirection();
            }

            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.OAuth2RedirectUrl("https://gateway.howler.chat/" +
                    "swagger/oauth2-redirect.html");
                c.OAuthClientId("6b75ooll3b86ugauhu22vj39ra");
                c.SwaggerEndpoint(
                "/swagger/v1/swagger.json",
                "Howler.AuthGateway v1");
            });
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
