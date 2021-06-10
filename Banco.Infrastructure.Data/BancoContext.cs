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
            modelBuilder.Entity<CuentaBancaria>().HasAlternateKey(a => a.Numero);
            modelBuilder.Entity<Consecutivo>().HasKey(c => c.Id);
            modelBuilder.Entity<ConsecutivoLog>().HasKey(c => c.Id);
            modelBuilder.Entity<Consecutivo>().HasData(
               new 
               {
                   Id=1,
                   Numero=1,
                   NumeroInicial = 1
               }
            );
            //modelBuilder.Entity<Consecutivo>().Property(p => p.Numero).IsConcurrencyToken();
            modelBuilder.Entity<Consecutivo>().Property(p => p.RowVersion).IsRowVersion();
        }
    }
}
