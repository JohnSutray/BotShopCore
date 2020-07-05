using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace BotShopCore.Extensions {
  public static class ServiceProviderExtensions {
    public static TResult MethodInvokeWithInjection<TResult>(
      this MethodInfo methodInfo,
      object context,
      IServiceProvider provider
    ) {
      var dependencies = methodInfo.GetParameters()
        .Select(p => p.ParameterType)
        .Select(provider.GetRequiredService);

      return (TResult)methodInfo.Invoke(context, dependencies.ToArray());
    }
  }
}
