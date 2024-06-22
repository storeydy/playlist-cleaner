using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using PlaylistCleaner.Api.Extensions;
using PlaylistCleaner.Api.HealthChecks;
using Serilog;

using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) => configuration
    .MinimumLevel.Information()
    .ReadFrom.Services(services)
    .WriteTo.Debug(formatProvider: CultureInfo.InvariantCulture)
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .Enrich.FromLogContext());

// Add services to the container.
builder.Services.AddDependencies();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApiVersioning(opt =>
{
    opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.ReportApiVersions = true;
    opt.ApiVersionReader = new UrlSegmentApiVersionReader();
});

builder.Services.AddVersionedApiExplorer(setup =>
{
    setup.GroupNameFormat = "'v'VVV";
    setup.SubstituteApiVersionInUrl = true;
});

builder.Services.AddCors(x =>
{
    x.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:4200", "https://playlist-cleaner.vercel.app")
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials();
    });
});

builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks()
    .AddCheck<VersionHealthCheck>("version");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    app.UseSerilogRequestLogging();
    app.UseSwagger();
    app.UseSwaggerUI(o =>
    {
        foreach (var groupName in apiVersionDescriptionProvider.ApiVersionDescriptions.Select(x => x.GroupName))
        {
            o.SwaggerEndpoint($"/swagger/{groupName}/swagger.json", groupName);
            o.SwaggerEndpoint($"/swagger/{groupName}-admin/swagger.json", $"{groupName}-admin");
        }
    });
}

app.UseHttpsRedirection();
app.UseHeaderPropagation();

app.UseCors();

app.UseProblemDetails();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
