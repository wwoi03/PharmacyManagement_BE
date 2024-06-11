using Microsoft.EntityFrameworkCore;
using PharmacyManagement_BE.API.Configs;
using PharmacyManagement_BE.Application.Extentions;
using PharmacyManagement_BE.Infrastructure.DBContext;
using PharmacyManagement_BE.Infrastructure.DBContext.Dapper;
using PharmacyManagement_BE.Infrastructure.Extentions;
using StackExchange.Redis;

namespace PharmacyManagement_BE.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add services to the container.
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();

            // Swagger Config
            services.AddSwaggerSetup();

            // Conn Database By EntityFramewoek
            services.AddDbContext<PharmacyManagementContext>(option => option.UseSqlServer(_configuration.GetConnectionString("ConnectionString")));

            // Dapper
            services.AddScoped<PMDapperContext>();

            // Redis
            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(_configuration["Redis:ConnectionString"]));

            // Identity Config
            services.AddIdentityConfig(_configuration);

            // Respositories Extention
            services.AddRepositoryExtension();

            // MediaR Extention
            services.AddMediatRExtention();

            // AutoMapper Extention
            services.AddAutoMapper(typeof(AutoMapperProfileExtention).Assembly);
        }

        public void Configure(WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/Customer/swagger.json", "Customer API");
                    options.SwaggerEndpoint("/swagger/Admin/swagger.json", "Admin API");
                });
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                app.UseMiddleware<TokenValidationExtention>();
                endpoints.MapControllers();
            });
        }
    }
}
