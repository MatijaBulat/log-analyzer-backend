using Microsoft.EntityFrameworkCore;
using zavrsni_backend.Entities;
using zavrsni_backend.Persistence;
using zavrsni_backend.Services.Interfaces;

namespace zavrsni_backend.Services
{
    public class RecordTypeService : IRecordTypeService
    {
        private ZavrsniRadDBContext _dbContext;

        public RecordTypeService(ZavrsniRadDBContext zavrsniRadDBContext)
        {
            _dbContext = zavrsniRadDBContext;
        }

        public async Task<IList<RecordType>> GetAllRecordTypes(CancellationToken token)
        {
            return await _dbContext.RecordTypes.ToListAsync(token);
        }
    }
}
