using System.ComponentModel.DataAnnotations;

namespace ImportShopCore.Models.Account {
  public class CreateAccountDto {
    [Required]
    [MinLength(46)]
    [MaxLength(46)]
    public string TelegramToken { get; set; }
  }
}