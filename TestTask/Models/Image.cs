using Newtonsoft.Json;

namespace TestTask.Models
{
    public class Image
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("media_storage")]
        public string MediaStorage { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("yams_storage")]
        public bool YamsStorage { get; set; }
    }

}
