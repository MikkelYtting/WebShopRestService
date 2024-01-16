using Microsoft.EntityFrameworkCore;
using WebShopRestService.DTO;
using WebShopRestService.Models;

namespace WebShopRestService.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<UserCredential> UserCredentials { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderTable> OrderTables { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<ProductAudit> ProductAudits { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<PaymentAudit> PaymentAudits { get; set; }
        public DbSet<SortProductDTO> SortProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define any specific configurations for your entities here, if needed
            modelBuilder.Entity<SortProductDTO>().HasNoKey();
            modelBuilder.Entity<Product>()
                .ToTable(tb => tb.HasTrigger("trg_AuditProductPriceChange"));
            modelBuilder.Entity<Category>()
                .ToTable(tb => tb.HasTrigger("trg_PreventCategoryDeletion"));
            modelBuilder.Entity<Payment>()
                .ToTable(tb => tb.HasTrigger("trg_AuditPayment"));
            modelBuilder.Entity<Payment>()
                .ToTable(tb => tb.HasTrigger("after_payments"));

        }
    }
}
