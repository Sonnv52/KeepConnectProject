using Chat.Application.Queries;
using Chat.Infrastructure.DataContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Connect Sql
builder.Services.AddDbContext<ChatDbContext>(options =>
options.UseSqlServer(
    builder.Configuration.GetConnectionString("SqlServerConnection"),
    b => b.MigrationsAssembly("Chat.Api")));
builder.Services.AddMediatR(typeof(GetUserQuery));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
