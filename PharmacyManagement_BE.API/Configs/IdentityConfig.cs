using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.DBContext;
using System.Text;

namespace PharmacyManagement_BE.API.Configs
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfig(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddIdentity<Customer, IdentityRole>().AddEntityFrameworkStores<PharmacyManagementContext>().AddDefaultTokenProviders();
            //services.AddIdentity<Staff, IdentityRole>().AddEntityFrameworkStores<PharmacyManagementContext>().AddDefaultTokenProviders();
            services.AddIdentity<ApplicationUser, IdentityRole<Guid>>().AddEntityFrameworkStores<PharmacyManagementContext>().AddDefaultTokenProviders();

            // Setup Authentication JWT 
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:ValidIssuer"],
                    ValidAudience = configuration["Jwt:ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]))
                };
            });

            return services;
        }
    }
}
