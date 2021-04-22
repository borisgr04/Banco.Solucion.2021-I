using Banco.Domain.Contracts;
using System;

namespace Banco.Infrastructure.Systems
{
    public class MailServer : IMailServer
    {
        public string Send(string message, string email)
        {
            //enviamos el correo electronico con el servidor determinado
            return "Se envío el correo";
        }
    }
}
