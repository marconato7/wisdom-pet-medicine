using Microsoft.EntityFrameworkCore;
using Polly;
using Wpm.Clinic.Api.Application;
using Wpm.Clinic.Api.Data;
using Wpm.Clinic.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<ManagementService>();

builder.Services.AddScoped<ClinicApplicationService>();

builder.Services.AddDbContext<ClinicDbContext>(options =>
{
    options.UseInMemoryDatabase("wpm.clinic");
});

builder.Services.AddHttpClient<ManagementService>(client =>
{
    var uri = builder
        .Configuration
        .GetSection("Wpm:ManagementServiceUri")
        .Value
        ?? throw new ArgumentNullException(nameof(client));

    client.BaseAddress = new Uri(uri);
})
.AddResilienceHandler("management-pipline", builder =>
{
    builder.AddRetry(new Polly.Retry.RetryStrategyOptions<HttpResponseMessage>()
    {
        BackoffType      = DelayBackoffType.Exponential,
        MaxRetryAttempts = 3,
        Delay            = TimeSpan.FromSeconds(10),
    });
});

var app = builder.Build();

app.EnsureDbIsCreated();

if (app.Environment.IsDevelopment())
{
}

app.MapControllers();

app.Run();
