using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Domain.Roles;
using PharmacyManagement_BE.Infrastructure.Customs.Authorization;
using PharmacyManagement_BE.Infrastructure.Customs.Authorization.Handlers;
using PharmacyManagement_BE.Infrastructure.Customs.Authorization.Requirements;
using PharmacyManagement_BE.Infrastructure.DBContext;
using System.Text;

namespace PharmacyManagement_BE.API.Configs
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
            {
                // Thiết lập về Password
                options.Password.RequireDigit = true; // Không bắt phải có số
                options.Password.RequireLowercase = true; // Không bắt phải có chữ thường
                options.Password.RequireNonAlphanumeric = true; // Không bắt ký tự đặc biệt
                options.Password.RequireUppercase = true; // Không bắt buộc chữ in
                options.Password.RequiredLength = 3; // Số ký tự tối thiểu của password
                options.Password.RequiredUniqueChars = 1; // Số ký tự riêng biệt

                // Cấu hình Lockout - khóa user
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Khóa 5 phút
                options.Lockout.MaxFailedAccessAttempts = 5; // Thất bại 5 lần thì khóa
                options.Lockout.AllowedForNewUsers = true;

                // Cấu hình về User.
                options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;  // Email là duy nhất

                // Cấu hình đăng nhập.
                options.SignIn.RequireConfirmedEmail = true;            // Cấu hình xác thực địa chỉ email (email phải tồn tại)
                options.SignIn.RequireConfirmedPhoneNumber = false;     // Xác thực số điện thoại
            }).AddEntityFrameworkStores<PharmacyManagementContext>().AddDefaultTokenProviders();

            // Cấu hình Authentication JWT 
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
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
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            // Cấu hình Policy
            services.AddAuthorization(options =>
            {
                // Quản trị viên hệ thống
                options.AddPolicy("Admin", policy =>
                {
                    policy.AddRequirements(new IsAdminRequirement(AccountRole.PM_ADMIN));
                });

                // Quản lý nhân viên
                options.AddPolicy("StaffManager", policy =>
                {
                    policy.AddRequirements(new IsStaffManagerRequirement(AccountRole.PM_STAFF_MANAGER));
                });

                // Quản lý sản phẩm
                options.AddPolicy("ProductManager", policy =>
                {
                    policy.Requirements.Add(new RoleRequirementBuilder()
                        .SetRequiredRole(AccountRole.PM_PRODUCT_MANAGER)
                        .AddRoleClaim(CategoryRole.PM_CREATE_CATEGORY)
                        .AddRoleClaim(CategoryRole.PM_EDIT_CATEGORY)
                        .AddRoleClaim(CategoryRole.PM_DELETE_CATEGORY)
                        .Build());
                });

                // Nhân viên bán hàng
                options.AddPolicy("Salesperson", policy =>
                {
                    policy.Requirements.Add(new RoleRequirementBuilder()
                        .SetRequiredRole(AccountRole.PM_SALESPERSON)
                        .Build());
                });

                // Khách hàng
                options.AddPolicy("Customer", policy =>
                {
                    policy.Requirements.Add(new RoleRequirementBuilder()
                        .SetRequiredRole(AccountRole.PM_CUSTOMER)
                        .Build());
                });
            });

            services.AddTransient<IAuthorizationHandler, RoleAuthorizationHandler>();
            services.AddTransient<IAuthorizationHandler, IsAdminHandler>();
            services.AddTransient<IAuthorizationHandler, IsStaffManagerHandler>();

            return services;
        }
    }
}
