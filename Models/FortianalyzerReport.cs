using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace zavrsni_backend.Models
{
    public class FortianalyzerReport
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "charts")]
        public IList<Chart> Charts { get; set; }
    }
}
