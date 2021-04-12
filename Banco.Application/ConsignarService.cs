using Banco.Domain.Contracts;
using System;

namespace Banco.Application
{
    public class ConsignarService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICuentaRepository _cuentaRepository;
        private readonly IMailServer _emailServer;

        public ConsignarService(
           IUnitOfWork unitOfWork,
           ICuentaRepository cuentaRepository,
           IMailServer emailServer
       )
        {
            _unitOfWork = unitOfWork;
            _cuentaRepository = cuentaRepository;
            _emailServer = emailServer;
        }

        public string Consignar(string numeroCuenta, string ciudad, decimal valor, DateTime fechaMovimiento)
        {
            var cuenta = _cuentaRepository.Find(numeroCuenta);//infraestructura-datos
            var response = cuenta.Consignar(valor, ciudad, fechaMovimiento);//domain
            _cuentaRepository.Update(cuenta);//proyectarse el cambio y registrarlo en la unidad de trabajo
            _unitOfWork.Commit();//infraestructura-datos
            _emailServer.Send("Se efectúo consignacion", cuenta.Email);//infraestructura-system
            return response;
        }

    }
}
