using System.ComponentModel.DataAnnotations;

namespace BotShopCore.Models.Entities {
  public class OrderItem: IIdentifiable {
    public int Id { get; set; }

    [Required] public int ProductId { get; set; }
    public Product Product { get; set; }

    [Required] public int OrderId { get; set; }
    public Order Order { get; set; }
  }
}