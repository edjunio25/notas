using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using notas.Server;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using notas.Server.Backend.Domain.Entities;
using notas.Server.Backend.Domain.Enums;
using notas.Server.Backend.Domain.ValueObjects;
using notas.Server.Backend.Infrastructure.Data;

namespace notas.Tests.Integration
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing"); 

            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseInMemoryDatabase("TestDb");
                });

                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                context.Database.EnsureCreated();

                context.Empresas.Add(new Empresa(
                    "Empresa Teste",
                    "Fantasia Teste",
                    "12345678901234",
                    new Endereco
                    {
                        logradouro = "Rua dos Aimorés",
                        numero = "100",
                        bairro = "Centro",
                        cidade = "Belo Horizonte",
                        uf = UF.MG,
                        cep = "30140071"
                    }
                ));

                context.SaveChanges();
            });
        }
    }


}
