using System;
using System.Collections.Generic;
using System.Reflection;
using BotShopCore.Attributes;
using BotShopCore.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace BotShopCore.Extensions {
  public static class ServiceCollectionExtensions {
    private static readonly IDictionary<Assembly, IDictionary<Type, ServiceScope>> ServicesCache =
      new Dictionary<Assembly, IDictionary<Type, ServiceScope>>();

    public static IServiceCollection AddAssemblyServices(
      this IServiceCollection serviceCollection,
      Assembly assembly
    ) {
      if (ServicesCache.ContainsKey(assembly))
        serviceCollection.AddServicesOfCachedAssembly(assembly);
      else
        serviceCollection.AddServicesOfNewAssembly(assembly);

      return serviceCollection;
    }

    private static void AddServicesOfCachedAssembly(this IServiceCollection serviceCollection, Assembly assembly) {
      foreach (var (service, scope) in ServicesCache[assembly]) {
        serviceCollection.AddServiceByScope(service, scope);
      }
    }

    private static void AddServicesOfNewAssembly(this IServiceCollection serviceCollection, Assembly assembly) {
      var typesToMap = new Dictionary<Type, ServiceScope>();
      ServicesCache[assembly] = typesToMap;

      foreach (var service in assembly.GetTypes()) {
        var serviceAttribute = service.GetCustomAttribute<Service>();

        if (serviceAttribute == null) continue;

        serviceCollection.AddServiceByScope(service, serviceAttribute.Scope);
        typesToMap.Add(service, serviceAttribute.Scope);
      }
    }

    private static void AddServiceByScope(
      this IServiceCollection serviceCollection,
      Type service,
      ServiceScope serviceScope
    ) {
      switch (serviceScope) {
        case ServiceScope.Singleton:
          serviceCollection.AddSingleton(service);
          break;
        case ServiceScope.Transient:
          serviceCollection.AddTransient(service);
          break;
        case ServiceScope.Scoped:
          serviceCollection.AddScoped(service);
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(serviceScope), serviceScope, null);
      }
    }
  }
}
