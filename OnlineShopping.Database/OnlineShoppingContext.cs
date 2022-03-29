using Microsoft.EntityFrameworkCore;
using OnlineShopping.Database.Entity;
using System;

namespace OnlineShopping.Database
{
    public class OnlineShoppingContext : DbContext
    {
        public OnlineShoppingContext(DbContextOptions<OnlineShoppingContext> options)
            : base(options)
        { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivitySession> ActivitySessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().Property(x => x.Price).HasPrecision(12, 2);

            modelBuilder.Entity<ActivitySession>().HasIndex(x => x.Session);

            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 1,
                Branch = "Apple",
                Color = "Blue",
                Name = "Iphone13",
                Price = 1200,
                CreatedDate = DateTime.Now
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 2,
                Branch = "Apple",
                Color = "Green",
                Name = "Iphone13",
                Price = 1200,
                CreatedDate = DateTime.Now
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 3,
                Branch = "Apple",
                Color = "Gray",
                Name = "Iphone13",
                Price = 1200,
                CreatedDate = DateTime.Now
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 4,
                Branch = "Samsung",
                Color = "White",
                Name = "Galaxy Z Fold3",
                Price = 900,
                CreatedDate = DateTime.Now
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 5,
                Branch = "Samsung",
                Color = "Gray",
                Name = "Galaxy Z Fold3",
                Price = 900,
                CreatedDate = DateTime.Now
            });
        }
    }
}
