using Microsoft.EntityFrameworkCore;
using Models;

namespace Infrasturcture
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Orders>()
                .HasOne(o => o.Customer)           // 每個 Order 關聯到一個 Customer
                .WithMany(c => c.Orders)           // 每個 Customer 關聯到多個 Order
                .HasForeignKey(o => o.CustomerID)  // 外鍵為 CustomerID
                .OnDelete(DeleteBehavior.Cascade); // 啟用級聯刪除
        }

        public DbSet<Customers> Customers { get; set; }
    }

    
}
