using zavrsni_backend.Entities;
using zavrsni_backend.Models.DTO;

namespace zavrsni_backend.Services.Interfaces
{
    public interface IRecordTypeService
    {
        public Task<IList<RecordType>> GetAllRecordTypes(CancellationToken token);
    }
}
