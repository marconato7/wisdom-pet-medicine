using Microsoft.EntityFrameworkCore;
using Wpm.Management.Api.Entities;

namespace Wpm.Management.Api.Data;

public sealed class ManagementDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Pet>   Pets   { get; set; }
    public DbSet<Breed> Breeds { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Breed>().HasData
        (
            [
                new Breed(1, "Beagle"),
                new Breed(2, "Golden Retriever"),
                new Breed(3, "Poodle"),
            ]
        );

        modelBuilder.Entity<Pet>().HasData
        (
            [
                new Pet { Id = 1, Name = "Belinha", Age = 1,            BreedId = 3 },
                new Pet { Id = 2, Name = "Bailey",  Age = 2,            BreedId = 2 },
                new Pet { Id = 3, Name = "Snoopy",  Age = int.MaxValue, BreedId = 1 },
            ]
        );
    }
}
