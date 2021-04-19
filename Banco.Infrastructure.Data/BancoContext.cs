using Banco.Domain;
using Banco.Infrastructure.Data.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Banco.Infrastructure.Data
{
    public class BancoContext: DbContextBase
    {
        public BancoContext() :base()
        {
        
        }
        public BancoContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<CuentaBancaria> CuentasBancarias { get; set; }//equivale a Repositorios
        public DbSet<CuentaAhorro> CuentasAhorro { get; set; }
        public DbSet<CuentaCorriente> CuentasCorriente { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CuentaBancaria>().HasKey(c => c.Id);
            modelBuilder.Entity<MovimientoFinanciero>().HasKey(c => c.Id);
        }
    }
}
