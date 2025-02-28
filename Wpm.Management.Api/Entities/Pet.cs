namespace Wpm.Management.Api.Entities;

public sealed class Pet
{
    public int    Id      { get; set; }
    public string Name    { get; set; }
    public int    Age     { get; set; }
    public int    BreedId { get; set; }
    public Breed  Breed   { get; set; }
}
