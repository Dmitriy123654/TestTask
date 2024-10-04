using Newtonsoft.Json;

namespace TestTask.Models.DTO
{
    public class Pagination
    {
        [JsonProperty("pages")]
        public List<Page> Pages { get; set; }
    }
}
