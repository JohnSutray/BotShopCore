using System;
using BotShopCore.Enums;

namespace BotShopCore.Attributes {
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
  public sealed class Service : Attribute {
    public ServiceScope Scope { get; set; }
    public Type ResolveType { get; set; }

    public Service(
      ServiceScope scope = ServiceScope.Transient,
      Type resolveType = null
    ) {
      Scope = scope;
      ResolveType = resolveType;
    }
  }
}