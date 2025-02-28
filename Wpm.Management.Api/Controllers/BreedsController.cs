using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wpm.Management.Api.Data;
using Wpm.Management.Api.Entities;

namespace Wpm.Management.Api.Controllers;

[ApiController]
public class BreedsController : ControllerBase
{
    private readonly ILogger<BreedsController> _logger;
    private readonly ManagementDbContext       _managementDbContext;

    public BreedsController
    (
        ILogger<BreedsController> logger,
        ManagementDbContext       managementDbContext
    )
    {
        _logger              = logger;
        _managementDbContext = managementDbContext;
    }

    [HttpGet("api/breeds", Name = nameof(GetAllBreeds))]
    public async Task<IActionResult> GetAllBreeds()
    {
        var allBreeds = await _managementDbContext
            .Set<Breed>()
            .AsNoTracking()
            .ToListAsync();

        return allBreeds is null ? NotFound() : Ok(allBreeds);
    }

    [HttpGet("api/breeds/{id}", Name = nameof(GetBreedById))]
    public async Task<IActionResult> GetBreedById(int id)
    {
        var breed = await _managementDbContext
            .Set<Breed>()
            .AsNoTracking()
            .SingleOrDefaultAsync(breed => breed.Id == id);

        return breed is null ? NotFound() : Ok(breed);
    }

    [HttpPost("api/breeds", Name = nameof(CreateBreed))]
    public async Task<IActionResult> CreateBreed(CreateBreedRequest request)
    {
        try
        {
            var breed = new Breed(0, request.Name);

            _managementDbContext.Add(breed);

            await _managementDbContext.SaveChangesAsync();

            return CreatedAtAction
            (
                actionName:  nameof(CreateBreed),
                routeValues: new { id = breed.Id },
                value:       breed
            );
        }
        catch (Exception exception)
        {
            _logger.LogError(exception.ToString());
            return StatusCode((int) HttpStatusCode.InternalServerError);
        }
    }
}
