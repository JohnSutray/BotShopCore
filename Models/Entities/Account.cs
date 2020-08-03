using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BotShopCore.Models.Entities {
  public class Account : IIdentifiable {
    public int Id { get; set; }

    [Required] [StringLength(46)] public string TelegramToken { get; set; }

    [Required] public bool IsActive { get; set; } = true;

    public ICollection<Product> Products { get; set; }
    public ICollection<Chat> Chats { get; set; }
  }
}
