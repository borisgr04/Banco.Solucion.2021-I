using Banco.Application;
using Banco.Infrastructure.WebApi.Test.Base;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Banco.Infrastructure.WebApi.Test
{
    public class CuentaBancariaTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        public CuentaBancariaTest(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }
        [Fact]
        public async Task PuedeCrearCuentaBancariaTestAsync()
        {
            var request = new CuentaBancariaRequest()
            {
                Numero = "123456",
                Ciudad = "VALLEDUPAR",
                Email = "test@hotmail.com",
                Nombre = "CUENTA PARA PRUEBAS DESDE LA WEB",
                TipoCuenta = "AHORRO"
            };
            var jsonObject = JsonConvert.SerializeObject(request);
            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
            var httpClient = _factory.CreateClient();
            var responseHttp = await httpClient.PostAsync("api/CuentaBancaria", content);
            responseHttp.StatusCode.Should().Be(HttpStatusCode.OK);
            var respuesta = await responseHttp.Content.ReadAsStringAsync();
            respuesta.Should().Be($"Se creo con exito la cuenta 123456.");
            var context = _factory.CreateContext();
            var cuenta123456 = context.CuentasBancarias.FirstOrDefault(t => t.Numero == "123456");
            cuenta123456.Should().NotBeNull();
        }

        [Fact]
        public async Task PuedeConsignarCuentaBancariaTestAsync()
        {
            var httpClient = _factory.CreateClient();

            #region DADA que existe la cuenta bancaria 123457 y saldo 0
            var request = new CuentaBancariaRequest()
            {
                Numero = "123457",
                Ciudad = "VALLEDUPAR",
                Email = "test@hotmail.com",
                Nombre = "CUENTA PARA PRUEBAS DESDE LA WEB",
                TipoCuenta = "AHORRO"
            };
            var jsonObject = JsonConvert.SerializeObject(request);
            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
          
            var responseHttp = await httpClient.PostAsync("api/CuentaBancaria", content);
            responseHttp.StatusCode.Should().Be(HttpStatusCode.OK);
            var respuesta = await responseHttp.Content.ReadAsStringAsync();
            respuesta.Should().Be($"Se creó con exito la cuenta {request.Numero}.");
            var context = _factory.CreateContext();
            var cuentaBancariaInicial = context.CuentasBancarias.FirstOrDefault(t => t.Numero == request.Numero);
            cuentaBancariaInicial.Should().NotBeNull();
            #endregion
            #region CUANDO se consigne 25.000


            var requestConsignar = new ConsignarRequest(
                 request.Numero,
                 "VALLEDUPAR",
                 25000, 
                 new DateTime(2020,1,1)
            );
            var jsonObjectConsignar = JsonConvert.SerializeObject(requestConsignar);
            var contentConsignar = new StringContent(jsonObjectConsignar, Encoding.UTF8, "application/json");

            var responseHttpConsignar = await httpClient.PostAsync("api/CuentaBancaria/Consignacion", contentConsignar);
            responseHttpConsignar.StatusCode.Should().Be(HttpStatusCode.OK);
            var respuestaConsignar = await responseHttpConsignar.Content.ReadAsStringAsync();
            var responseConsignar=JsonConvert.DeserializeObject<ConsignarResponse>(respuestaConsignar);

            #endregion
            #region ENTONCES el saldo será 25.000
            _ = responseConsignar.Mensaje.Should().Be($"Su Nuevo Saldo es de {requestConsignar.Valor.ToString("C2", new CultureInfo("es-CO"))} pesos m/c");

            context = _factory.CreateContext();
            var cuentaBancariaFinal = context.CuentasBancarias.FirstOrDefault(t => t.Numero == request.Numero);
            cuentaBancariaFinal.Should().NotBeNull();
            cuentaBancariaFinal.Saldo.Should().Be(cuentaBancariaInicial.Saldo + requestConsignar.Valor);
            #endregion

        }
    }
}
