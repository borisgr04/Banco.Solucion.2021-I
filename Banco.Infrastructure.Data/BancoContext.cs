using Banco.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Banco.Infrastructure.Data
{
    public class BancoContext:DbContext
    {
        //public DbSet<CuentaBancaria> CuentasBancarias { get; set; }//equivale a Repositorios
        public DbSet<CuentaAhorro> CuentasAhorro { get; set; }
        public DbSet<CuentaCorriente> CuentasCorriente { get; set; }
        public DbSet<CuentaBancaria> CuentasBancarias { get; set; }

        //se debe quitar posteriomente para trabajarlo con inyeccion de dependencias. 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
         => optionsBuilder.UseSqlite(@"Data Source=C:\sqlite\bancoDataBase.db");
    }
}
