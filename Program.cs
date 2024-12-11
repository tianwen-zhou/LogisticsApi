using Microsoft.EntityFrameworkCore;
using LogisticsApi.Data;
using Microsoft.OpenApi.Models; // Required for Swagger
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "yourapp",
            ValidAudience = "yourapp",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSuperSecretKey"))
        };
    });


// 添加 CORS 服务
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", builder =>
    {
        builder.WithOrigins("http://localhost:5173") // React 前端地址
               .AllowAnyHeader()
               .AllowAnyMethod()
               .WithExposedHeaders("X-Total-Count"); // 暴露自定义头
    });
});

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

// 启用 CORS
app.UseCors("AllowSpecificOrigins");

app.Run();
