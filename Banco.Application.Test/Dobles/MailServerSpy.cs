using Banco.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.Application.Test.Dobles
{
    public class MailServerSpy : IMailServer
    {
        public void Send(string v, string email)
        {
            Console.WriteLine("Se enviar el email");
        }
    }
}
