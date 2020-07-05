using BotShopCore.Utils;
using Microsoft.EntityFrameworkCore.Design;

namespace BotShopCore {
  // ReSharper disable once UnusedType.Global
  public class ApplicationContextUpdateFactory : IDesignTimeDbContextFactory<ApplicationContext> {
    public ApplicationContext CreateDbContext(string[] args) {
      DotEnvUtils.InjectDotEnvVars();
      return new ApplicationContext();
    }
  }
}
