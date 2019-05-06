using System;
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
        public DbSet<Supplier> suppliers { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderItem> orderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Souvenir>().ToTable("Souvenir");
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Supplier>().ToTable("Supplier");
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<OrderItem>().ToTable("OrderItem");
        }
    }
}
