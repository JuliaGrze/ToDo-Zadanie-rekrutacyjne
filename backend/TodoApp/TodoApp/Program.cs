using Microsoft.EntityFrameworkCore;
using TodoApp.Application.Interfaces;
using TodoApp.Application.Services;
using TodoApp.Infrastructure.Data; 

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

// CORS dla Angulara
const string CorsPolicy = "AllowAngular";

services.AddCors(options =>
{
    options.AddPolicy(CorsPolicy, policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Controllers + Swagger
services.AddControllers();
//services.AddEndpointsApiExplorer();
//services.AddSwaggerGen();

// EF Core + PostgreSQL
var connectionString = configuration.GetConnectionString("DefaultConnection");
services.AddDbContext<TodoDbContext>(options =>
    options.UseNpgsql(connectionString));

// Serwis aplikacyjny
services.AddScoped<ITodoTaskService, TodoTaskService>();

var app = builder.Build();


// HTTP pipeline
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseCors(CorsPolicy);

app.UseAuthorization();

app.MapControllers();

app.Run();
