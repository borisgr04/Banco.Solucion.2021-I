﻿// <auto-generated />
using System;
using Banco.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Banco.Infrastructure.Data.Migrations
{
    [DbContext(typeof(BancoContext))]
    [Migration("20210605193024_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Banco.Domain.Consecutivo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.Property<int>("NumeroInicial")
                        .HasColumnType("int");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.ToTable("Consecutivo");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Numero = 1,
                            NumeroInicial = 1
                        });
                });

            modelBuilder.Entity("Banco.Domain.ConsecutivoLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ConsecutivoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.Property<byte[]>("RowVersion")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.HasIndex("ConsecutivoId");

                    b.ToTable("ConsecutivoLog");
                });

            modelBuilder.Entity("Banco.Domain.CuentaBancaria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Ciudad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Saldo")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasAlternateKey("Numero");

                    b.ToTable("CuentasBancarias");

                    b.HasDiscriminator<string>("Discriminator").HasValue("CuentaBancaria");
                });

            modelBuilder.Entity("Banco.Domain.MovimientoFinanciero", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CuentaBancariaId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaMovimiento")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("ValorConsignacion")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ValorRetiro")
                        .HasColumnType("decimal(18,2)");

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

            modelBuilder.Entity("Banco.Domain.ConsecutivoLog", b =>
                {
                    b.HasOne("Banco.Domain.Consecutivo", null)
                        .WithMany("Logs")
                        .HasForeignKey("ConsecutivoId");
                });

            modelBuilder.Entity("Banco.Domain.MovimientoFinanciero", b =>
                {
                    b.HasOne("Banco.Domain.CuentaBancaria", "CuentaBancaria")
                        .WithMany("Movimientos")
                        .HasForeignKey("CuentaBancariaId");

                    b.Navigation("CuentaBancaria");
                });

            modelBuilder.Entity("Banco.Domain.Consecutivo", b =>
                {
                    b.Navigation("Logs");
                });

            modelBuilder.Entity("Banco.Domain.CuentaBancaria", b =>
                {
                    b.Navigation("Movimientos");
                });
#pragma warning restore 612, 618
        }
    }
}