using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace zavrsni_backend.Models
{
    public class JsonFullObject
    {
        [JsonProperty(PropertyName = "fortianalyzer-report")]
        public FortianalyzerReport FortianalyzerReport { get; set; }
    }
}
