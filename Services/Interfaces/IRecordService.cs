﻿using zavrsni_backend.Models.DTO;

namespace zavrsni_backend.Services.Interfaces
{
    public interface IRecordService
    {
        public Task<IList<RecordDTO>> GetAllRecords(CancellationToken token);
        public Task<IList<RecordDTO>> UploadFile(IFormFile file, CancellationToken token);
        public Task<int> CreateRecord(RecordDTO recordDto, CancellationToken token);
        public Task UpdateRecord(RecordDTO recordDto, CancellationToken token);
        public Task DeleteRecord(int recordId, CancellationToken token);
    }
}
