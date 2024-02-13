using Microsoft.AspNetCore.Mvc;
using zavrsni_backend.Models.DTO;
using zavrsni_backend.Services.Interfaces;

namespace zavrsni_backend.Controllers
{
    [Route("api/record")]
    [ApiController]
    public class RecordController : ControllerBase
    {
        private IRecordService _recordService;

        public RecordController(IRecordService recordService)
        {
            _recordService = recordService;
        }

        [HttpPost("uploadFile")]
        public async Task<IActionResult> UploadFile(IFormFile file, CancellationToken cancellationToken)
        {
            var list = await _recordService.UploadFile(file, cancellationToken);

            return list is null ? NoContent() : Ok(list);
        }

        [HttpPost("createRecord")]
        public async Task<IActionResult> CreateRecord([FromBody] RecordDTO recordDTO, CancellationToken cancellationToken)
        {
            await _recordService.CreateRecord(recordDTO, cancellationToken);

            return Ok();
        }

        [HttpDelete("deleteRecord/{id}")]
        public async Task<IActionResult> DeleteRecord([FromRoute] int id, CancellationToken cancellationToken)
        {
            await _recordService.DeleteRecord(id, cancellationToken);

            return Ok();
        }

        [HttpPut("updateRecord/{id}")]
        public async Task<IActionResult> UpdateCultureObject([FromBody] RecordDTO recordDto, CancellationToken cancellationToken)
        {
            await _recordService.UpdateRecord(recordDto, cancellationToken);

            return Ok();
        }
    }
}
