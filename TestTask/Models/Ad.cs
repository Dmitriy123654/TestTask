using Newtonsoft.Json;


namespace TestTask.Models
{
    public class Ad
    {
        [JsonProperty("account_id")]
        public string AccountId { get; set; }

        [JsonProperty("account_parameters")]
        public List<AccountParameter> AccountParameters { get; set; }

        [JsonProperty("ad_id")]
        public int AdId { get; set; }

        [JsonProperty("ad_link")]
        public string AdLink { get; set; }

        [JsonProperty("ad_parameters")]
        public List<AdParameter> AdParameters { get; set; }

        [JsonProperty("body")]
        public object Body { get; set; }

        [JsonProperty("body_short")]
        public string BodyShort { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("company_ad")]
        public bool CompanyAd { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("images")]
        public List<Image> Images { get; set; }

        [JsonProperty("is_mine")]
        public bool IsMine { get; set; }

        [JsonProperty("list_id")]
        public int ListId { get; set; }

        [JsonProperty("list_time")]
        public DateTime ListTime { get; set; }

        [JsonProperty("message_id")]
        public string MessageId { get; set; }

        [JsonProperty("paid_services")]
        public PaidServices PaidServices { get; set; }

        [JsonProperty("phone_hidden")]
        public bool PhoneHidden { get; set; }

        [JsonProperty("price_byn")]
        public string PriceByn { get; set; }

        [JsonProperty("price_usd")]
        public string PriceUsd { get; set; }

        [JsonProperty("remuneration_type")]
        public string RemunerationType { get; set; }

        [JsonProperty("show_parameters")]
        public ShowParameters ShowParameters { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
