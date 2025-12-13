using Blocks.Api.Extensions;
using Carter;
using HealthChecks.UI.Client;
using IcTest.Infrastructure.Extensions;
using IcTest.Shared.Exceptions.Handlers;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSwaggerServices();
builder.Services.AddHandlersAndServices(builder.Configuration);
builder.Services.AddCorsPolicies();
builder.Services.AddCarterAndMediatR();
builder.Services.AddSystemHealthChecks(builder.Configuration);
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.RegisterCustomMapsterConfiguration();
builder.Services.AddOpenTelemetryWithMetrics(builder.Environment.ApplicationName);
// Configure OpenTelemetry Logging
builder.Logging.AddOpenTelemetry(logging =>
{
    logging.IncludeFormattedMessage = true;
    logging.IncludeScopes = true;
});

var app = builder.Build();

app.UseSwaggerPipeline();
app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
// Map Carter module endpoints
app.MapCarter();

app.UseExceptionHandler(options => { });

//Initialise Database
await app.InitialiseDatabaseAsync();

app.Run();
