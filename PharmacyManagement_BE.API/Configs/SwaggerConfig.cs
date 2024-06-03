using Microsoft.OpenApi.Models;

namespace PharmacyManagement_BE.API.Configs
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwaggerSetup(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                //options.SwaggerDoc("v1", new OpenApiInfo { Title = "PharmacyManagement API", Version = "v1" });
                options.SwaggerDoc("Customer", new OpenApiInfo { Title = "Customer API", Version = "v1" });
                options.SwaggerDoc("Admin", new OpenApiInfo { Title = "Admin API", Version = "v1" });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            return services;
        }
    }
}
