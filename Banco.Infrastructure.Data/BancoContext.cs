using Banco.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Banco.Infrastructure.Data
{
    public class BancoContext:DbContext
    {
        public DbSet<CuentaBancaria> CuentasBancarias { get; set; }//equivale a Repositorios
    }
}
