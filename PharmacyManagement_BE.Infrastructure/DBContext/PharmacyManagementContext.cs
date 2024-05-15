using Microsoft.EntityFrameworkCore;
using PharmacyManagement_BE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.DBContext
{
    public class PharmacyManagementContext : DbContext
    {
        public PharmacyManagementContext()
        {

        }

        public PharmacyManagementContext(DbContextOptions<PharmacyManagementContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-OTHPHUSK\\SQLEXPRESS;Initial Catalog=PharmacyManagement;Integrated Security=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;TrustServerCertificate=True");
            }
        }

        #region DbSet
        public DbSet<Customer> Users { get; set; }
        #endregion
    }
}
