using NUnit.Framework;
using System;

namespace Banco.Domain.Test
{
    public class Tests
    {
    

        /*
         H1: Como Usuario quiero realizar consignaciones a una cuenta de ahorro para salvaguardar el dinero.
        
        Criterio de Aceptaci�n:
        1.2 El valor a abono no puede ser menor o igual a 0.

        Dado El cliente tiene una cuenta de ahorro 
        N�mero 10001, Nombre �Cuenta ejemplo�, Saldo de 0
        Cuando Va a consignar un valor menor o igual a cero ///  errada
        Cuando Va a consignar un valor de cero /// 
        Entonces El sistema presentar� el mensaje. �El valor a consignar es incorrecto�
         */
        [Test]
        public void NoPuedeConsignarCeroPesos()
        {
            //ARRANGE //PREPARAR // DADO // GIVEN
            var cuentaAhorro = new CuentaAhorro("10001", "Cuenta ejemplo", "VALLEDUPAR", "client@clientebanco", DateTime.Now );
            // ACT // ACCION // CUANDO // WHEN
            var resultado = cuentaAhorro.Consignar(0, "VALLEDUPAR", new System.DateTime(2020, 2, 10));
            //ASSERT //AFIRMACION //ENTONCES //THEN
            Assert.AreEqual("El valor a consignar es incorrecto", resultado);
        }

        /*
         Escenario: Consignaci�n Inicial Correcta
            HU: Como Usuario quiero realizar consignaciones a una cuenta de ahorro para salvaguardar el 
            dinero.
            Criterio de Aceptaci�n:
            1.1 La consignaci�n inicial debe ser mayor o igual a 50 mil pesos
            1.3 El valor de la consignaci�n se le adicionar� al valor del saldo aumentar�
            Dado El cliente tiene una cuenta de ahorro 
            N�mero 10001, Nombre �Cuenta ejemplo�, Saldo de 0
            Cuando Va a consignar el valor inicial de 50 mil pesos 
            Entonces El sistema registrar� la consignaci�n
            AND presentar� el mensaje. �Su Nuevo Saldo es de $50.000,00 pesos m/c�.
         */
        [Test]
        public void PuedeConsignar50MilPesosInicialmente()
        {
            //ARRANGE //PREPARAR // DADO // GIVEN
            var cuentaAhorro = new CuentaAhorro("10001", "Cuenta ejemplo", "VALLEDUPAR", "cliente@bancoacme.com", DateTime.Now);
            // ACT // ACCION // CUANDO // WHEN
            var resultado = cuentaAhorro.Consignar(50000, "VALLEDUPAR", new System.DateTime(2021, 1, 30));
            //ASSERT //AFIRMACION //ENTONCES //THEN
            Assert.AreEqual("Su Nuevo Saldo es de $ 50.000,00 pesos m/c", resultado);
        }

       
    }
}