using Banco.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Banco.Domain
{
    public class Cliente:Entity<string>, IAggregateRoot
    {
        public string Nombre { get; private set; }
        public string Apellido { get; private set; }
    }
}
