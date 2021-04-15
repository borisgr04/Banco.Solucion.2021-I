using Banco.Domain;
using Banco.Domain.Contracts;
using Banco.Infrastructure.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.Infrastructure.Data
{
    public class CuentaBancariaRepository : GenericRepository<CuentaBancaria>, ICuentaBancariaRepository
    {
        public CuentaBancariaRepository(IDbContext context) : base(context)
        {
        }
    }
}
