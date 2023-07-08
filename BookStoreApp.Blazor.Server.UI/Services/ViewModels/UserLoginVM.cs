using Newtonsoft.Json;

namespace BookStoreApp.Blazor.Server.UI.Services.ViewModels
{
    public class UserLoginVM
    {
        [JsonProperty("username", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Username { get; set; }

        [JsonProperty("password", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Password { get; set; }
    }
}
