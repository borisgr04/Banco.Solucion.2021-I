using Banco.Application.Test.Dobles;
using Banco.Infrastructure.Data;
using Banco.Infrastructure.Data.ObjectMother;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.Application.Test
{
    /*
    HU TRASLADO: COMO CAJERO QUIERO TRANSFERERIR DINERO DE UNA CUENTA ORIGEN A UNA CUENTA DESTINO DE ACUERDO A LOS INDICADO POR EL USUARIO
   CRITERIOS DE ACEPTACIÓN:
   LAS CUENTAS DEBEN EXISTIR PREVIAMENTE
   LA CUENTA  ORIGEN DEBE TENER LA DISPONIBILIDAD PARA RETIRAR EL MONTO A TRASLADAR
   AL FINALIZAR EL TRASLADO A LA CUENTA ORIGEN SE LE REDUCIRA EL VALOR TRASLADO
   AL FINALIZAR EL TRASLADO A LA CUENTA DESTINO SE LE AUMENTARÁ EL VALOR TRASLADO
   */
    class TrasladoDineroServiceConBaseDeDatosTest
    {
        private BancoContext _dbContext;
        private TrasladarService _trasladarService;//SUT - Objeto bajo prueba

        //se ejecuta una vez por cada prueba //hace parte del Arrange
        [SetUp]
        public void Setup()
        {
            //Arrange
            var optionsSqlite = new DbContextOptionsBuilder<BancoContext>()
           .UseSqlite(@"Data Source=bancoDataBaseTest.db")
           .Options;

            _dbContext = new BancoContext(optionsSqlite);
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();


            _trasladarService = new TrasladarService(
                new UnitOfWork(_dbContext),
                new CuentaBancariaRepository(_dbContext),
                new MailServerSpy());
        }

   
        /*
         * ESCENARIO: ESCENARIO EXITOSO
         * DADO QUE TENERMOS UNA CUENTA ORIGEN #1020 CON UN SALDO INICIAL DE 1 MILLON DE PESOS 
         * Y TENEMOS UNA CUENTA DESTINO #1030 CON UN SALDO INICIAL DE 500 MIL PESOS
         * CUANDO SE TRASLADE 200 MIL PESOS DE LA CUENTA ORIGEN A LA CUENTA DESTINO
         * ENTONCES LA CUENTA ORIGEN QUEDARA CON UN SALDO DE 800 MIL PESOS
         * Y LA CUENTA DESTINO CON UN SALDO DE 700 MIL PESOS
         */
        [Test]
        public void PuedeHacerTrasladoTest()
        {

            //Arrange
            var cuentaAhorroOrigen = CuentaBancariaMother.CreateCuentaAhorros("1020");
            var cuentaAhorroDestino = CuentaBancariaMother.CreateCuentaAhorros("1030");
            cuentaAhorroOrigen.Consignar(1000000, "VALLEDUPAR", new DateTime(2020, 1, 1));
            cuentaAhorroDestino.Consignar(500000, "VALLEDUPAR", new DateTime(2020, 1, 1));
            _dbContext.CuentasBancarias.Add(cuentaAhorroOrigen);
            _dbContext.CuentasBancarias.Add(cuentaAhorroDestino);
            _dbContext.SaveChanges();
            

            //Act
            var response = _trasladarService.Handle(
                new TrasladarRequest
                (
                    cuentaAhorroOrigen.Numero, 
                    cuentaAhorroDestino.Numero, 
                    "VALLEDUPAR", 
                    200000,
                    new System.DateTime(2021, 1, 2)));
            //Assert
            //Obtener los objetos de la base de datos.
            cuentaAhorroOrigen = _dbContext.CuentasBancarias.FirstOrDefault(t => t.Numero == cuentaAhorroOrigen.Numero);
            cuentaAhorroDestino = _dbContext.CuentasBancarias.FirstOrDefault(t => t.Numero == cuentaAhorroDestino.Numero);
            response.Mensaje.Should().Be("Se realizó el traslado satisfactoriamente");
            cuentaAhorroOrigen.Saldo.Should().Be(800000);
            cuentaAhorroDestino.Saldo.Should().Be(700000);
            //
            //Revertir
            _dbContext.CuentasBancarias.Remove(cuentaAhorroOrigen);
            _dbContext.CuentasBancarias.Remove(cuentaAhorroDestino);
            _dbContext.SaveChanges();

        }

        /*
        * ESCENARIO: ESCENARIO CUENTA ORIGEN SIN DISPONIBILIDAD
        * DADO QUE TENERMOS UNA CUENTA ORIGEN #1020 CON UN SALDO INICIAL DE 1 MILLON DE PESOS 
        * Y TENEMOS UNA CUENTA DESTINO #1030 CON UN SALDO INICIAL DE 500 MIL PESOS
        * CUANDO SE TRASLADE 1000000 MIL PESOS DE LA CUENTA ORIGEN A LA CUENTA DESTINO
        * ENTONCES EL SISTEMA ENVIARA UN MENSAJE DE TRASLADO NO PERMITIDO 
        * Y LOS SALDOS DE AMBAS CUENTA QUEDAN SIN MODIFICACION
        * Y LA CUENTA DESTINO CON UN SALDO DE 700 MIL PESOS /*
         * ESCENARIO: ESCENARIO EXITOSO
         * DADO QUE TENERMOS UNA CUENTA ORIGEN #1020 CON UN SALDO INICIAL DE 1 MILLON DE PESOS 
         * Y TENEMOS UNA CUENTA DESTINO #1030 CON UN SALDO INICIAL DE 500 MIL PESOS
         * CUANDO SE TRASLADE 200 MIL PESOS DE LA CUENTA ORIGEN A LA CUENTA DESTINO
         * ENTONCES LA CUENTA ORIGEN QUEDARA CON UN SALDO DE 800 MIL PESOS
         */
        [Test]
        public void NoPuedeHacerTrasladoCuentaAhorroSinDispinibilidadTest()
        {

            //Arrange
            var cuentaAhorroOrigen = CuentaBancariaMother.CreateCuentaAhorros("1025");
            var cuentaAhorroDestino = CuentaBancariaMother.CreateCuentaAhorros("1035");
            cuentaAhorroOrigen.Consignar(1000000, "VALLEDUPAR", new DateTime(2020, 1, 1));
            cuentaAhorroDestino.Consignar(500000, "VALLEDUPAR", new DateTime(2020, 1, 1));
            _dbContext.CuentasBancarias.Add(cuentaAhorroOrigen);
            _dbContext.CuentasBancarias.Add(cuentaAhorroDestino);
            _dbContext.SaveChanges();


            //Act
            var response = _trasladarService.Handle(
                new TrasladarRequest
                (
                    cuentaAhorroOrigen.Numero,
                    cuentaAhorroDestino.Numero,
                    "VALLEDUPAR",
                    1000000,
                    new DateTime(2021, 1, 2)));
            //Assert
            response.Mensaje.Should().Be("La cuenta origen no tiene la disponibilidad para efectuar el traslado");
            cuentaAhorroOrigen.Saldo.Should().Be(1000000);
            cuentaAhorroDestino.Saldo.Should().Be(500000);
            //
            //Revertir
            _dbContext.CuentasBancarias.Remove(cuentaAhorroOrigen);
            _dbContext.CuentasBancarias.Remove(cuentaAhorroDestino);
            _dbContext.SaveChanges();

        }

       

    }
}
