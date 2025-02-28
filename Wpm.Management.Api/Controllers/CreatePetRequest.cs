namespace Wpm.Management.Api.Controllers;

public sealed record CreatePetRequest
(
    string Name,
    int Age,
    int BreedId
);
