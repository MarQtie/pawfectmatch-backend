using PawfectMatch.Backend.DTOs;

namespace PawfectMatch.Backend.Services.Interfaces
{
    public interface IAdoptionRequestService
    {
        Task<IEnumerable<AdoptionRequestResponseDto>> GetAllAsync();
        Task<AdoptionRequestResponseDto?> GetByIdAsync(Guid id);
        Task<AdoptionRequestResponseDto> CreateAsync(CreateAdoptionRequestDto dto);
        Task<AdoptionRequestResponseDto?> UpdateAsync(Guid id, UpdateAdoptionRequestDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
