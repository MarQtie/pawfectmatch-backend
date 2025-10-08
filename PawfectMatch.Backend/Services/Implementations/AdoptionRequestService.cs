using Microsoft.EntityFrameworkCore;
using PawfectMatch.Backend.Data;
using PawfectMatch.Backend.DTOs;
using PawfectMatch.Backend.Models;
using PawfectMatch.Backend.Services.Interfaces;

namespace PawfectMatch.Backend.Services.Implementations
{
    public class AdoptionRequestService : IAdoptionRequestService
    {
        private readonly AppDbContext _context;

        public AdoptionRequestService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AdoptionRequestResponseDto>> GetAllAsync()
        {
            return await _context.AdoptionRequests
                .Select(ar => new AdoptionRequestResponseDto
                {
                    Id = ar.Id,
                    PetId = ar.PetId,
                    AdopterId = ar.UserId,
                    Status = ar.Status,
                    Notes = ar.Notes,
                    CreatedAt = ar.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<AdoptionRequestResponseDto?> GetByIdAsync(Guid id)
        {
            var request = await _context.AdoptionRequests.FindAsync(id);
            if (request == null) return null;

            return new AdoptionRequestResponseDto
            {
                Id = request.Id,
                PetId = request.PetId,
                AdopterId = request.UserId,
                Status = request.Status,
                Notes = request.Notes,
                CreatedAt = request.CreatedAt
            };
        }

        public async Task<AdoptionRequestResponseDto> CreateAsync(CreateAdoptionRequestDto dto)
        {
            var entity = new AdoptionRequest
            {
                Id = Guid.NewGuid(),
                PetId = dto.PetId,
                UserId = dto.AdopterId,
                Status = "Pending",
                CreatedAt = DateTime.UtcNow
            };

            _context.AdoptionRequests.Add(entity);
            await _context.SaveChangesAsync();

            return new AdoptionRequestResponseDto
            {
                Id = entity.Id,
                PetId = entity.PetId,
                AdopterId = entity.UserId,
                Status = entity.Status,
                CreatedAt = entity.CreatedAt
            };
        }

        public async Task<AdoptionRequestResponseDto?> UpdateAsync(Guid id, UpdateAdoptionRequestDto dto)
        {
            var request = await _context.AdoptionRequests.FindAsync(id);
            if (request == null) return null;

            request.Status = dto.Status ?? request.Status;
            request.Notes = dto.Notes ?? request.Notes;

            await _context.SaveChangesAsync();

            return new AdoptionRequestResponseDto
            {
                Id = request.Id,
                PetId = request.PetId,
                AdopterId = request.UserId,
                Status = request.Status,
                Notes = request.Notes,
                CreatedAt = request.CreatedAt
            };

        }


        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _context.AdoptionRequests.FindAsync(id);
            if (entity == null) return false;

            _context.AdoptionRequests.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
