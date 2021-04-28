using Newtonsoft.Json;

namespace aspnetcore_sql.Models
{
    public class MyItem
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
