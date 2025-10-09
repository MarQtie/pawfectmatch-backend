using Microsoft.AspNetCore.Mvc;

using PawfectMatch.Backend.Models;
using PawfectMatch.Backend.DTOs;
using PawfectMatch.Backend.Services.Interfaces;


namespace PawfectMatch.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService) => _userService = userService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponseDto>> Get(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            return user is null ? NotFound() : Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserResponseDto>> Create([FromBody] CreateUserDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Password) || dto.Password.Length < 6)
                return BadRequest("Password must be at least 6 characters long.");

            if (!Enum.TryParse<UserRole>(dto.Role, out var role))
                role = UserRole.Adopter;

            var created = await _userService.CreateAsync(new CreateUserDto
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Password = dto.Password,
                Role = role.ToString()
            });

            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserDto dto)
        {
            if (!string.IsNullOrWhiteSpace(dto.Password) && dto.Password.Length < 6)
                return BadRequest("Password must be at least 6 characters long.");

            if (!string.IsNullOrEmpty(dto.Role) && !Enum.TryParse<UserRole>(dto.Role, out _))
                return BadRequest("Invalid role specified.");

            var updatedUser = await _userService.UpdateAsync(id, dto);
            return updatedUser is null ? NotFound() : Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _userService.DeleteAsync(id);
            if (!result) return NotFound();
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            var token = await _userService.LoginAsync(dto.Email, dto.Password);
            if (token == null)
                return Unauthorized("Invalid email or password");

            return Ok(new LoginResponseDto { Token = token });
        }
    }
}
