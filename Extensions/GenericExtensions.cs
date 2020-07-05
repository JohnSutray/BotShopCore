using System.Collections.Generic;

namespace BotShopCore.Extensions {
  public static class GenericExtensions {
    public static IEnumerable<T> WrapIntoEnumerable<T>(this T item) => new[] {item};
  }
}