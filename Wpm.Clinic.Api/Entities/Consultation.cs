namespace Wpm.Clinic.Api.Entities;

public sealed record Consultation
(
    Guid     Id,
    int      PatientId,
    string   PatientName,
    int      PatientAge,
    DateTime StartTime
);
