using System;
using System.Collections.Generic;
using System.Text;

namespace Banco.Domain.Contracts
{
    public interface ICuentaRepository
    {
        CuentaBancaria Find(string numeroCuenta);
    }
}
