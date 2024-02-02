using zavrsni_backend.Entities.Common;

namespace zavrsni_backend.Entities
{
    public class Record : Entity
    {
        public string Scrip { get; set; }
        public DateTime Timestamp { get; set; }
        public string Action { get; set; }
        public string HostName { get; set; }
        public string Keyword { get; set; }
        public string Url { get; set; }
        public RecordType RecordType { get; set; }
    }
}
