using ImportShopCore.Extensions.Common;
using ImportShopCore.Models.Account;
using ImportShopCore.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ImportShopCore {
  public class ApplicationContext : DbContext {
    private IConfiguration Configuration { get; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<TelegramMessage> Messages { get; set; }

    public ApplicationContext(IConfiguration configuration) => Configuration = configuration;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
      optionsBuilder.UseMySql(Configuration.GetDefaultConnectionString());
      base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
      modelBuilder.Entity<Chat>().Property(chat => chat.Query).HasDefaultValue("menu");
      
      base.OnModelCreating(modelBuilder);
    }
  }
}