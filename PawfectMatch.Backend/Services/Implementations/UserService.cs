using PawfectMatch.Backend.Data;
using PawfectMatch.Backend.DTOs;
using PawfectMatch.Backend.Models;
using PawfectMatch.Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using BCryptNet = BCrypt.Net.BCrypt;

namespace PawfectMatch.Backend.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllAsync()
        {
            var users = await _context.Users.ToListAsync();
            return users.Select(u => new UserResponseDto
            {
                Id = u.Id,
                FullName = u.FullName,
                Email = u.Email,
                Role = u.Role.ToString(),
                CreatedAt = u.CreatedAt
            });
        }

        public async Task<UserResponseDto?> GetByIdAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;

            return new UserResponseDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role.ToString(),
                CreatedAt = user.CreatedAt
            };
        }

        public async Task<UserResponseDto> CreateAsync(CreateUserDto dto)
        {
            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = BCryptNet.HashPassword(dto.Password),
                Role = Enum.TryParse<UserRole>(dto.Role, out var parsedRole)
                    ? parsedRole
                    : UserRole.Adopter,
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserResponseDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role.ToString(),
                CreatedAt = user.CreatedAt
            };
        }

        public async Task<UserResponseDto?> UpdateAsync(Guid id, UpdateUserDto dto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;

            user.FullName = dto.FullName ?? user.FullName;
            user.Email = dto.Email ?? user.Email;

            if (!string.IsNullOrEmpty(dto.Password))
                user.PasswordHash = BCryptNet.HashPassword(dto.Password);

            if (!string.IsNullOrEmpty(dto.Role) && Enum.TryParse<UserRole>(dto.Role, out var parsedRole))
                user.Role = parsedRole;

            await _context.SaveChangesAsync();

            return new UserResponseDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role.ToString(),
                CreatedAt = user.CreatedAt
            };
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
