using Newtonsoft.Json;

namespace BookStoreApp.Blazor.Server.UI.Services.ViewModels
{
    public class BookCreateVM
    {
        [JsonProperty("title", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("year", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public int? Year { get; set; }

        [JsonProperty("isbn", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Isbn { get; set; }

        [JsonProperty("summary", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Summary { get; set; }

        [JsonProperty("image", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Image { get; set; }

        [JsonProperty("price", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Price { get; set; }

        [JsonProperty("authorId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int AuthorId { get; set; }
    }
}
