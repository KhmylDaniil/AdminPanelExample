using AdminPanel.DAL;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

namespace AdminPanel.WebApi
{
    /// <summary>
    /// Startup class as collection of extension methods
    /// </summary>
    public static class Startup
    {
        /// <summary>
        /// IServiceCollection extension
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection ConfugureServices(this IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Api",
                    Version = "v1",
                    Description = "Backend"
                });

                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{typeof(Program).Assembly.GetName().Name}.xml"), true);
            });

            return services;
        }

        /// <summary>
        /// IApplicationBuilder extension
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public static void Configure(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();
            app.UseRouting();
            app.UseHttpsRedirection();

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
            }

            app.UseHttpsRedirection();


            app.UseEndpoints(endpoints => endpoints.MapControllers());

            EntryDbProject.MigrateDB(app.ApplicationServices);
        }
    }
}
