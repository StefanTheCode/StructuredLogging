using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;

Log.Logger = new LoggerConfiguration()
        .WriteTo.Console(new JsonFormatter())
        .WriteTo.File("logs/app.txt")
        .WriteTo.Seq("http://localhost:5341/")
        .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
        .CreateLogger();

Log.Information("Starting Web host");

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Logging.AddSerilog();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

Log.CloseAndFlush();
