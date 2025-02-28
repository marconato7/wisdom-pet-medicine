using Microsoft.EntityFrameworkCore;
using Wpm.Management.Api.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<ManagementDbContext>(options =>
{
    options.UseInMemoryDatabase("wpm.management");
});

var app = builder.Build();

app.EnsureDatabaseIsCreated();

if (app.Environment.IsDevelopment())
{
}

app.MapControllers();

app.Run();
