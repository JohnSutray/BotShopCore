using System;
using System.Linq;
using System.Reflection;
using BotShopCore.Attributes;
using BotShopCore.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace BotShopCore.Extensions {
  public static class ServiceCollectionExtensions {
    public static IServiceCollection AddAssemblyServices(
      this IServiceCollection serviceCollection,
      Assembly assembly
    ) {
      assembly.GetTypes().ToList().ForEach(
        service => service.GetCustomAttributes<Service>().ToList().ForEach(
          serviceAttribute => serviceCollection.AddServiceByScope(
            serviceAttribute.ResolveType ?? service,
            service,
            serviceAttribute.Scope
          )
        )
      );

      return serviceCollection;
    }

    private static void AddServiceByScope(
      this IServiceCollection serviceCollection,
      Type service,
      Type implementation,
      ServiceScope serviceScope
    ) {
      switch (serviceScope) {
        case ServiceScope.Singleton:
          serviceCollection.AddSingleton(service, implementation);
          break;
        case ServiceScope.Transient:
          serviceCollection.AddTransient(service, implementation);
          break;
        case ServiceScope.Scoped:
          serviceCollection.AddScoped(service, implementation);
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(serviceScope), serviceScope, null);
      }
    }
  }
}