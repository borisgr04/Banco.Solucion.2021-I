using Banco.Application.Test.Dobles;
using NUnit.Framework;

namespace Banco.Application.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ConsignarTest()
        {
            //Arrange
            var service = new ConsignarService(
                new UnitOfWorkFake(),
                new CuentaRepositoryFake(),
                new MailServerFake());
            //Act
            var response=service.Consignar("1010","VALLEDUPAR",0);
            //Assert
            Assert.AreEqual("El valor a consignar es incorrecto", response);


        }
    }
}