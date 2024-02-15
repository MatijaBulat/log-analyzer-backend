using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using zavrsni_backend.Models.DTO;
using zavrsni_backend.Services.Interfaces;

namespace zavrsni_backend.Controllers
{
    [Route("api/Record")]
    [ApiController]
    public class RecordController : ControllerBase
    {
        private IRecordService _recordService;

        public RecordController(IRecordService recordService)
        {
            _recordService = recordService;
        }

        [Authorize]
        [HttpGet("GetAllRecords")]
        public async Task<IActionResult> GetAllRecords(CancellationToken cancellationToken)
        {
            var list = await _recordService.GetAllRecords(cancellationToken);

            return list is null ? NoContent() : Ok(list);
        }

        [Authorize]
        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFile(IFormFile file, CancellationToken cancellationToken)
        {
            var list = await _recordService.UploadFile(file, cancellationToken);

            return list is null ? NoContent() : Ok(list);
        }

        [Authorize]
        [HttpPost("CreateRecord")]
        public async Task<IActionResult> CreateRecord([FromBody] RecordDTO recordDTO, CancellationToken cancellationToken)
        {
            var id = await _recordService.CreateRecord(recordDTO, cancellationToken);

            return Ok(id);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecord([FromRoute] int id, CancellationToken cancellationToken)
        {
            await _recordService.DeleteRecord(id, cancellationToken);

            return Ok();
        }

        [Authorize]
        [HttpPut("UpdateRecord")]
        public async Task<IActionResult> UpdateCultureObject([FromBody] RecordDTO recordDto, CancellationToken cancellationToken)
        {
            await _recordService.UpdateRecord(recordDto, cancellationToken);

            return Ok();
        }
    }
}
