using Banco.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.Application.Test.Dobles
{
    class UnitOfWorkFake : IUnitOfWork
    {
        public IConsecutivoRepository ConsecutivoRepository => throw new NotImplementedException();

        public void Commit()
        {
            Console.WriteLine("Se confirman cambios en la base de datos");
        }

      
    }
}
