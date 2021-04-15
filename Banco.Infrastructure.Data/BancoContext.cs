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
     

        //se debe quitar posteriomente para trabajarlo con inyeccion de dependencias. 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
         => optionsBuilder.UseSqlite(@"Data Source=C:\sqlite\bancoDataBase.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<CuentaBancaria>()
            //    .Property(b => b.Nombre)
            //    .HasColumnName("cue_nom");
        }
    }
}
