using Microsoft.EntityFrameworkCore;
using PawfectMatch.Backend.Data;
using PawfectMatch.Backend.DTOs;
using PawfectMatch.Backend.Models;
using PawfectMatch.Backend.Services.Interfaces;

namespace PawfectMatch.Backend.Services.Implementations
{
    public class LogService : ILogService
    {
        private readonly AppDbContext _context;

        public LogService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LogResponseDto>> GetAllAsync()
        {
            return await _context.Logs
                .Select(log => new LogResponseDto
                {
                    Id = log.Id,
                    Action = log.Action,
                    Details = log.Details,        // include Details
                    UserId = log.UserId,
                    CreatedAt = log.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<LogResponseDto?> GetByIdAsync(Guid id)
        {
            var log = await _context.Logs.FindAsync(id);
            if (log == null) return null;

            return new LogResponseDto
            {
                Id = log.Id,
                Action = log.Action,
                Details = log.Details,         
                UserId = log.UserId,
                CreatedAt = log.CreatedAt
            };
        }

        public async Task<LogResponseDto> CreateAsync(CreateLogDto dto)
        {
            var entity = new Log
            {
                Id = Guid.NewGuid(),
                Action = dto.Action,
                UserId = dto.UserId,
                Details = dto.Details ?? "No details provided.", 
                CreatedAt = DateTime.UtcNow
            };

            _context.Logs.Add(entity);
            await _context.SaveChangesAsync();

            return new LogResponseDto
            {
                Id = entity.Id,
                Action = entity.Action,
                Details = entity.Details,     
                UserId = entity.UserId,
                CreatedAt = entity.CreatedAt
            };
        }


        public async Task<bool> DeleteAsync(Guid id)
        {
            var log = await _context.Logs.FindAsync(id);
            if (log == null) return false;

            _context.Logs.Remove(log);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
