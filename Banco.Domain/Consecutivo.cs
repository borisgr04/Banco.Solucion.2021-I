using Banco.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.Domain
{
    public class Consecutivo:Entity<int>, IAggregateRoot
    {
        public Consecutivo(int numeroInicial)
        {
            NumeroInicial = numeroInicial;
            Numero = numeroInicial;
            Logs = new List<ConsecutivoLog>();
        }
        public int NumeroInicial { get; private set; }
        public int Numero { get; private set; }
        [Timestamp]
        public byte[] RowVersion { get; private set; }
        public int Incrementar()
        {
            Numero++;
            Logs.Add(new ConsecutivoLog(Numero, RowVersion));
            return Numero;
        }
        public List<ConsecutivoLog> Logs { get; private set; }
    }

    public class ConsecutivoLog : Entity<int>
    {
        public ConsecutivoLog(int numero, byte[] rowVersion) 
        {
            Numero = numero;
            RowVersion = rowVersion;
            Fecha = DateTime.Now;
        }
        
        public int Numero { get; init; }
        public byte[] RowVersion { get; private set; }
        public DateTime Fecha { get; private set; }
    }
}
