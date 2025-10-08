using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawfectMatch.Backend.Models
{
    public class AdoptionRequest
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Pet")]
        public Guid PetId { get; set; }
        public Pet Pet { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public User User { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "Pending";

        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
