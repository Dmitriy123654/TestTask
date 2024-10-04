using Newtonsoft.Json;

namespace TestTask.Models.DTO
{
    public class Page
    {
        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("num")]
        public int Num { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
