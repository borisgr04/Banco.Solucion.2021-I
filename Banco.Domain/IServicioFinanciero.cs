using System;
using System.Collections.Generic;
using System.Text;

namespace Banco.Domain
{
    public interface IServicioFinanciero
    {

        string Nombre { get; }
        string Numero { get; }
        decimal Saldo { get; }
        string Ciudad { get; }
        string Email { get; }

        string Retirar(decimal valor, string ciudad, DateTime fechaMovimiento);
        string Consignar(decimal valor, string ciudad, DateTime fechaMovimiento);
        string Trasladar(IServicioFinanciero servicioFinanciero, decimal valor, string ciudad, DateTime fechaMovimiento);

    }
}
