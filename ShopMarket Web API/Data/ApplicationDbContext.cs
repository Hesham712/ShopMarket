using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopMarket_Web_API.Models;

namespace ShopMarket_Web_API.Data
{
    public class ApplicationDbContext : IdentityDbContext<User,IdentityRole<int>,int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(m => m.Shifts)
                .WithOne(o => o.User)
                .HasForeignKey(f => f.UserId);

            modelBuilder.Entity<Shift>()
                .HasMany(m => m.Orders)
                .WithOne(o => o.Shift)
                .HasForeignKey(f => f.ShiftId);

            modelBuilder.Entity<Order>()
                .HasMany(m => m.OrderItems)
                .WithOne(o => o.Order)
                .HasForeignKey(f => f.OrderId);

            modelBuilder.Entity<Product>()
                .HasMany(m => m.OrderItems)
                .WithOne(o => o.Product)
                .HasForeignKey(f => f.ProductId);

            modelBuilder.Entity<Order>()
                .HasMany(m => m.Refunds)
                .WithOne(o => o.Order)
                .HasForeignKey(f => f.OrderId);

            modelBuilder.Entity<Refund>()
                .HasMany(m => m.RefundItems)
                .WithOne(o => o.Refund)
                .HasForeignKey(f => f.RefundId);
            modelBuilder.Entity<OrderItem>()
                .HasMany(m => m.RefundItem)
                .WithOne(o => o.OrderItem)
                .HasForeignKey(f => f.OrderItemId);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                }
            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<RefundItem> RefundItems { get; set; }
        public DbSet<Refund> Refunds { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
