using System;
using System.Collections.Generic;
using System.Linq;

namespace Banco.Domain
{
    public class CuentaAhorro : CuentaBancaria
    {

        public const decimal TOPERETIRO = 1000;

        public CuentaAhorro(string numero, string nombre, string ciudad, string email, DateTime fecha) : base(numero, nombre, ciudad, email, fecha)
        {
        }

        public override string Retirar(decimal valor, string ciudad, DateTime fechaMovimiento)
        {
            if (PuedeRetirar(valor).Any()) 
            {
                throw new CuentaAhorroTopeDeRetiroException("No es posible realizar el Retiro, Supera el tope mínimo permitido de retiro");
            }
            Saldo -= valor;
            var retiro = new MovimientoFinanciero(this, valor,0, fechaMovimiento);
            this.Movimientos.Add(retiro);
            return "Se realizó  el retiro satisfactoriamente";
        }

        public override List<string> PuedeRetirar(decimal valor) 
        {
            List<string> errors = new List<string>();
            decimal nuevoSaldo = Saldo - valor;
            if (nuevoSaldo <= TOPERETIRO) 
            {
                errors.Add("No es posible realizar el Retiro, Supera el tope mínimo permitido de retiro");
            }
            return errors;
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
