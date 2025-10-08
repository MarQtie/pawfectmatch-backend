using PawfectMatch.Backend.DTOs;

namespace PawfectMatch.Backend.Services.Interfaces
{
    public interface IPetService
    {
        Task<IEnumerable<PetResponseDto>> GetAllAsync();
        Task<PetResponseDto?> GetByIdAsync(Guid id);
        Task<PetResponseDto> CreateAsync(CreatePetDto dto);
        Task<bool> UpdateAsync(Guid id, UpdatePetDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
