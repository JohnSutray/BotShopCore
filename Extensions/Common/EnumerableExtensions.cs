using System;
using System.Collections.Generic;
using System.Linq;

namespace BotShopCore.Extensions.Common {
  public static class EnumerableExtensions {
    public static IEnumerable<TGroup> GetGroups<TSource, TGroup>(
      this IEnumerable<TSource> source,
      Func<TSource, TGroup> mapper
    ) => source.GroupBy(mapper).Select(g => g.Key);
  }
}