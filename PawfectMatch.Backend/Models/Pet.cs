    namespace PawfectMatch.Backend.Models
{
    public enum PetType
    {
        Dog,
        Cat,
        Bird,
        Other
    }

    public class Pet
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public string Name { get; set; }
        public PetType Type { get; set; }
        public int? Age { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }
        public string Status { get; set; } = "available";

        // Navigation
        public User Owner { get; set; }
        public ICollection<AdoptionRequest> AdoptionRequests { get; set; } = new List<AdoptionRequest>();
    }
}
