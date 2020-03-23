using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ImportShopApi.Models;

namespace ImportShopCore.Models.Entities {
  public class Account: IIdentifiable {
    public int Id { get; set; }

    [Required]
    [StringLength(46, ErrorMessage = "Длина Telegram токена составляет 46 символов")]
    public string TelegramToken { get; set; }

    [Required] public bool IsActive { get; set; } = true;
    
    public virtual ICollection<Product> Products { get; set; }
    public virtual ICollection<Chat> Chats { get; set; }
  }
}