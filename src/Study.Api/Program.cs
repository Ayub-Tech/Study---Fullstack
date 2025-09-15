using Study.Application;
using Study.Infrastructure;
using Study.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS för frontend (Vite kör på http://localhost:5173)
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("frontend", p =>
        p.WithOrigins("http://localhost:5173")
         .AllowAnyHeader()
         .AllowAnyMethod());
});

// Application & Infrastructure
var conn = builder.Configuration.GetConnectionString("Default") ?? "Data Source=study.db";
builder.Services.AddApplication();
builder.Services.AddInfrastructure(conn);

var app = builder.Build();

// Skapa SQLite-databasen om den saknas
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

// Swagger alltid på lokalt
app.UseSwagger();
app.UseSwaggerUI();

// Aktivera CORS (måste komma före MapControllers)
app.UseCors("frontend");

app.MapControllers();
app.Run();
