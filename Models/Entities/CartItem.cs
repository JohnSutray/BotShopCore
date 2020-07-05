using System.ComponentModel.DataAnnotations;

namespace BotShopCore.Models.Entities {
  public class CartItem : IIdentifiable {
    public int Id { get; set; }

    [Required] public int ProductId { get; set; }
    public virtual Product Product { get; set; }

    [Required] public int ChatId { get; set; }
    public virtual Chat Chat { get; set; }
  }
}