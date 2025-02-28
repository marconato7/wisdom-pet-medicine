namespace Wpm.Management.Api.Data;

public static class ManagementDbContextExtensions
{
    public static void EnsureDatabaseIsCreated(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        var managementDbContext = serviceScope.ServiceProvider.GetService<ManagementDbContext>();
        managementDbContext?.Database.EnsureCreated();
    }
}
