using zavrsni_backend.Entities;

namespace zavrsni_backend.Models.DTO
{
    public class RecordDTO
    {
        public int? Id { get; set; }
        public string Srcip { get; set; }
        public DateTime Timestamp { get; set; }
        public string Action { get; set; }
        public string HostName { get; set; }
        public string? Keyword { get; set; }
        public string Url { get; set; }
        public RecordType RecordType { get; set; }
    }
}
