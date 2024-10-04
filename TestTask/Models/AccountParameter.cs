using Newtonsoft.Json;

namespace TestTask.Models
{
    public class AccountParameter
    {
        [JsonProperty("pl")]
        public string Pl { get; set; }

        [JsonProperty("vl")]
        public string Vl { get; set; }

        [JsonProperty("p")]
        public string P { get; set; }

        [JsonProperty("v")]
        public string V { get; set; }

        [JsonProperty("pu")]
        public string Pu { get; set; }

        [JsonProperty("g")]
        public List<G> G { get; set; }
    }
}
