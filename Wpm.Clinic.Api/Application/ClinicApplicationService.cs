using Microsoft.Extensions.Caching.Memory;
using Wpm.Clinic.Api.Data;
using Wpm.Clinic.Api.Entities;
using Wpm.Clinic.Api.Services;

namespace Wpm.Clinic.Api.Application;

public sealed class ClinicApplicationService
(
    ClinicDbContext clinicDbContext,
    ManagementService managementService,
    IMemoryCache memoryCache
)
{
    private readonly ClinicDbContext _clinicDbContext = clinicDbContext;
    private readonly ManagementService _managementService = managementService;
    private readonly IMemoryCache _memoryCache = memoryCache;

    public async Task<Consultation?> Handle
    (
        StartConsultationCommand command,
        CancellationToken cancellationToken = default
    )
    {
        PetInfo? petInfo = await _memoryCache.GetOrCreateAsync
        (
            command.PatientId,
            async cacheEntry =>
            {
                cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30);
                return await _managementService.GetPetInfoById(command.PatientId);
            }
        );

        var consultation = new Consultation
        (
            Guid.CreateVersion7(),
            petInfo.Id,
            petInfo.Name,
            petInfo.Age,
            DateTime.UtcNow
        );

        _clinicDbContext.Add(consultation);

        await _clinicDbContext.SaveChangesAsync(cancellationToken);

        return consultation;
    }
}
