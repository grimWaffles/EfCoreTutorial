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
        public DbSet<Role> Roles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-5DP7KR2\\MSSQLSERVERWAZI;Database=ECommercePlatform;Trusted_Connection=True;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>()
                .HasNoKey()
                .HasQueryFilter(x=> !x.IsDeleted);

            modelBuilder.Entity<OrderItem>()
                .HasQueryFilter(x => !x.IsDeleted)
                .Property(x => x.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (OrderItemStatus)Enum.Parse(typeof(OrderItemStatus), v))
                ;

            modelBuilder.Entity<Order>()
                .HasQueryFilter(x => !x.IsDeleted)
                .Property(o => o.Status)
                .HasConversion(
                    o => o.ToString(),
                    o => (OrderStatus)Enum.Parse(typeof(OrderStatus), o)
                );

            modelBuilder.Entity<ProductCategory>()
                .HasQueryFilter(x => !x.IsDeleted);

            modelBuilder.Entity<Product>().HasQueryFilter(x => !x.IsDeleted);

            modelBuilder.Entity<Seller>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<User>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Role>().HasQueryFilter (x => !x.IsDeleted); 

            modelBuilder.Entity<User>()
                .HasOne(x => x.Role)
                .WithMany(r=> r.Users)
                .HasForeignKey(f=> f.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Role>().Property(r=>r.Name).HasConversion(r=> r.ToString(), r => (UserRole) Enum.Parse(typeof(UserRole), r));
        }
    }
}
