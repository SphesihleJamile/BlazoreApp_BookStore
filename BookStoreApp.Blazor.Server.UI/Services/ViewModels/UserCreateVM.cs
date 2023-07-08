using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.Blazor.Server.UI.Services.ViewModels
{
    public class UserCreateVM
    {
        [JsonProperty("firstName", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [Required(AllowEmptyStrings = false)]
        [StringLength(maximumLength: 200)]
        public string FirstName { get; set; }

        [JsonProperty("lastName", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [Required(AllowEmptyStrings = false)]
        [StringLength(maximumLength: 200)]
        public string LastName { get; set; }

        [JsonProperty("email", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [Required(AllowEmptyStrings = false)]
        [EmailAddress]
        [StringLength(maximumLength: 256)]
        public string Email { get; set; }

        [JsonProperty("password", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [Required(AllowEmptyStrings = false)]
        [StringLength(maximumLength: 200)]
        public string Password { get; set; }

        [JsonProperty("role", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        [Required(AllowEmptyStrings = false)]
        public string Role { get; set; }
    }
}
