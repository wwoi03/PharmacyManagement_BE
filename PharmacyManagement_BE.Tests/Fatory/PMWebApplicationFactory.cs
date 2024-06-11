using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PharmacyManagement_BE.Infrastructure.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Tests.Fatory
{
    internal class PMWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Register a new DBContext that will use our test connection string
                string? connString = "Data Source=LAPTOP-OTHPHUSK\\SQLEXPRESS;Initial Catalog=PharmacyManagement;Integrated Security=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;TrustServerCertificate=True";
                services.AddSqlServer<PharmacyManagementContext>(connString);

                PharmacyManagementContext dbContext = CreateDbContext(services);
                dbContext.Database.EnsureDeleted();
            });

            builder.UseEnvironment("Development");
        }

        private static string? GetConnectionString()
        {
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<PMWebApplicationFactory>()
                .Build();

            var connString = configuration.GetConnectionString("ConnectionString");
            return connString;
        }

        private static PharmacyManagementContext CreateDbContext(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<PharmacyManagementContext>();
            return dbContext;
        }
    }
}
