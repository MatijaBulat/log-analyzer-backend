using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace zavrsni_backend.Models
{
    public class Row
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "cols")]
        public IList<Column> Columns { get; set; }
    }
}
