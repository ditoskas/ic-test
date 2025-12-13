using Blocks.Importer.Extensions;
using HealthChecks.UI.Client;
using IcTest.Infrastructure.Extensions;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHandlersAndServices(builder.Configuration);
builder.Services.AddSystemHealthChecks(builder.Configuration);
builder.Services.RegisterCustomMapsterConfiguration();
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
app.Run();

