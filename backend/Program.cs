using Microsoft.EntityFrameworkCore;
using MowerManagement.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// Luôn lắng nghe http://localhost:5000 khi chạy dotnet run
builder.WebHost.UseUrls("http://localhost:5050");

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// PostgreSQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");
}
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(connectionString ?? "Host=localhost;Port=5432;Database=mower_management;Username=postgres;Password=postgres");
});

// CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseAuthorization();
app.MapControllers();

// Health check endpoint để kiểm tra kết nối DB
app.MapGet("/api/health", async (AppDbContext db) =>
{
    try
    {
        await db.Database.CanConnectAsync();
        return Results.Ok(new { status = "healthy", database = "connected" });
    }
    catch (Exception ex)
    {
        return Results.Json(new { status = "unhealthy", database = "disconnected", error = ex.Message }, statusCode: 503);
    }
});

app.Run();
