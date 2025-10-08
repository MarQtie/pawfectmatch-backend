using Microsoft.AspNetCore.Mvc;
using PawfectMatch.Backend.DTOs;
using PawfectMatch.Backend.Services.Interfaces;

namespace PawfectMatch.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetsController : ControllerBase
    {
        private readonly IPetService _petService;
        public PetsController(IPetService petService) => _petService = petService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PetResponseDto>>> GetAll()
        {
            var pets = await _petService.GetAllAsync();
            return Ok(pets);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PetResponseDto>> Get(Guid id)
        {
            var pet = await _petService.GetByIdAsync(id);
            return pet is null ? NotFound() : Ok(pet);
        }

        [HttpPost]
        public async Task<ActionResult<PetResponseDto>> Create([FromBody] CreatePetDto dto)
        {
            var created = await _petService.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePetDto dto)
        {
            var success = await _petService.UpdateAsync(id, dto);
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _petService.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}
