using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AdminPanel.WebApi
{
    /// <summary>
    /// Provides methods to configure custom authentication for the application
    /// </summary>
    public static class AuthenticationEntry
    {
        /// <summary>
        /// Adds custom authentication to the specified <paramref name="services"/>
        /// </summary>
        /// <param name="services">The collection of services to configure</param>
        /// <param name="configuration">The configuration containing authentication settings</param>
        /// <returns>Updated service collection</returns>
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(opt =>
                {
                    opt.RequireHttpsMetadata = false;
                    opt.SaveToken = true;
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = configuration["Token:Issuer"],
                        ValidAudience = configuration["Token:Audience"],
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:JwtSecret"])),
                        ClockSkew = TimeSpan.Zero,
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true
                    };
                });
            return services;
        }
    }
}
