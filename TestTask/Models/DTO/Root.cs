using Newtonsoft.Json;

namespace TestTask.Models.DTO
{
    public class Root
    {
        [JsonProperty("ads")]
        public List<Ad> Ads { get; set; }

        [JsonProperty("pagination")]
        public Pagination Pagination { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }
    }
}
