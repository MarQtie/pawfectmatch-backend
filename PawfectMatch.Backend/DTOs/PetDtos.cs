namespace PawfectMatch.Backend.DTOs
{
    public class CreatePetDto
    {
        public Guid OwnerId { get; set; }
        public string Name { get; set; }
        public int Type { get; set; } // Maps to PetType enum
        public int? Age { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }
    }

    public class UpdatePetDto
    {
        public string? Name { get; set; }
        public int? Type { get; set; }
        public int? Age { get; set; }
        public string? Description { get; set; }
        public string? PhotoUrl { get; set; }
        public string? Status { get; set; }
    }

    public class PetResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int? Age { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }
        public string Status { get; set; }
        public string? OwnerEmail { get; set; }
    }
}
