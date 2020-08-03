using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotShopCore.Models.Entities {
  public class BotMessage : IIdentifiable {
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Required]
    public int Id { get; set; }

    [Required] public string PlatformId { get; set; }

    [Required] public int ChatId { get; set; }
    public virtual Chat Chat { get; set; }
  }
}