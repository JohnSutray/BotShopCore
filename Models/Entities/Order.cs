using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ImportShopCore.Models.Entities {
  public class Order : IIdentifiable {
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; }

    [Required] public int ChatId { get; set; }
    public Chat Chat { get; set; }
    
    [Required] public int AccountId { get; set; }
    public Account Account { get; set; }
    
    public ICollection<OrderItem> OrderItems { get; set; }
  }
}