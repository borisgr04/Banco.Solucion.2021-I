using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.Domain.Test
{
    class EjemplosTestCase
    {
        [TestCase(1, 5, 6, TestName = "Suma correcta escenario 2")]
        [TestCase(1, 1, 2, TestName = "Suma correcta escenario 1")]
        [Test]
        public void PuedeSumar(int primeroNumero, int segundoNumero, int resultaEsperado)
        {
            var suma = primeroNumero + segundoNumero;
            Assert.AreEqual(resultaEsperado, suma);
        }

        [TestCase(1, 5, 7, TestName = "Suma incorrecta escenario 2")]
        [TestCase(1, 1, 3, TestName = "Suma incorrecta escenario 1")]
        [Test]
        public void NoPuedeSumar(int primeroNumero, int segundoNumero, int resultaEsperado)
        {
            var suma = primeroNumero + segundoNumero;
            Assert.AreNotEqual(resultaEsperado, suma);
        }

        [TestCase(12, 3, ExpectedResult = 4)]
        [TestCase(12, 2, ExpectedResult = 6)]
        [TestCase(12, 4, ExpectedResult = 3)]
        public int DivideTest(int n, int d)
        {
            return n / d;
        }

    }
}
