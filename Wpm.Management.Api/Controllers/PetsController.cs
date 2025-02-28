using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wpm.Management.Api.Data;
using Wpm.Management.Api.Entities;

namespace Wpm.Management.Api.Controllers;

[ApiController]
public class PetsController : ControllerBase
{
    private readonly ILogger<PetsController> _logger;
    private readonly ManagementDbContext     _managementDbContext;

    public PetsController
    (
        ILogger<PetsController> logger,
        ManagementDbContext     managementDbContext
    )
    {
        _logger              = logger;
        _managementDbContext = managementDbContext;
    }

    [HttpGet("api/pets", Name = "GetAllPets")]
    public async Task<IActionResult> GetAllPets()
    {
        var allPets = await _managementDbContext
            .Set<Pet>()
            .AsNoTracking()
            .Include(pet => pet.Breed)
            .ToListAsync();

        return allPets is null ? NotFound() : Ok(allPets);
    }

    [HttpGet("api/pets/{id}", Name = "GetPetById")]
    public async Task<IActionResult> GetPetById(int id)
    {
        var pet = await _managementDbContext
            .Set<Pet>()
            .AsNoTracking()
            .Include(pet => pet.Breed)
            .SingleOrDefaultAsync(pet => pet.Id == id);

        return pet is null ? NotFound() : Ok(pet);
    }

    [HttpPost("api/pets", Name = nameof(CreatePet))]
    public async Task<IActionResult> CreatePet(CreatePetRequest request)
    {
        var pet = new Pet() { Name = request.Name, Age = request.Age, BreedId = request.BreedId };

        _managementDbContext.Add(pet);

        await _managementDbContext.SaveChangesAsync();

        return CreatedAtAction
        (
            actionName:  nameof(GetPetById),
            routeValues: new { id = pet.Id },
            value:       pet
        );
    }
}
