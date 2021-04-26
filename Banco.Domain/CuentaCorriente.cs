using System;
using System.Collections.Generic;

namespace Banco.Domain
{
    public class CuentaCorriente : CuentaBancaria
    {
        public const decimal SOBREGIRO = -1000;

        public CuentaCorriente( string numero, string nombre, string ciudad, string email) : base( numero, nombre, ciudad, email)
        {
        }

        public override List<string> PuedeRetirar(decimal valor)
        {
            throw new NotImplementedException();
        }

        public override string Retirar(decimal valor, string ciudad, DateTime fechaMovimiento)
        {
            decimal nuevoSaldo = Saldo - valor;
            if (nuevoSaldo >= SOBREGIRO)
            {
                MovimientoFinanciero movimiento = new MovimientoFinanciero(this, valor, 0, fechaMovimiento);
                Saldo -= valor;
                this.Movimientos.Add(movimiento);
            }
            else
            {
                throw new CuentaCorrienteRetirarMaximoSobregiroException("No es posible realizar el Retiro, supera el valor de sobregiro permitido");
            }
            return "Se realizó el retiro satisfactoriamente";
        }
    }

    [Serializable]
    public class CuentaCorrienteRetirarMaximoSobregiroException : Exception
    {
        public CuentaCorrienteRetirarMaximoSobregiroException() { }
        public CuentaCorrienteRetirarMaximoSobregiroException(string message) : base(message) { }
        public CuentaCorrienteRetirarMaximoSobregiroException(string message, Exception inner) : base(message, inner) { }
        protected CuentaCorrienteRetirarMaximoSobregiroException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
