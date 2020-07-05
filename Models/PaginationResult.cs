using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace BotShopCore.Models {
  public class PaginationResult<TItem> {
    [Required] public List<TItem> Items { get; set; }
    [Required] public int Page { get; set; }
    [Required] public int Limit { get; set; }
    [Required] public int TotalPages { get; set; }

    [JsonIgnore] public bool IsFirstPage => Page == 0;

    [JsonIgnore] public bool IsLastPage => TotalPages == 0 || Page == TotalPages - 1;
  }
}