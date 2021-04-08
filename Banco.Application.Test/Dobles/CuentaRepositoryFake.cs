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
        public void Add(CuentaBancaria cuentaBancaria)
        {
            throw new NotImplementedException();
        }

        public void Delete(CuentaBancaria cuentaBancaria)
        {
            throw new NotImplementedException();
        }

        public CuentaBancaria Find(string numeroCuenta)
        {
            return new CuentaAhorro("10001", "Cuenta ejemplo", "VALLEDUPAR");
        }

        public List<CuentaBancaria> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(CuentaBancaria cuentaBancaria)
        {
            throw new NotImplementedException();
        }
    }
}
