using Banco.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Banco.Domain
{
    public abstract class CuentaBancaria: Entity<int>, IServicioFinanciero, IAggregateRoot
    {
        protected CuentaBancaria( string numero, string nombre, string ciudad, string email)
        {
            Nombre = nombre;
            Numero = numero;
            Ciudad = ciudad;
            Email = email;
            Movimientos = new List<MovimientoFinanciero>();
        }
        public List<MovimientoFinanciero> Movimientos { get; private set; }
        public string Nombre { get; private set; }
        public string Numero { get; private set; }
        public string Ciudad { get; private set; }
        public string Email { get; private set; }
        public decimal Saldo { get; protected set; }
        public virtual string Consignar(decimal valor, string ciudad, DateTime fechaMovimiento)
        {
            if (valor <= 0)
            {
                return "El valor a consignar es incorrecto";
            }
            if (Saldo == 0 && valor >= 50000)
            {
                var movimiento = new MovimientoFinanciero(this, 0, valor, fechaMovimiento);
                Movimientos.Add(movimiento);
                Saldo += valor;
                return $"Su Nuevo Saldo es de ${valor:n2} pesos m/c";
            }
            throw new NotImplementedException();
        }

        public abstract string Retirar(decimal valor, string ciudad, DateTime fechaMovimiento);

        public override string ToString()
        {
            return ($"Su saldo disponible es {Saldo}.");
        }

        public string Trasladar(IServicioFinanciero servicioFinanciero, decimal valor, string ciudad, DateTime fechaMovimiento)
        {
            Retirar(valor,ciudad, fechaMovimiento);
            servicioFinanciero.Consignar(valor, ciudad, fechaMovimiento);
            return "Se realizó el traslado satisfactoriamente";
        }

       
    }
}
