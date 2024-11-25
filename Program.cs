using Microsoft.EntityFrameworkCore;
using LogisticsApi.Data;
using Microsoft.OpenApi.Models; // Required for Swagger

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Logistics API", Version = "v1" });
});

// Configure SQLite database
builder.Services.AddDbContext<LogisticsDbContext>(options =>
    options.UseSqlite("Data Source=logistics.db"));

var app = builder.Build();

// Enable Swagger middleware in the pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Logistics API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Ensure the database is created
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<LogisticsDbContext>();
    dbContext.Database.EnsureCreated();
}

app.Run();
