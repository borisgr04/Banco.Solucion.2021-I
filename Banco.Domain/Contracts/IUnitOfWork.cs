using System;
using System.Collections.Generic;
using System.Text;

namespace Banco.Domain.Contracts
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
