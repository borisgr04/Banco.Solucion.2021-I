using Banco.Application.Test.Dobles;
using Banco.Domain;
using Banco.Domain.Contracts;
using Banco.Infrastructure.Data;
using Banco.Infrastructure.Data.ObjectMother;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System.Data.Common;

namespace Banco.Application.Test
{
    public class ConsignarServiceConMoqTest
    {
        private BancoContext _dbContext;
        private ConsignarService _consignarService;//SUT - Objeto bajo prueba
        
        [SetUp]
        public void Setup()
        {
            //Arrange
         
            var optionsSqlite = new DbContextOptionsBuilder<BancoContext>()
           .UseSqlite(SqlLiteDatabaseInMemory.CreateConnection())
           .Options;

            _dbContext = new BancoContext(optionsSqlite);
            _dbContext.Database.EnsureCreated();

         
        }
      
        [Test]
        public void NoPuedeConsignarTest()
        {

            //Arrange
            var cuentaAhorro = CuentaBancariaMother.CreateCuentaAhorros("1010");

            _dbContext.CuentasBancarias.Add(cuentaAhorro);
            _dbContext.SaveChanges();

            var mockEmailServer = new Mock<IMailServer>();
            mockEmailServer.Setup( emailServer   => 
                emailServer.Send(It.IsAny<string>(), It.IsAny<string>())
                ).Returns("Se envío el correo");

            _consignarService = new ConsignarService(
             new UnitOfWork(_dbContext),
             new CuentaBancariaRepository(_dbContext),
             mockEmailServer.Object
             );
   

            //Act
            var response= _consignarService.Consignar(new ConsignarRequest(cuentaAhorro.Numero, "VALLEDUPAR",0, new System.DateTime(2021,1,2)));
            //Assert
            mockEmailServer.Verify(x => x.Send(It.IsAny<string>(), cuentaAhorro.Email), Times.Once);
            Assert.AreEqual("El valor a consignar es incorrecto", response.Mensaje);
            
            //
            //Revertir
            _dbContext.CuentasBancarias.Remove(cuentaAhorro);
            _dbContext.SaveChanges();

        }

        [Test]
        public void PuedeConsignar50MilPesosInicialmente()
        {
            var cuentaAhorro = CuentaBancariaMother.CreateCuentaAhorros("1020");

            _dbContext.CuentasBancarias.Add(cuentaAhorro);
            _dbContext.SaveChanges();

            var mockEmailServer = new Mock<IMailServer>();
            mockEmailServer.Setup(emailServer =>
               emailServer.Send(It.IsAny<string>(), It.IsAny<string>())
                ).Returns("Se envío el correo");

            _consignarService = new ConsignarService(
             new UnitOfWork(_dbContext),
             new CuentaBancariaRepository(_dbContext),
             mockEmailServer.Object
             );

            
            //Act
            var response = _consignarService.Consignar(new ConsignarRequest(cuentaAhorro.Numero, "VALLEDUPAR", 50000, new System.DateTime(2021, 1, 2)));
            //Assert
            Assert.AreEqual("Su Nuevo Saldo es de $ 50.000,00 pesos m/c", response.Mensaje);
            
            mockEmailServer.Verify(x => x.Send(It.IsAny<string>(), cuentaAhorro.Email), Times.Once);
            

            //Revertir
            _dbContext.CuentasBancarias.Remove(cuentaAhorro);
            _dbContext.SaveChanges();
        }

        [Test]
        public void PuedeConsignar50MilPesosInicialmentePeroNoEnviaCorreo()
        {
            var cuentaAhorro = CuentaBancariaMother.CreateCuentaAhorros("1025");

            _dbContext.CuentasBancarias.Add(cuentaAhorro);
            _dbContext.SaveChanges();

            var mockEmailServer = new Mock<IMailServer>();
            mockEmailServer.Setup(emailServer =>
               emailServer.Send(It.IsAny<string>(), It.IsAny<string>())
                ).Returns("no se envío el correo");

            _consignarService = new ConsignarService(
             new UnitOfWork(_dbContext),
             new CuentaBancariaRepository(_dbContext),
             mockEmailServer.Object
             );


            //Act
            var response = _consignarService.Consignar(new ConsignarRequest(cuentaAhorro.Numero, "VALLEDUPAR", 50000, new System.DateTime(2021, 1, 2)));
            //Assert
            Assert.AreEqual("Su Nuevo Saldo es de $ 50.000,00 pesos m/c", response.Mensaje);

            mockEmailServer.Verify(x => x.Send(It.IsAny<string>(), cuentaAhorro.Email), Times.Once);


            //Revertir
            _dbContext.CuentasBancarias.Remove(cuentaAhorro);
            _dbContext.SaveChanges();
        }
    }
}
