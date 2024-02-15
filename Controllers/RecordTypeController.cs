using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using zavrsni_backend.Services.Interfaces;

namespace zavrsni_backend.Controllers
{
    [Route("api/RecordType")]
    [ApiController]
    public class RecordTypeController : ControllerBase
    {
        private IRecordTypeService _recordTypeService;

        public RecordTypeController(IRecordTypeService recordTypeService)
        {
            _recordTypeService = recordTypeService;
        }

        [Authorize]
        [HttpGet("GetAllRecordTypes")]
        public async Task<IActionResult> GetAllRecordTypes(CancellationToken cancellationToken)
        {
            var list = await _recordTypeService.GetAllRecordTypes(cancellationToken);

            return list is null ? NoContent() : Ok(list);
        }
    }
}
