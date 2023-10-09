using AdminPanel.Core.Interfaces;
using AdminPanel.Core.Services;
using AdminPanel.DAL;
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
        public static IServiceCollection ConfugureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers(opt => opt.Filters.Add(typeof(ExceptionFilter)))
                .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            services.AddEndpointsApiExplorer();

            services.AddCustomSwagger();
            services.AddCustomAuthentication(configuration);

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAppDbContext, AppDbContext>();
            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<IPasswordService, PasswordService>();
            services.AddTransient<IJwtService, JwtService>();

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

            app.UseMiddleware<LoggingMiddleware>();

            app.UseRouting();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            if (env.IsDevelopment())
                app.UseCustomSwagger();

            
            app.UseEndpoints(endpoints => endpoints.MapControllers());

            EntryDbProject.MigrateDB(app.ApplicationServices);
        }
    }
}
