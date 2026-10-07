using Microsoft.EntityFrameworkCore;
using NetForge.Core.Interfaces;
using NetForge.Infrastructure.Data;
using NetForge.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// ----- SERVICES REGISTRATION -----

// Register the DbContext with the SQL Server connection string
builder.Services.AddDbContext<NetForgeDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register our application services (interface → implementation)
// AddScoped = one instance per HTTP request (correct lifetime for DbContext-dependent services)
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<INetworkRequirementService, NetworkRequirementService>();

// Register controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ----- MIDDLEWARE PIPELINE -----

// Enable Swagger UI in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();