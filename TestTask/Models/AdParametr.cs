using Newtonsoft.Json;

namespace TestTask.Models
{
    public class AdParameter
    {
        [JsonProperty("pl")]
        public string Pl { get; set; }

        [JsonProperty("vl")]
        public object Vl { get; set; }

        [JsonProperty("p")]
        public string P { get; set; }

        [JsonProperty("v")]
        public object V { get; set; }

        [JsonProperty("pu")]
        public string Pu { get; set; }

        [JsonProperty("g")]
        public List<G> G { get; set; }
    }
}
