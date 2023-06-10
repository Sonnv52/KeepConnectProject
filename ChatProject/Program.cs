using AutoMapper;
using Chat.Api.Helper.Filters;
using Chat.Api.MilderWares;
using Chat.Application;
using Chat.Hubs;
using Chat.Hubs.Hubs;
using Chat.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<FileFormatFilter>();
builder.Services.CofigurationApplicationServices(builder.Configuration);
builder.Services.ConfigurePersistenceServices(builder.Configuration);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.CofigurationHubsServices();
var path = Directory.GetCurrentDirectory();
var _logger = new LoggerConfiguration()
    .WriteTo.File($"{path}\\Logs\\RunTimeLog.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
builder.Logging.AddSerilog(_logger);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(x => x
          .AllowAnyMethod()
          .AllowAnyHeader()
          .SetIsOriginAllowed(origin => true)
          .AllowCredentials());

app.UseMiddleware();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseRouting();
app.MapHub<ChatHub>("/chatHub");
app.UseAuthorization();
app.MapControllers();

app.Run();
