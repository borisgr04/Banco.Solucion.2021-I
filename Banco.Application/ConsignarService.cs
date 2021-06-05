﻿using Banco.Domain;
using Banco.Domain.Contracts;
using System;
using System.Globalization;

namespace Banco.Application
{
    /// <summary>
    /// Comando 
    /// </summary>
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

        public ConsignarResponse Consignar(ConsignarRequest request)
        {
            var cuenta = _cuentaRepository.FindFirstOrDefault(cuenta=>cuenta.Numero== request.NumeroCuenta);//infraestructura-datos
            if (cuenta == null) return new ConsignarResponse("la cuenta no existe");
            var response = cuenta.Consignar(request.Valor, request.Ciudad, request.FechaMovimiento);//domain
            _cuentaRepository.Update(cuenta);//proyectarse el cambio y registrarlo en la unidad de trabajo
            _unitOfWork.Commit();//infraestructura-datos
            var responseMail=_emailServer.Send("Se efectúo consignación", cuenta.Email);//infraestructura-system
           // if (responseMail != "Se envío el correo") 
           // {
           //      response += "- Hubo problemas enviando el correo";
           // }
            return new ConsignarResponse(response);
        }

    }
    public record ConsignarRequest(string NumeroCuenta, string Ciudad, decimal Valor, DateTime FechaMovimiento);
    public record ConsignarResponse 
    {
        public ConsignarResponse()
        {

        }

        public ConsignarResponse(string mensaje)
        {
            Mensaje = mensaje;
        }

        public string Mensaje { get; set; }
    }


}
