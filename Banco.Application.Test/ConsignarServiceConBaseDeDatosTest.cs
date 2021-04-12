using Banco.Application.Test.Dobles;
using Banco.Domain;
using Banco.Infrastructure.Data;
using NUnit.Framework;

namespace Banco.Application.Test
{
    public class ConsignarServiceConBaseDeDatosTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void ConsignarTest()
        {
            //Arrange
            var cuentaAhorro = new CuentaAhorro("1010", "Cuenta ejemplo", "VALLEDUPAR", "cliente@bancoacme.com");
            var dbContext = new BancoContext();
            
            dbContext.CuentasBancarias.Add(cuentaAhorro);
            dbContext.SaveChanges();


            var service = new ConsignarService(
                new UnitOfWork(dbContext),
                new CuentaRepository(dbContext),
                new MailServerFake());
            //Act
            var response=service.Consignar("1010","VALLEDUPAR",0, new System.DateTime(2021,1,2));
            //Assert
            Assert.AreEqual("El valor a consignar es incorrecto", response);

            //
            //Revertir
            dbContext.CuentasBancarias.Remove(cuentaAhorro);
            dbContext.SaveChanges();

        }

        [Test]
        public void PuedeConsignar50MilPesosInicialmente()
        {
            //Arrange
            var cuentaAhorro = new CuentaAhorro("1010", "Cuenta ejemplo", "VALLEDUPAR", "cliente@bancoacme.com");
            var dbContext = new BancoContext();

            dbContext.CuentasBancarias.Add(cuentaAhorro);
            dbContext.SaveChanges();


            var service = new ConsignarService(
                new UnitOfWork(dbContext),
                new CuentaRepository(dbContext),
                new MailServerFake());
            //Act
            var response = service.Consignar("1010", "VALLEDUPAR", 50000, new System.DateTime(2021, 1, 2));
            //Assert
            Assert.AreEqual("Su Nuevo Saldo es de $50.000,00 pesos m/c", response);

            //Revertir
            dbContext.CuentasBancarias.Remove(cuentaAhorro);
            dbContext.SaveChanges();
        }
    }
}
