using PawfectMatch.Backend.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PawfectMatch.Backend.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponseDto>> GetAllAsync();
        Task<string?> LoginAsync(string email, string password);
        Task<UserResponseDto?> GetByIdAsync(Guid id);
        Task<UserResponseDto> CreateAsync(CreateUserDto dto);
        Task<UserResponseDto?> UpdateAsync(Guid id, UpdateUserDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
