using System;
using System.Collections.Generic;
using System.Text;

namespace Banco.Domain.Contracts
{
    public interface ICuentaRepository
    {
        CuentaBancaria Find(string numeroCuenta);
        List<CuentaBancaria> GetAll();
        void Add(CuentaBancaria cuentaBancaria);
        void Update(CuentaBancaria cuentaBancaria);
        void Delete(CuentaBancaria cuentaBancaria);
        
    }
}
