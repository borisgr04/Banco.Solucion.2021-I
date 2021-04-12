using System;

namespace Banco.Domain
{
    public class CuentaAhorro : CuentaBancaria
    {

        public const decimal TOPERETIRO = 1000;

        public CuentaAhorro(string numero, string nombre, string ciudad, string email) : base(numero, nombre, ciudad, email)
        {
        }

        public override string Retirar(decimal valor, string ciudad, DateTime fechaMovimiento)
        {
            decimal nuevoSaldo = Saldo - valor;
            if (nuevoSaldo > TOPERETIRO)
            {
                MovimientoFinanciero retiro = new MovimientoFinanciero(this, valor,0, fechaMovimiento);
                Saldo -= valor;
                this.Movimientos.Add(retiro);
            }
            else
            {
                throw new CuentaAhorroTopeDeRetiroException("No es posible realizar el Retiro, Supera el tope mínimo permitido de retiro");
            }
            return "Se realizó  el retiro satisfactoriamente";
        }
    }


    [Serializable]
    public class CuentaAhorroTopeDeRetiroException : Exception
    {
        public CuentaAhorroTopeDeRetiroException() { }
        public CuentaAhorroTopeDeRetiroException(string message) : base(message) { }
        public CuentaAhorroTopeDeRetiroException(string message, Exception inner) : base(message, inner) { }
        protected CuentaAhorroTopeDeRetiroException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
