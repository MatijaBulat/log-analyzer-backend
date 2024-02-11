using Newtonsoft.Json;
using zavrsni_backend.Entities;
using zavrsni_backend.Models;
using zavrsni_backend.Persistence;

namespace zavrsni_backend.Helpers
{
    public class LogFileParser
    {
        private const string SrcIp = "srcip";
        private const string Timestamp = "timestamp";
        private const string Action = "Action";
        private const string HostName = "Host Name";
        private const string Keyword = "keyword";
        private const string Url = "URL";

        private ZavrsniRadDBContext _dbContext;
        private RecordType? unclassifiedRecordType;

        public LogFileParser(ZavrsniRadDBContext zavrsniRadDBContext)
        {
            _dbContext = zavrsniRadDBContext;
            unclassifiedRecordType = GetUnclassifiedRecordType();
        }

        public IList<Record> ParseItems(string json)
        {
            var result = JsonConvert.DeserializeObject<JsonFullObject>(json);

            return result.FortianalyzerReport.Charts[0].Rows.Select(ProcessRow).ToList();
        }

        private Record ProcessRow(Row row)
        {
            return new Record
            {
                Scrip = GetValue(SrcIp, row),
                Timestamp = DateTime.Parse(GetValue(Timestamp, row)),
                Action = GetValue(Action, row),
                HostName = GetValue(HostName, row),
                Keyword = GetValue(Keyword, row),
                Url = GetValue(Url, row),
                RecordType = unclassifiedRecordType
            };
        }

        private static string GetValue(string name, Row row)
        {
            return row.Columns.SingleOrDefault(c => c.Name == name)?.Value ?? string.Empty;
        }

        private RecordType GetUnclassifiedRecordType()
        {
            if (unclassifiedRecordType == null)
            {
                unclassifiedRecordType = _dbContext.RecordTypes.FirstOrDefault(rt => rt.Id == (int)Enums.RecordType.Unclassified);
            }
            return unclassifiedRecordType;
        }
    }
}
