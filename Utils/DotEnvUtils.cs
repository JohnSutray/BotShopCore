using DotNetEnv;

namespace BotShopCore.Utils {
  public static class DotEnvUtils {
    public static void InjectDotEnvVars() {
#if DEBUG
      Env.Load();
#endif
    }
  }
}
