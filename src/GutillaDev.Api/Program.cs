using GutillaDev.Api.Configuration;
using GutillDev.Data.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<MeuDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddIdentityConfig(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddApiConfig();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.ResolveDependencies();

var app = builder.Build();

// Configure

app.UseApiConfig(app.Environment);

app.MapControllers();

app.Run();
