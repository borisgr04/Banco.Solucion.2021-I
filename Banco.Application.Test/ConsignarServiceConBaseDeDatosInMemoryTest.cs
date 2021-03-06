using Banco.Application.Test.Dobles;
using Banco.Domain;
using Banco.Infrastructure.Data;
using Banco.Infrastructure.Data.ObjectMother;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace Banco.Application.Test
{
    public class ConsignarServiceConBaseDeDatosInMemoryTest
    {
        private BancoContext _dbContext;
        private ConsignarService _consignarService;//SUT - Objeto bajo prueba
       
        //se ejecuta una vez por cada prueba //hace parte del Arrange
        [SetUp]
        public void Setup()
        {
            //Arrange
            var optionsSqlite = new DbContextOptionsBuilder<BancoContext>()
           .UseInMemoryDatabase(@"BancoInmemory")
           .Options;

            _dbContext = new BancoContext(optionsSqlite);

            _consignarService = new ConsignarService(
                new UnitOfWork(_dbContext),
                new CuentaBancariaRepository(_dbContext),
                new MailServerSpy());
        }

        [Test]
        public void ConsignarTest()
        {

            //Arrange
            var cuentaAhorro = CuentaBancariaMother.CreateCuentaAhorros("1010");
                 
            _dbContext.CuentasBancarias.Add(cuentaAhorro);
            _dbContext.SaveChanges();

            //Act
            var response= _consignarService.Consignar(new ConsignarRequest(cuentaAhorro.Numero, "VALLEDUPAR",0, new System.DateTime(2021,1,2)));
            //Assert
            Assert.AreEqual("El valor a consignar es incorrecto", response.Mensaje);

            //
            //Revertir
            _dbContext.CuentasBancarias.Remove(cuentaAhorro);
            _dbContext.SaveChanges();

        }

        [Test]
        public void PuedeConsignar50MilPesosInicialmente()
        {


            var cuentaAhorro = CuentaBancariaMother.CreateCuentaAhorros("1015");


            _dbContext.CuentasBancarias.Add(cuentaAhorro);
            _dbContext.SaveChanges();

            
            //Act
            var response = _consignarService.Consignar(new ConsignarRequest(cuentaAhorro.Numero, "VALLEDUPAR", 50000, new System.DateTime(2021, 1, 2)));
            //Assert
            Assert.AreEqual("Su Nuevo Saldo es de $ 50.000,00 pesos m/c", response.Mensaje);

            //Revertir
            _dbContext.CuentasBancarias.Remove(cuentaAhorro);
            _dbContext.SaveChanges();
        }
    }
}
