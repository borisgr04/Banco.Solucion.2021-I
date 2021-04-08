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
        public void Commit()
        {
            Console.WriteLine("Se confirman cambios en la base de datos");
        }

      
    }
}
