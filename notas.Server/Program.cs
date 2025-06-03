using Microsoft.EntityFrameworkCore;
using notas.Server.Backend.Domain.Interfaces;
using notas.Server.Application.Services;
using notas.Server.Infrastructure.Data;
using notas.Server.Backend.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// === Serviços ===
builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddHttpClient<ICepService, ViaCepService>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=documentos.db"));

builder.Services.AddScoped<IEmpresaRepository, EmpresaRepository>();
builder.Services.AddScoped<EmpresaService>();

var app = builder.Build();

// === Pipeline HTTP ===
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
