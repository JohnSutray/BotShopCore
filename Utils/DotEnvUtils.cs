using System.IO;
using DotNetEnv;

namespace BotShopCore.Utils {
  public static class DotEnvUtils {
    public static void InjectDotEnvVars() {
#if DEBUG
      while (Directory.GetFiles(Directory.GetCurrentDirectory(), "*.csproj").Length == 0) {
        Directory.SetCurrentDirectory("../");
      }

      Env.Load();
#endif
    }
  }
}
