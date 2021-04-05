using System;
using System.Collections.Generic;
using System.Text;

namespace Banco.Domain
{
    public abstract class CuentaBancaria
    {
        protected CuentaBancaria(string numero, string nombre, string ciudad, string email)
        {
            Numero = numero;
            Nombre = nombre;
            Ciudad = ciudad;
            Saldo = 0;
            Email = email;
        }

        public string Numero { get; private set; }
        public string Nombre { get; private set; }
        public string Ciudad { get; private set; }
        public decimal Saldo { get; protected set; }
        public string Email { get; set; }

        public abstract string Consignar(decimal valor, string ciudad);

        public abstract string Retirar(decimal valor, string ciudad);
        
    }
}
