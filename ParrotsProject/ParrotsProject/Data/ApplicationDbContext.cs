namespace ParrotsProject.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using ParrotsProject.Data.Models;
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Parrot> Parrots { get; set; }
        public DbSet<Breed> Breeds { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder
                .Entity<Parrot>()
                .HasOne(p => p.Breed)
                .WithMany(b => b.Parrots)
                .HasForeignKey(p => p.BreedId);
        }
    }
}
