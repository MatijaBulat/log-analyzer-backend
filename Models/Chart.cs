using Newtonsoft.Json;

namespace zavrsni_backend.Models
{
    public class Chart
    {
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "rows")]
        public IList<Row> Rows { get; set; }
    }
}
