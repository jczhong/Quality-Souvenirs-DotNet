using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QualitySouvenirs.Models;

namespace QualitySouvenirs.Data
{
    public class ApplicationContext : IdentityDbContext<AppUser>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        public DbSet<Souvenir> Souvenirs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Souvenir>().ToTable("Souvenir");
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Supplier>().ToTable("Supplier");
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<OrderDetail>().ToTable("OrderDetail");
            modelBuilder.Entity<CartItem>().ToTable("CartItem");

            modelBuilder.Entity<Order>()
                .HasOne(o => o.AppUser)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(o => o.Souvenir)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(o => o.Order)
                .WithMany(o => o.OrderDetails)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Souvenir>()
                .HasOne(s => s.Category)
                .WithMany(c => c.Souvenirs)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Souvenir>()
                .HasOne(s => s.Supplier)
                .WithMany(s => s.Souvenirs)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<IdentityUser>(b =>
            {
                b.ToTable("AppUser");
            });
        }
    }
}
