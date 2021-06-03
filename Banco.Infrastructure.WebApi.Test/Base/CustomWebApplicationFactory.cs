using Banco.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Banco.Infrastructure.WebApi.Test.Base
{
    public class CustomWebApplicationFactory<TStartup>
    : WebApplicationFactory<TStartup> where TStartup : class
    {
        private readonly string _connectionString=@"Data Source=C:\sqlite\bancoDataBaseTest.db";
        public BancoContext CreateContext() 
        {
            var builder = new DbContextOptionsBuilder<BancoContext>().UseSqlite(_connectionString);
            return new BancoContext(builder.Options);
        }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                #region Reemplazar la inyección del Contexto de Datos de EF Core
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<BancoContext>));

                services.Remove(descriptor);

                services.AddDbContext<BancoContext>(options =>
                {
                    options.UseSqlite(_connectionString);
                });
                #endregion

                #region Eliminar y Crear nueva base de datos. 
                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<BancoContext>();
                    db.Database.EnsureDeleted();
                    db.Database.EnsureCreated();
                    //invocar clase que inicilice los datos semillas. 
                }
                #endregion 
            });
        }
    }
}

