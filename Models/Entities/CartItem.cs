using System.ComponentModel.DataAnnotations;
using ImportShopApi.Models;

namespace ImportShopCore.Models.Entities {
  public class CartItem : IIdentifiable {
    public int Id { get; set; }

    [Required] public int ProductId { get; set; }
    public virtual Product Product { get; set; }

    [Required] public int ChatId { get; set; }
    public virtual Chat Chat { get; set; }
  }
}