using Banco.Domain.Base;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Banco.Domain
{
    public abstract class CuentaBancaria: Entity<int>, IServicioFinanciero, IAggregateRoot
    {
        protected CuentaBancaria(string numero, string nombre, string ciudad, string email, DateTime fecha)
        {
            Nombre = nombre;
            Numero = numero;
            Ciudad = ciudad;
            Email = email;
            Fecha = fecha;
            Movimientos = new List<MovimientoFinanciero>();
        }
        public List<MovimientoFinanciero> Movimientos { get; private set; }
        public string Nombre { get; private set; }
        public string Numero { get; private set; }
        public string Ciudad { get; private set; }
        public string Email { get; private set; }
        public decimal Saldo { get; protected set; }
        public DateTime Fecha { get; protected set; }
        public virtual string Consignar(decimal valor, string ciudad, DateTime fechaMovimiento)
        {
            if (valor <= 0)
            {
                return "El valor a consignar es incorrecto";
            }
            MovimientoFinanciero movimiento;
            if (Saldo == 0 && valor >= 50000)
            {
                movimiento= new MovimientoFinanciero(this, 0, valor, fechaMovimiento);
                Movimientos.Add(movimiento);
                Saldo += valor;
                return $"Su Nuevo Saldo es de {valor.ToString("C2", new CultureInfo("es-CO"))} pesos m/c";
            }
            movimiento = new MovimientoFinanciero(this, 0, valor, fechaMovimiento);
            Movimientos.Add(movimiento);
            Saldo += valor;
            return $"Su Nuevo Saldo es de {Saldo.ToString("C2", new CultureInfo("es-CO"))} pesos m/c";
        }

        public abstract string Retirar(decimal valor, string ciudad, DateTime fechaMovimiento);
        public abstract List<string> PuedeRetirar(decimal valor);

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
