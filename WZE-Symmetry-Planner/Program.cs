using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Infrastructure.DependencyInjection;
using Infrastructure.Data;
using Application.DependencyInjection;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
Env.Load();
// Add services to the container.
string? password = Environment.GetEnvironmentVariable("POSTGRESPSW");
string? connectionString = $"Host=localhost;Port=5432;Database=WZE-Symmetry-Planner;Username=postgres;Password={password}";
Console.WriteLine($"📡 Connection string: {connectionString}");
builder.Configuration["ConnectionStrings:DefaultConnection"] = connectionString;
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddControllers();
builder.Services.AddServices();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



// TODO: check this when it's time to deploy properly
builder.Services.AddCors(options => {
    options.AddPolicy("AllowLocalhost", policy => {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
WebApplication app = builder.Build();
app.UseCors("AllowLocalhost");

// until here
using (IServiceScope scope = app.Services.CreateScope()) {
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    if (!context.Units.Any()) {
        SeedData.Seed(context);
    }
}
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
