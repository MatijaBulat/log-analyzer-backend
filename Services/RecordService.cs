using AutoMapper;
using Microsoft.EntityFrameworkCore;
using zavrsni_backend.Entities;
using zavrsni_backend.Helpers;
using zavrsni_backend.Models.DTO;
using zavrsni_backend.Persistence;
using zavrsni_backend.Services.Interfaces;

namespace zavrsni_backend.Services
{
    public class RecordService : IRecordService
    {
        private ZavrsniRadDBContext _dbContext;
        private IMapper _mapper;

        public RecordService(ZavrsniRadDBContext zavrsniRadDBContext, IMapper mapper)
        {
            _dbContext = zavrsniRadDBContext;
            _mapper = mapper;
        }

        public async Task CreateRecord(RecordDTO recordDto, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(recordDto, nameof(recordDto));

            var recordType = await _dbContext.RecordTypes.FirstOrDefaultAsync(r => r.Id == recordDto.RecordType.Id, token);

            Record record = new Record()
            {
                Scrip = recordDto.Scrip,
                Timestamp = recordDto.Timestamp,
                Action = recordDto.Action,
                HostName = recordDto.HostName,
                Keyword = recordDto.Keyword,
                Url = recordDto.Url,
                RecordType = recordType
            };

            _dbContext.Records.Add(record);
            await _dbContext.SaveChangesAsync(token);
        }

        public async Task DeleteRecord(int recordId, CancellationToken token)
        {
            Record? record = await _dbContext.Records.FirstOrDefaultAsync(r => r.Id == recordId, token);

            if (record is null)
            {
                throw new NullReferenceException($"Record with ID {recordId} not found.");
            }

            _dbContext.Records.Remove(record);
            await _dbContext.SaveChangesAsync(token);
        }

        public async Task UpdateRecord(RecordDTO recordDto, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(recordDto, nameof(recordDto));

            Record? record = await _dbContext.Records.FirstOrDefaultAsync(r => r.Id == recordDto.Id, token);
            RecordType recordType = await _dbContext.RecordTypes.FirstOrDefaultAsync(r => r.Id == recordDto.RecordType.Id, token);

            if (record is null)
            {
                throw new NullReferenceException($"Record with id '{recordDto.Id}' does not exist");
            }

            record.Scrip = recordDto.Scrip;
            record.Timestamp = recordDto.Timestamp;
            record.Action = recordDto.Action;
            record.HostName = recordDto.HostName;
            record.Keyword = recordDto.Keyword;
            record.Url = recordDto.Url;
            record.RecordType = recordType;

            await _dbContext.SaveChangesAsync(token);
        }

        public async Task<IList<RecordDTO>> UploadFile(IFormFile file, CancellationToken token)
        {
            var whiteListUrls = await _dbContext.Records
                .Where(r => r.RecordType.Id == (int)Enums.RecordType.Whitelisted)
                .Select(r => r.Url)
                .ToListAsync(token);

            IList<Record> parsedRecords;

            using (var stream = file.OpenReadStream())
            {
                using (var reader = new StreamReader(stream))
                {
                    var json = reader.ReadToEnd();
                    parsedRecords = new LogFileParser(_dbContext).ParseItems(json, false);            
                }
            }

            var recordsToSave = parsedRecords
                .Where(record => !whiteListUrls.Contains(record.Url))
                .ToList();
 
            await _dbContext.AddRangeAsync(recordsToSave);
            await _dbContext.SaveChangesAsync(token);

            var recordsDTO = recordsToSave.Select(_mapper.Map<RecordDTO>).ToList();

            return recordsDTO;
        }
    }
}
