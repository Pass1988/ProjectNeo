using BackendApi.Application.Interfaces;
using BackendApi.Application.Services;
using BackendApi.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IPreventivoRepository, InMemoryPreventivoRepository>();
builder.Services.AddScoped<IPreventivoService, PreventivoService>();
builder.Services.AddScoped<IAdIdentityProvider, PlaceholderAdIdentityProvider>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.MapGet("/health", () => Results.Ok(new { status = "ok" }));

app.Run();
