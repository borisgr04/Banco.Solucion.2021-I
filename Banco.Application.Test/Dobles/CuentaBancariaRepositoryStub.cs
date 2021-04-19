using Banco.Domain;
using Banco.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Banco.Application.Test.Dobles
{
    class CuentaBancariaRepositoryStub : ICuentaBancariaRepository
    {
        public void Add(CuentaBancaria entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(List<CuentaBancaria> entities)
        {
            throw new NotImplementedException();
        }

        public void Delete(CuentaBancaria entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteRange(List<CuentaBancaria> entities)
        {
            throw new NotImplementedException();
        }

        public CuentaBancaria Find(object id)
        {
            return new CuentaAhorro("10001", "Cuenta ejemplo", "VALLEDUPAR", "cliente@bancoacme.com");
        }

        public IEnumerable<CuentaBancaria> FindBy(Expression<Func<CuentaBancaria, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CuentaBancaria> FindBy(Expression<Func<CuentaBancaria, bool>> filter = null, Func<IQueryable<CuentaBancaria>, IOrderedQueryable<CuentaBancaria>> orderBy = null, string includeProperties = "")
        {
            throw new NotImplementedException();
        }

        public CuentaBancaria FindFirstOrDefault(Expression<Func<CuentaBancaria, bool>> predicate)
        {
            return new CuentaAhorro("10001", "Cuenta ejemplo", "VALLEDUPAR", "cliente@bancoacme.com");
        }

        public IEnumerable<CuentaBancaria> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(CuentaBancaria entity)
        {
            Console.WriteLine("Se actualiza la entidad");
        }
    }
}
