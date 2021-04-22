using Banco.Domain.Contracts;
using System;

namespace Banco.Application
{
    public class ConsignarService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICuentaBancariaRepository _cuentaRepository;
        private readonly IMailServer _emailServer;

        public ConsignarService(
           IUnitOfWork unitOfWork,
           ICuentaBancariaRepository cuentaRepository,
           IMailServer emailServer
       )
        {
            _unitOfWork = unitOfWork;
            _cuentaRepository = cuentaRepository;
            _emailServer = emailServer;
        }

        public string Consignar(string numeroCuenta, string ciudad, decimal valor, DateTime fechaMovimiento)
        {
            var cuenta = _cuentaRepository.FindFirstOrDefault(cuenta=>cuenta.Numero== numeroCuenta);//infraestructura-datos
            if (cuenta == null) return "la cuenta no existe";
            var response = cuenta.Consignar(valor, ciudad, fechaMovimiento);//domain
            _cuentaRepository.Update(cuenta);//proyectarse el cambio y registrarlo en la unidad de trabajo
            _unitOfWork.Commit();//infraestructura-datos
            var responseMail=_emailServer.Send("Se efectúo consignación", cuenta.Email);//infraestructura-system
            if (responseMail != "Se envío el correo") 
            {
                response += "- Hubo problemas enviando el correo";
            }
            return response;
        }

    }
}
