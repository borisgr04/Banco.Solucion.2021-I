﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Banco.Domain
{
    public class MovimientoFinanciero ///: Entity<int>
    {
        public MovimientoFinanciero()
        {
        }

        public MovimientoFinanciero(CuentaBancaria cuentaBancaria, decimal valorRetiro, decimal valorConsignacion, DateTime fechaMovimiento)
        {
            CuentaBancaria = cuentaBancaria;
            ValorRetiro = valorRetiro;
            ValorConsignacion = valorConsignacion;
            FechaMovimiento = fechaMovimiento;
        }

        public long Id { get; private set; }

        public CuentaBancaria CuentaBancaria { get; private set; }
        public decimal ValorRetiro { get; private set; }
        public decimal ValorConsignacion { get; private set; }
        public DateTime FechaMovimiento { get; private set; }
    }
}
