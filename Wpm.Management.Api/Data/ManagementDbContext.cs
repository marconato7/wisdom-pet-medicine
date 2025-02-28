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
                new Breed(2, "Staffordshire Retriever"),
                new Breed(3, "Golden Retriever"),
            ]
        );

        modelBuilder.Entity<Pet>().HasData
        (
            [
                new Pet { Id = 1, Name = "Totó",    Age = 1, BreedId = 1 },
                new Pet { Id = 2, Name = "Belinha", Age = 1, BreedId = 2 },
                new Pet { Id = 3, Name = "Bailey",  Age = 1, BreedId = 3 },
            ]
        );
    }
}
