using EfCoreTutorial.Entity.ECommerceModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EfCoreTutorial.Entity.Enums;

namespace EfCoreTutorial.Database
{
    public class EcommerceContext : DbContext
    {
        public DbSet<Cart> Carts { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-5DP7KR2\\MSSQLSERVERWAZI;Database=ECommercePlatform;Trusted_Connection=True;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>()
                .HasNoKey();

            modelBuilder.Entity<OrderItem>()
                .Property(x => x.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (OrderItemStatus)Enum.Parse(typeof(OrderItemStatus), v));

            modelBuilder.Entity<Order>()
                .Property(o => o.Status)
                .HasConversion(
                    o => o.ToString(),
                    o => (OrderStatus)Enum.Parse(typeof(OrderStatus), o)
                );

        }
    }
}
