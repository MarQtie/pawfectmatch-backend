using PawfectMatch.Backend.DTOs;

namespace PawfectMatch.Backend.Services.Interfaces
{
    public interface ILogService
    {
        Task<IEnumerable<LogResponseDto>> GetAllAsync();
        Task<LogResponseDto?> GetByIdAsync(Guid id);
        Task<LogResponseDto> CreateAsync(CreateLogDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
