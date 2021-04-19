using Banco.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.Infrastructure.Data.ObjectMother
{
    //Se puede mejorar con fluent api (desarrollada por nosotros)
    public static class CuentaBancariaMother
    {

        public static CuentaBancaria CreateCuentaAhorros(string numeroCuenta) 
        {
             return new CuentaAhorro(numeroCuenta, "Cuenta ejemplo", "VALLEDUPAR", "cliente@bancoacme.com"); ;
        }
    }
}
