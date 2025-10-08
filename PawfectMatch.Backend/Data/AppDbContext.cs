using Microsoft.EntityFrameworkCore;
using PawfectMatch.Backend.Models;

namespace PawfectMatch.Backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<AdoptionRequest> AdoptionRequests { get; set; }
        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<Pet>().ToTable("pets");
            modelBuilder.Entity<AdoptionRequest>().ToTable("adoption_requests");
            modelBuilder.Entity<Log>().ToTable("logs");
        }
    }
}
