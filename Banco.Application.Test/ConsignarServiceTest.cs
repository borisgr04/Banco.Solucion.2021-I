using Banco.Application.Test.Dobles;
using Banco.Infrastructure.Data;
using NUnit.Framework;

namespace Banco.Application.Test
{
    public class ConsignarServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ConsignarTest()
        {
            var spy = new MailServerSpy();
            //Arrange
            var service = new ConsignarService(
                new UnitOfWorkFake(),
                new CuentaBancariaRepositoryStub(),
                spy);
            //Act
            var response=service.Consignar("1010","VALLEDUPAR",0, new System.DateTime(2020,1,1));
            //Assert
            Assert.AreEqual(1, spy.CantidadLlamadas);
            Assert.AreEqual("cliente@bancoacme.com", spy.Email);
            Assert.AreEqual("El valor a consignar es incorrecto", response);
        }
        
        [Test]
        public void ConsignarMockTest()
        {
            var spy = new MailServerSpy();
            //Arrange
            var service = new ConsignarService(
                new UnitOfWorkFake(),
                new CuentaBancariaRepositoryStub(),
                spy);
            //Act
            var response=service.Consignar("1010","VALLEDUPAR",0, new System.DateTime(2020,1,1));
            //Assert
            Assert.AreEqual(1, spy.CantidadLlamadas);
            Assert.AreEqual("cliente@bancoacme.com", spy.Email);
            Assert.AreEqual("El valor a consignar es incorrecto", response);
        }
    }
}