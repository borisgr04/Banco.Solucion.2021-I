﻿// <auto-generated />
using System;
using Banco.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Banco.Infrastructure.Data.Migrations
{
    [DbContext(typeof(BancoContext))]
    [Migration("20210419131816_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("Banco.Domain.CuentaBancaria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Ciudad")
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombre")
                        .HasColumnType("TEXT");

                    b.Property<string>("Numero")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Saldo")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("CuentasBancarias");

                    b.HasDiscriminator<string>("Discriminator").HasValue("CuentaBancaria");
                });

            modelBuilder.Entity("Banco.Domain.MovimientoFinanciero", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CuentaBancariaId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("FechaMovimiento")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("ValorConsignacion")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("ValorRetiro")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CuentaBancariaId");

                    b.ToTable("MovimientoFinanciero");
                });

            modelBuilder.Entity("Banco.Domain.CuentaAhorro", b =>
                {
                    b.HasBaseType("Banco.Domain.CuentaBancaria");

                    b.HasDiscriminator().HasValue("CuentaAhorro");
                });

            modelBuilder.Entity("Banco.Domain.CuentaCorriente", b =>
                {
                    b.HasBaseType("Banco.Domain.CuentaBancaria");

                    b.HasDiscriminator().HasValue("CuentaCorriente");
                });

            modelBuilder.Entity("Banco.Domain.MovimientoFinanciero", b =>
                {
                    b.HasOne("Banco.Domain.CuentaBancaria", "CuentaBancaria")
                        .WithMany("Movimientos")
                        .HasForeignKey("CuentaBancariaId");

                    b.Navigation("CuentaBancaria");
                });

            modelBuilder.Entity("Banco.Domain.CuentaBancaria", b =>
                {
                    b.Navigation("Movimientos");
                });
#pragma warning restore 612, 618
        }
    }
}
