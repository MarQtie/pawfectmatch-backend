namespace PawfectMatch.Backend.DTOs
{
    public class CreateAdoptionRequestDto
    {
        public Guid PetId { get; set; }
        public Guid AdopterId { get; set; }
        public string Message { get; set; }
    }

    public class UpdateAdoptionRequestDto
    {
        public string? Status { get; set; }
        public string? Notes { get; set; }
    }

    public class AdoptionRequestResponseDto
    {
        public Guid Id { get; set; }
        public Guid PetId { get; set; }
        public Guid AdopterId { get; set; }
        public string PetName { get; set; }
        public string AdopterEmail { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}