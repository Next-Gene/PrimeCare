using eCommerceApp.Application.DependencyInjection;
using eCommerceApp.Infrastructure.DependencyInjection;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("log/log.text", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();
Log.Logger.Information("Application is building....");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructureService(builder.Configuration);
builder.Services.AddApplicationService();

try
{
    var app = builder.Build();

    app.UseSerilogRequestLogging();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseInfrastructureService();

    app.UseHttpsRedirection();

    app.UseAuthorization();


    app.MapControllers();

    Log.Logger.Information("Application is running....");

    app.Run();
}
catch (Exception ex)
{
    Log.Logger.Error(ex, "Application Failed to start....");
}
finally
{
    Log.CloseAndFlush();
}
