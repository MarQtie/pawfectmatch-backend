namespace PawfectMatch.Backend.DTOs
{
    public class CreateLogDto
    {
        public Guid UserId { get; set; }
        public string Action { get; set; } = string.Empty;
        public string? Details { get; set; }
    }

    public class LogResponseDto
    {
        public Guid Id { get; set; }
        public string Action { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;  
        public Guid? UserId { get; set; }                   
        public string? UserEmail { get; set; }                 
        public DateTime CreatedAt { get; set; }
    }
}