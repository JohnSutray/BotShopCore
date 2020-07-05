using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotShopCore.Models.Entities {
  public class Chat : IIdentifiable {
    [Required]
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }

    [Required] public string Query { get; set; }

    [Required] public string Address { get; set; }

    [Required]
    [MinLength(13)]
    [MaxLength(13)]
    public string Phone { get; set; }

    [Required]
    [MinLength(1)]
    [MaxLength(byte.MaxValue)]
    public string FirstName { get; set; }

    [Required]
    [MinLength(1)]
    [MaxLength(byte.MaxValue)]
    public string LastName { get; set; }

    public int AccountId { get; set; }
    public Account Account { get; set; }

    public ICollection<BotMessage> Messages { get; set; }

    public ICollection<CartItem> CartItems { get; set; }
  }
}