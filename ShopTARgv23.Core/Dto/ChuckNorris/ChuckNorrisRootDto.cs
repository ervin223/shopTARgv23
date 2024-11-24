using System.Text.Json.Serialization;

namespace ShopTARgv23.Core.Dto.ChuckNorris
{
    public class ChuckNorrisRootDto
    {
        [JsonPropertyName("Categories")]
        public string[] Categories { get; set; }

        [JsonPropertyName("CreatedAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("IconUrl")]
        public string IconUrl { get; set; }

        [JsonPropertyName("Id")]
        public string Id { get; set; }

        [JsonPropertyName("UpdatedAt")]
        public DateTime UpdatedAt { get; set; }

        [JsonPropertyName("Url")]
        public string Url { get; set; }

        [JsonPropertyName("Value")]
        public string Value { get; set; }
    }
}
