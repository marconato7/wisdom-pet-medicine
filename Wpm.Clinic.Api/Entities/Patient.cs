namespace Wpm.Clinic.Api.Entities;

public sealed class Patient
{
    public int    Id      { get; private set; }
    public string Name    { get; private set; }
    public int    Age     { get; private set; }
    // public int    BreedId { get; private set; }
    // public Breed  Breed   { get; private set; }

    #pragma warning disable CS8618
    private Patient() {} // For EF Core
    #pragma warning restore CS8618

    public Patient(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
