using System.Text.Json.Serialization;

namespace ImportShopCore.Enums {
  [JsonConverter(typeof(JsonStringEnumConverter))]
  public enum EDisplayType {
    Video,
    Image,
  }
}