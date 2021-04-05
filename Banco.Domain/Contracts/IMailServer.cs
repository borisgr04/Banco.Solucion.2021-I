using System;
using System.Collections.Generic;
using System.Text;

namespace Banco.Domain.Contracts
{
    public interface IMailServer
    {
        void Send(string v, string email);
    }
}
