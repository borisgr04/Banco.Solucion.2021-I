using Banco.Domain;
using Banco.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Banco.Infrastructure.Data
{
    public class CuentaRepository : ICuentaRepository
    {
        private readonly BancoContext _context;
        public CuentaRepository(BancoContext context)
        {
            _context = context;
        }
        public void Add(CuentaBancaria cuentaBancaria)
        {
            _context.CuentasBancarias.Add(cuentaBancaria);
        }

        public void Delete(CuentaBancaria cuentaBancaria)
        {
            _context.CuentasBancarias.Remove(cuentaBancaria);
        }

        public CuentaBancaria Find(string numeroCuenta)
        {
            var todas=_context.CuentasBancarias.ToList();
            var cuenta=_context.CuentasBancarias.FirstOrDefault(cuenta=> cuenta.Numero==numeroCuenta);
            return cuenta;
        }

        public List<CuentaBancaria> GetAll()
        {
            return _context.CuentasBancarias.ToList();
        }

        public void Update(CuentaBancaria cuentaBancaria)
        {
            _context.CuentasBancarias.Update(cuentaBancaria);
        }
    }
}
