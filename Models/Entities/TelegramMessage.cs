using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ImportShopApi.Models;

namespace ImportShopCore.Models.Entities {
  public class TelegramMessage: IIdentifiable { 
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Required]
    public int Id { get; set; }
    
    [Required] public int ChatId { get; set; }
    public virtual Chat Chat { get; set; }
  }
}