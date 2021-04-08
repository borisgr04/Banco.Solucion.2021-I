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

        //se debe quitar posteriomente para trabajarlo con inyeccion de dependencias. 
        protected override void OnConfiguring(DbContextOptionsBuilder options)
         => options.UseSqlite(@"Data Source=C:\sqlite\blogging.db");
    }
}
