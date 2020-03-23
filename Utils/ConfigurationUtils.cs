using System.IO;
using Microsoft.Extensions.Configuration;

namespace ImportShopCore.Utils {
  public static class ConfigurationUtils {
    public static IConfiguration CreateAppSettingsConfiguration() => new ConfigurationBuilder()
      .SetBasePath(Directory.GetCurrentDirectory())
      .AddJsonFile("appsettings.json", false)
      .Build();
  }
}