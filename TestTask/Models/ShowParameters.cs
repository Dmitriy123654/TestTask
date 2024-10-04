using Newtonsoft.Json;

namespace TestTask.Models
{
    public class ShowParameters
    {
        [JsonProperty("show_call")]
        public bool ShowCall { get; set; }

        [JsonProperty("show_chat")]
        public bool ShowChat { get; set; }

        [JsonProperty("show_import_link")]
        public bool ShowImportLink { get; set; }

        [JsonProperty("show_web_shop_link")]
        public bool ShowWebShopLink { get; set; }
    }
}
