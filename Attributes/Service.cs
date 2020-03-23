using System;
using ImportShopCore.Enums;

namespace ImportShopCore.Attributes {
  public class Service : Attribute {
    public ServiceScope Scope { get; set; }
    
    public Service(ServiceScope scope = ServiceScope.Transient) => Scope = scope;
  }
}