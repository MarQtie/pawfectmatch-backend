using Microsoft.AspNetCore.Mvc;
using PawfectMatch.Backend.DTOs;
using PawfectMatch.Backend.Services.Interfaces;

namespace PawfectMatch.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdoptionRequestsController : ControllerBase
    {
        private readonly IAdoptionRequestService _service;
        public AdoptionRequestsController(IAdoptionRequestService service) => _service = service;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdoptionRequestResponseDto>>> GetAll()
        {
            var requests = await _service.GetAllAsync();
            return Ok(requests);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AdoptionRequestResponseDto>> Get(Guid id)
        {
            var request = await _service.GetByIdAsync(id);
            return request is null ? NotFound() : Ok(request);
        }

        [HttpPost]
        public async Task<ActionResult<AdoptionRequestResponseDto>> Create([FromBody] CreateAdoptionRequestDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAdoptionRequestDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _service.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}
