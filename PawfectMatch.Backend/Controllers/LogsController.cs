using Microsoft.AspNetCore.Mvc;
using PawfectMatch.Backend.DTOs;
using PawfectMatch.Backend.Services.Interfaces;

namespace PawfectMatch.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogsController : ControllerBase
    {
        private readonly ILogService _logService;
        public LogsController(ILogService logService) => _logService = logService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogResponseDto>>> GetAll()
        {
            var logs = await _logService.GetAllAsync();
            return Ok(logs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LogResponseDto>> Get(Guid id)
        {
            var log = await _logService.GetByIdAsync(id);
            return log is null ? NotFound() : Ok(log);
        }

        [HttpPost]
        public async Task<ActionResult<LogResponseDto>> Create([FromBody] CreateLogDto dto)
        {
            var created = await _logService.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _logService.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}
