using Microsoft.EntityFrameworkCore;
using Wpm.Clinic.Api.Entities;

namespace Wpm.Clinic.Api.Data;

public sealed class ClinicDbContext(DbContextOptions options) : DbContext(options)
{
    // public DbSet<Patient>      Patients      { get; set; }
    public DbSet<Consultation> Consultations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
