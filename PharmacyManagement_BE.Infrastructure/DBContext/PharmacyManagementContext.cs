using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PharmacyManagement_BE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.DBContext
{
    public class PharmacyManagementContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        private readonly IConfiguration _configuration;

        public PharmacyManagementContext() { }

        public PharmacyManagementContext(DbContextOptions<PharmacyManagementContext> options, IConfiguration configuration) : base(options)
        {
            this._configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer(_configuration.GetConnectionString("ConnectionString"));
                //optionsBuilder.UseSqlServer("Data Source=dev-mssql-db.lizai.co;Initial Catalog=dev-intern-project-database;User Id=intern;Password=LizAI.I@54321;");
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-OTHPHUSK\\SQLEXPRESS;Initial Catalog=PharmacyManagement;Integrated Security=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductSupport>()
                .HasKey(o => new { o.ProductId, o.SupportId });
            modelBuilder.Entity<ProductDisease>()
                .HasKey(o => new { o.ProductId, o.DiseaseId });
            modelBuilder.Entity<DiseaseSymptom>()
                .HasKey(o => new { o.DiseaseId, o.SymptomId });
            modelBuilder.Entity<ProductIngredient>()
                .HasKey(o => new { o.ProductId, o.IngredientId });
            modelBuilder.Entity<ProductSupport>()
                .HasKey(o => new { o.ProductId, o.SupportId });
            modelBuilder.Entity<PromotionProgram>()
                .HasKey(o => new { o.PromotionProductId, o.ProductId });
            modelBuilder.Entity<VoucherHistory>()
                .HasKey(o => new { o.OrderId, o.VoucherId });
            modelBuilder.Entity<PromotionHistory>()
                .HasKey(o => new { o.PromotionId, o.OrderDetailsId });
            modelBuilder.Entity<OrderDetails>()
               .HasKey(o => new { o.OrderId, o.ShipmentDetailsId, o.UnitId });
            modelBuilder.Entity<ProductUnit>()
               .HasKey(o => new { o.ProductId, o.UnitId });
        }

        #region DbSet
        public DbSet<Branch> Branchs { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Disease> Diseases { get; set; }
        public DbSet<DiseaseSymptom> DiseaseSymptoms { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductDisease> ProductDiseases { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductIngredient> ProductIngredients { get; set; }
        public DbSet<ProductUnit> ProductUnits { get; set; }
        public DbSet<ProductSupport> ProductSupports { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<PromotionHistory> PromotionHistories { get; set; }
        public DbSet<PromotionProduct> PromotionProducts { get; set; }
        public DbSet<PromotionProgram> PromotionPrograms { get; set; }
        public DbSet<ReceiverInformation> ReceiverInformations { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<ShipmentDetails> ShipmentDetails { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Symptom> Symptoms { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<VoucherHistory> VoucherHistories { get; set; }
        #endregion
    }
}
