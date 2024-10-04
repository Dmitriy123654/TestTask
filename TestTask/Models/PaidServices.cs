using Newtonsoft.Json;

namespace TestTask.Models
{
    public class PaidServices
    {
        [JsonProperty("halva")]
        public bool Halva { get; set; }

        [JsonProperty("highlight")]
        public bool Highlight { get; set; }

        [JsonProperty("polepos")]
        public bool Polepos { get; set; }

        [JsonProperty("ribbons")]
        public object Ribbons { get; set; }
    }
}
