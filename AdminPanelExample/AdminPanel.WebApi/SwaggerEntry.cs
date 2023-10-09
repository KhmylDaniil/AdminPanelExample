using Microsoft.OpenApi.Models;

namespace AdminPanel.WebApi
{
    /// <summary>
    /// Extension methods for swagger support
    /// </summary>
    public static class SwaggerEntry
    {
        /// <summary>
        /// Configure custom swagger
        /// </summary>
        /// <param name="services">Services</param>
        /// <returns>Services</returns>
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Api",
                    Version = "v1",
                    Description = "AdminPanel"
                });

                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{typeof(Program).Assembly.GetName().Name}.xml"), true);

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
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
            });

            return services;
        }

        /// <summary>
        /// Use custom swagger
        /// </summary>
        /// <param name="app">application builder</param>
        /// <returns>application builder</returns>
        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            return app;
        }
    }
}
