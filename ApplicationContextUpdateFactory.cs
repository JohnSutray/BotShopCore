using ImportShopCore.Utils;
using Microsoft.EntityFrameworkCore.Design;

namespace ImportShopCore {
  // ReSharper disable once UnusedType.Global
  public class ApplicationContextUpdateFactory : IDesignTimeDbContextFactory<ApplicationContext> {
    public ApplicationContext CreateDbContext(string[] args) => new ApplicationContext(
      ConfigurationUtils.CreateAppSettingsConfiguration()
    );
  }
}