using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ImportShopApi.Models;

namespace ImportShopCore.Models.Entities {
  public class Chat : IIdentifiable {
    [Required]
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }

    [Required] [DefaultValue("menu")] public string Query { get; set; }

    [Required]
    public string Address { get; set; }

    [Required]
    public string Phone { get; set; }

    public int AccountId { get; set; }
    public virtual Account Account { get; set; }

    public virtual ICollection<TelegramMessage> Messages { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; }
  }
}