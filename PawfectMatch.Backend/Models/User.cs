namespace PawfectMatch.Backend.Models
{
    public enum UserRole
    {
        Adopter,
        Owner,
        Admin
    }

    public class User
    {
        public Guid Id { get; set; }
        public UserRole Role { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public ICollection<Pet> Pets { get; set; } = new List<Pet>();
        public ICollection<AdoptionRequest> AdoptionRequests { get; set; } = new List<AdoptionRequest>();
        public ICollection<Log> Logs { get; set; } = new List<Log>();
    }
}
