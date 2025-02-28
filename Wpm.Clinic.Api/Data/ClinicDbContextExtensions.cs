namespace Wpm.Clinic.Api.Data;

public static class ClinicDbContextExtensions
{
    public static void EnsureDbIsCreated(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();

        var managementDbContext = serviceScope.ServiceProvider.GetService<ClinicDbContext>();

        managementDbContext?.Database.EnsureCreated();
    }
}
