using System.Collections.Generic;

namespace ImportShopCore.Models {
  public class PaginateResult<TItem> {
    public List<TItem> Items { get; set; }
    public int Page { get; set; }
    public int Limit { get; set; }
    public int TotalPages { get; set; }

    public bool IsFirstPage => Page == 0;
    public bool IsLastPage => TotalPages == 0 || Page == TotalPages - 1;
  }
}