using System;

namespace Banco.Domain
{
    public class CuentaAhorro : CuentaBancaria
    {
        public CuentaAhorro(string numero, string nombre, string ciudad) : base(numero, nombre, ciudad)
        {
        }

        public override string Consignar(decimal valor, string ciudad)
        {
            if (valor <= 0) 
            {
                return "El valor a consignar es incorrecto";
            }
            if (Saldo == 0 && valor >= 50000) 
            {
                Saldo += valor;
                return $"Su Nuevo Saldo es de ${valor:n2} pesos m/c";
            }
            throw new NotImplementedException();
        }

        public override string Retirar(decimal valor, string ciudad)
        {
            throw new NotImplementedException();
        }


    }
}
