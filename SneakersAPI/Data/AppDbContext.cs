﻿using Microsoft.EntityFrameworkCore;
using SneakersAPI.Models;
namespace SneakersAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
        public DbSet<Item> Items { get; set; }  
        
        public DbSet<User> Users { get; set; }

        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
