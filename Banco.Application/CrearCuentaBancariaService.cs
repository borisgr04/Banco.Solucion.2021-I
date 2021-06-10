using Banco.Domain;
using Banco.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Banco.Application
{
    public class CrearCuentaBancariaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICuentaBancariaRepository _cuentaRepository;
        private readonly IMailServer _emailServer;

        public CrearCuentaBancariaService(
           IUnitOfWork unitOfWork,
           ICuentaBancariaRepository cuentaRepository,
           IMailServer emailServer
       )
        {
            _unitOfWork = unitOfWork;
            _cuentaRepository = cuentaRepository;
            _emailServer = emailServer;
        }
        public string CrearCuentaBancaria(CuentaBancariaRequest request)
        {
            
            

            CuentaBancaria cuenta = _cuentaRepository.FindFirstOrDefault(t => t.Numero == request.Numero);
            if (cuenta == null)
            {
                var consecutivo = _unitOfWork.ConsecutivoRepository.Find(1);
                consecutivo.Incrementar();
                //var numero = consecutivo.Numero.ToString();
                Thread.Sleep(1000);
                var numero = request.Numero;
                CuentaBancaria cuentaNueva =  TipoCuenta.CrearCuenta(
                                                request.TipoCuenta,
                                                numero,
                                                request.Nombre,
                                                request.Ciudad,
                                                request.Email,
                                                DateTime.Now
                                                );
                _cuentaRepository.Add(cuentaNueva);
                _unitOfWork.Commit();
                
                return $"Se creo con exito la cuenta {cuentaNueva.Numero}.";
            }
            else
            {
                return  $"El número de cuenta ya exite";
            }
        }

    }
    public class CuentaBancariaRequest
    {
        public string Nombre { get; set; }
        public string TipoCuenta { get; set; }
        public string Numero { get; set; }
        public string Ciudad { get; set; }
        public string Email { get; set; }

    }
    public static class TipoCuenta
    {
        public static CuentaBancaria CrearCuenta(string tipoCuenta, string numero, string nombre, string ciudad, string email, DateTime fecha)
        {
            if (tipoCuenta.Equals("Ahorro"))
            {
                return new CuentaAhorro(numero, nombre, ciudad, email, fecha);
            }
            return new CuentaCorriente(numero, nombre, ciudad, email, fecha);
        }
    }
}
