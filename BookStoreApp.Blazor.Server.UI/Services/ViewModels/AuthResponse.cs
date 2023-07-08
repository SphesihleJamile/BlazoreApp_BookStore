using Newtonsoft.Json;

namespace BookStoreApp.Blazor.Server.UI.Services.ViewModels
{
    public class AuthResponse
    {
        [JsonProperty("userId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string UserId { get; set; }

        [JsonProperty("userName", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string UserName { get; set; }

        [JsonProperty("token", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Token { get; set; }
    }
}
