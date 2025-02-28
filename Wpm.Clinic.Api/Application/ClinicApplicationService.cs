using Wpm.Clinic.Api.Data;
using Wpm.Clinic.Api.Entities;
using Wpm.Clinic.Api.Services;

namespace Wpm.Clinic.Api.Application;

public sealed class ClinicApplicationService
(
    ClinicDbContext   clinicDbContext,
    ManagementService managementService
)
{
    private readonly ClinicDbContext   _clinicDbContext   = clinicDbContext;
    private readonly ManagementService _managementService = managementService;

    public async Task<Consultation?> Handle
    (
        StartConsultationCommand command,
        CancellationToken        cancellationToken = default
    )
    {
        var petInfo = await _managementService.GetPetInfoById(command.PatientId);
        if (petInfo is null)
        {
            return null;
        }

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
