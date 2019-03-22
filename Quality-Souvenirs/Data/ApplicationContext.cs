using System;
using Microsoft.EntityFrameworkCore;
using QualitySouvenirs.Models;

namespace QualitySouvenirs.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        public DbSet<Souvenir> Souvenirs { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Souvenir>().ToTable("Souvenir");
            modelBuilder.Entity<Category>().ToTable("Category");
        }
    }
}
