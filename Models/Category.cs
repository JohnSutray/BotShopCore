using System.Collections.Generic;

namespace ImportShopCore.Models {
  public class Category {
    public string Name { get; set; }
    public IEnumerable<string> Types { get; set; }
  }
}