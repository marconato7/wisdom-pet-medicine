using Microsoft.AspNetCore.Mvc;
using Wpm.Clinic.Api.Application;

namespace Wpm.Clinic.Api.Controllers;

[ApiController]
public class ConsultationsController(ClinicApplicationService clinicApplicationService) : ControllerBase
{
    private readonly ClinicApplicationService _clinicApplicationService = clinicApplicationService;

    [HttpPost
    ("api/consultations/start", Name = nameof(StartConsultation))]
    public async Task<IActionResult> StartConsultation
    (
        [FromBody] StartConsultationRequest request,
        CancellationToken                   cancellationToken = default
    )
    {
        var startConsultationCommand = new StartConsultationCommand(request.PatientId);

        var consultation = await _clinicApplicationService.Handle(startConsultationCommand, cancellationToken);

        return Ok(consultation);
    }
}
