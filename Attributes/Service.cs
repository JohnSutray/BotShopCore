using System;
using BotShopCore.Enums;

namespace BotShopCore.Attributes {
  public class Service : Attribute {
    public ServiceScope Scope { get; set; }

    public Service(ServiceScope scope = ServiceScope.Transient) => Scope = scope;
  }
}
