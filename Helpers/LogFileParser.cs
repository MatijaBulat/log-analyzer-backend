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

        public LogFileParser(ZavrsniRadDBContext zavrsniRadDBContext)
        {
            _dbContext = zavrsniRadDBContext;
        }

        public IList<Record> ParseItems(string json, bool isWhitelist = false)
        {
            RecordType recordType = isWhitelist ? GetRecordType((int)Enums.RecordType.Whitelisted) : GetRecordType((int)Enums.RecordType.Unclassified);

            var result = JsonConvert.DeserializeObject<JsonFullObject>(json);

            return result.FortianalyzerReport.Charts[0].Rows.Select(row => ProcessRow(row, recordType)).ToList();
        }

        private Record ProcessRow(Row row, RecordType recordType)
        {
            return new Record
            {
                Scrip = GetValue(SrcIp, row),
                Timestamp = DateTime.Parse(GetValue(Timestamp, row)),
                Action = GetValue(Action, row),
                HostName = GetValue(HostName, row),
                Keyword = GetValue(Keyword, row),
                Url = GetValue(Url, row),
                RecordType = recordType
            };
        }

        private static string GetValue(string name, Row row)
        {
            return row.Columns.SingleOrDefault(c => c.Name == name)?.Value ?? string.Empty;
        }

        private RecordType GetRecordType(int id)
        {
            return _dbContext.RecordTypes.FirstOrDefault(rt => rt.Id == id);
        }
    }
}
