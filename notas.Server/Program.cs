using Microsoft.EntityFrameworkCore;
using notas.Server.Backend.Domain.Interfaces;
using notas.Server.Backend.Application.Services;
using notas.Server.Backend.Infrastructure.Data;
using notas.Server.Backend.Infrastructure.Services;
using notas.Server.Backend.Application.Interfaces;

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

builder.Services.AddScoped<INotaFiscalRepository, NotaFiscalRepository>();
builder.Services.AddScoped<NotaFiscalService>();

builder.Services.AddScoped<INotaFiscalService, NotaFiscalService>();
builder.Services.AddScoped<IEmpresaService, EmpresaService>();


// === CORS ===
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFrontend", policy =>
    {
        policy
            .WithOrigins("https://localhost:65124")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

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

// === Ativa CORS ===
app.UseCors("PermitirFrontend");

app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
public partial class Program { }

