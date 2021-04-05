using Banco.Domain;
using Banco.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.Application.Test.Dobles
{
    class CuentaRepositoryFake : ICuentaRepository
    {
        public CuentaBancaria Find(string numeroCuenta)
        {
            return new CuentaAhorro("10001", "Cuenta ejemplo", "VALLEDUPAR");
        }
    }
}
