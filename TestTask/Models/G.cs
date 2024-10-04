using Newtonsoft.Json;

namespace TestTask.Models
{
    public class G
    {
        [JsonProperty("gi")]
        public int Gi { get; set; }

        [JsonProperty("gl")]
        public string Gl { get; set; }

        [JsonProperty("go")]
        public int Go { get; set; }

        [JsonProperty("po")]
        public int Po { get; set; }
    }
}
