using Newtonsoft.Json;

namespace BookStoreApp.Blazor.Server.UI.Services.ViewModels
{
    public class AuthorCreateVM
    {
        [JsonProperty("firstName", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string FirstName { get; set; }

        [JsonProperty("lastName", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string LastName { get; set; }

        [JsonProperty("bio", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Bio { get; set; }
    }
}
