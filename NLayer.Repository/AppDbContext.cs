﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; } //This should be added via Product normally.
        public DbSet<User> Users { get; set; }
        public DbSet<UserCredentials> UserCredentials { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Gets all interfaces in the context and applies
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            //This is not the best practice, Just shown for educational purposes
            modelBuilder.Entity<ProductFeature>().HasData(
                new ProductFeature { Id = 1, Color = "Red", Height = 100, Width = 200, ProductId = 1 },
                new ProductFeature { Id = 2, Color = "Blue", Height = 100, Width = 200, ProductId = 2 }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
