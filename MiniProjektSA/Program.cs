using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniProjektSA.Controllers;
using MiniProjektSA.Data;
using MiniProjektSA.Services;

var builder = WebApplication.CreateBuilder(args);

// CORS
var AllowSomeStuff = "_AllowSomeStuff";
builder.Services.AddCors(options =>
{
    options.AddPolicy(AllowSomeStuff, policy =>
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

// Database
builder.Services.AddDbContext<MainContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ContextSQLite")));

// Services
builder.Services.AddScoped<DataService>();
builder.Services.AddControllers();
builder.Services.AddScoped<PostController>();


var app = builder.Build();

// Opret database + seed data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MainContext>();
    context.Database.EnsureCreated();

    var dataService = scope.ServiceProvider.GetRequiredService<DataService>();
    dataService.SeedData();
}

app.UseCors(AllowSomeStuff);
app.MapControllers();
app.MapGet("/", () => "BACKEND KØRE!");

app.Run();