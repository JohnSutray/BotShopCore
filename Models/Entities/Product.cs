using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ImportShopCore.Models.Entities {
  public class Product : IIdentifiable {
    public int Id { get; set; }

    [Required] [Range(0f, float.MaxValue)] public float Price { get; set; }

    [Required]
    [MinLength(1)]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    [MinLength(1)]
    [MaxLength(5000)]
    public string Description { get; set; }

    [Required]
    [MinLength(1)]
    [MaxLength(100)]
    public string Category { get; set; }

    [Required]
    [MinLength(1)]
    [MaxLength(100)]
    public string Type { get; set; }

    [Required]
    [MinLength(1)]
    [MaxLength(1000)]
    public string MediaUrl { get; set; }

    [Required] public int AccountId { get; set; }
    public Account Account { get; set; }
  }
}