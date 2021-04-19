using Banco.Domain.Contracts;
using Banco.Infrastructure.Data;
using Banco.Infrastructure.Data.Base;
using Banco.Infrastructure.Systems;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Banco.Infrastructure.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("BancoContext");//obtiene la configuracion del appsettitgs

            services.AddDbContext<BancoContext>(opt => opt.UseSqlite(connectionString));

            ///Inyección de dependencia Especifica
            //https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.0#register-additional-services-with-extension-methods
            services.AddScoped<IUnitOfWork, UnitOfWork>(); //Crear Instancia por peticion
            services.AddScoped<ICuentaBancariaRepository, CuentaBancariaRepository>(); //Crear Instancia por peticion
            services.AddScoped<IDbContext, BancoContext>(); //Crear Instancia por peticion
            services.AddScoped<IMailServer, MailServer>(); //Crear Instancia por peticion
            
            //inyección del servicio de mail

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Banco.Infrastructure.WebApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Banco.Infrastructure.WebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
