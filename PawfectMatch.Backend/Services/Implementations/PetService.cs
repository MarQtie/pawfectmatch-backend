using Microsoft.EntityFrameworkCore;
using PawfectMatch.Backend.Data;
using PawfectMatch.Backend.DTOs;
using PawfectMatch.Backend.Models;
using PawfectMatch.Backend.Services.Interfaces;

namespace PawfectMatch.Backend.Services.Implementations
{
    public class PetService : IPetService
    {
        private readonly AppDbContext _context;
        public PetService(AppDbContext context) => _context = context;

        public async Task<IEnumerable<PetResponseDto>> GetAllAsync()
        {
            return await _context.Pets
                .Include(p => p.Owner)
                .Select(p => new PetResponseDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Type = p.Type.ToString(),
                    Age = p.Age,
                    Description = p.Description,
                    PhotoUrl = p.PhotoUrl,
                    Status = p.Status,
                    OwnerEmail = p.Owner.Email
                })
                .ToListAsync();
        }

        public async Task<PetResponseDto?> GetByIdAsync(Guid id)
        {
            var pet = await _context.Pets.Include(p => p.Owner).FirstOrDefaultAsync(p => p.Id == id);
            if (pet == null) return null;

            return new PetResponseDto
            {
                Id = pet.Id,
                Name = pet.Name,
                Type = pet.Type.ToString(),
                Age = pet.Age,
                Description = pet.Description,
                PhotoUrl = pet.PhotoUrl,
                Status = pet.Status,
                OwnerEmail = pet.Owner.Email
            };
        }

        public async Task<PetResponseDto> CreateAsync(CreatePetDto dto)
        {
            var pet = new Pet
            {
                Id = Guid.NewGuid(),
                OwnerId = dto.OwnerId,
                Name = dto.Name,
                Type = (PetType)dto.Type,
                Age = dto.Age,
                Description = dto.Description,
                PhotoUrl = dto.PhotoUrl,
                Status = "available"
            };

            _context.Pets.Add(pet);
            await _context.SaveChangesAsync();

            var owner = await _context.Users.FindAsync(dto.OwnerId);

            return new PetResponseDto
            {
                Id = pet.Id,
                Name = pet.Name,
                Type = pet.Type.ToString(),
                Age = pet.Age,
                Description = pet.Description,
                PhotoUrl = pet.PhotoUrl,
                Status = pet.Status,
                OwnerEmail = owner?.Email
            };
        }

        public async Task<bool> UpdateAsync(Guid id, UpdatePetDto dto)
        {
            var pet = await _context.Pets.FindAsync(id);
            if (pet == null) return false;

            pet.Name = dto.Name ?? pet.Name;
            if (dto.Type.HasValue)
                pet.Type = (PetType)dto.Type.Value;
            pet.Age = dto.Age ?? pet.Age;
            pet.Description = dto.Description ?? pet.Description;
            pet.PhotoUrl = dto.PhotoUrl ?? pet.PhotoUrl;
            pet.Status = dto.Status ?? pet.Status;

            _context.Pets.Update(pet);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var pet = await _context.Pets.FindAsync(id);
            if (pet == null) return false;

            _context.Pets.Remove(pet);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
