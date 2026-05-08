using DotNetEnv;
using Infrastructure.DependencyInjection;
using Infrastructure.Data;
using Application.DependencyInjection;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using static WZE_Symmetry_Planner.Utilities.CommandHelper;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
Env.Load();

string? password = Environment.GetEnvironmentVariable("POSTGRESPSW");
string dbHost = Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";
string dbPort = Environment.GetEnvironmentVariable("DB_PORT") ?? "5433";
string dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? "WZE-Symmetry-Planner";
string dbUser = Environment.GetEnvironmentVariable("DB_USER") ?? "postgres";
string? connectionString = $"Host={dbHost};Port={dbPort};Database={dbName};Username={dbUser};Password={password}";
Console.WriteLine($"📡 Connection string: {connectionString}");
builder.Configuration["ConnectionStrings:DefaultConnection"] = connectionString;

// JWT config from env (fallback to appsettings)
string jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET") ?? builder.Configuration["Jwt:Secret"]!;
string jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? builder.Configuration["Jwt:Issuer"] ?? "wze-api";
string jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? builder.Configuration["Jwt:Audience"] ?? "wze-front";
builder.Configuration["Jwt:Secret"] = jwtSecret;
builder.Configuration["Jwt:Issuer"] = jwtIssuer;
builder.Configuration["Jwt:Audience"] = jwtAudience;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret))
        };
    });
builder.Services.AddAuthorization();

builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfile));
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddControllers().AddJsonOptions(o => {
    o.JsonSerializerOptions.ReferenceHandler =
        ReferenceHandler.IgnoreCycles;
});
builder.Services.AddServices();
builder.Services.AddHttpClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options => {
    options.AddPolicy("AllowLocalhost", policy => {
        var allowedOrigins = (Environment.GetEnvironmentVariable("ALLOWED_ORIGINS") ?? "http://localhost:3000")
            .Split(',', StringSplitOptions.RemoveEmptyEntries);
        policy.WithOrigins(allowedOrigins)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

WebApplication app = builder.Build();
app.UseCors("AllowLocalhost");

using (IServiceScope scope = app.Services.CreateScope()) {
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    try
    {
        bool hasTables = context.Database.GetService<IRelationalDatabaseCreator>()
            .Exists();
        if (!hasTables){
            Console.WriteLine("🧱 No tables detected. Ensuring database and migrations...");
            string migrationsFolder = Path.Combine(Directory.GetCurrentDirectory(), "../Infrastructure/Migrations");

            if (!Directory.Exists(migrationsFolder) || Directory.GetFiles(migrationsFolder, "*.cs").Length == 0){
                Console.WriteLine("📦 No migrations found — creating initial migration...");

                RunCommand("dotnet", "ef migrations add InitialCreate --project ../Infrastructure --startup-project .");
            }

            Console.WriteLine("⚙️ Applying migrations...");
            RunCommand("dotnet", "ef database update --project ../Infrastructure --startup-project .");
        }
        if (!context.Units.Any()){
            SeedData.Seed(context);
        }
    } catch (Exception ex){
        Console.WriteLine($"An error occurred while migrating or initializing the database: {ex.Message}");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
