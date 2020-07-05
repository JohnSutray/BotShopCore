using System;
using BotShopCore.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BotShopCore {
  public class ApplicationContext : DbContext {
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<BotMessage> Messages { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
      optionsBuilder.UseMySql(Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING"));
      base.OnConfiguring(optionsBuilder);
    }
  }
}
