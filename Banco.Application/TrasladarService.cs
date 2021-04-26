using Banco.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Banco.Application
{
    /// <summary>
    /// Comando 
    /// </summary>
    public class TrasladarService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICuentaBancariaRepository _cuentaRepository;
        private readonly IMailServer _emailServer;

        public TrasladarService(
           IUnitOfWork unitOfWork,
           ICuentaBancariaRepository cuentaRepository,
           IMailServer emailServer
       )
        {
            _unitOfWork = unitOfWork;
            _cuentaRepository = cuentaRepository;
            _emailServer = emailServer;

        }

        public TrasladarResponse Handle(TrasladarRequest request)
        {
            List<string> errors = new List<string>();
            
            //fase de valiacion (validar que los datos venga completos) ejemplo valor >0, los numero de cuentas no pueden estar vacios, la ciudad no puede estar vacia
            var cuentaOrigen = _cuentaRepository.FindFirstOrDefault(cuenta => cuenta.Numero == request.NumeroCuentaOrigen);
            var cuentaDestino = _cuentaRepository.FindFirstOrDefault(cuenta => cuenta.Numero == request.NumeroCuentaDestino);
            //validaciones con las base de datos, que las cuentas existan
            
            //validar la logica del dominio
            var errorsPuedeRetirar = cuentaOrigen.PuedeRetirar(request.Valor);

            if (errorsPuedeRetirar.Any()) 
            {
                errors.Add("La cuenta origen no tiene la disponibilidad para efectuar el traslado");
            }

            //verifican final de errores
            if (errors.Any()) 
            {
                var errorsString = string.Join(",", errors);
                return new TrasladarResponse(errorsString);
            }


            var response=cuentaOrigen.Trasladar(cuentaDestino, request.Valor, request.Ciudad, request.FechaMovimiento);

            _unitOfWork.Commit();
            return new TrasladarResponse(response);
        }

    }
    public record TrasladarRequest(string NumeroCuentaOrigen, string NumeroCuentaDestino, string Ciudad, decimal Valor, DateTime FechaMovimiento);
    public record TrasladarResponse(string Mensaje);


}
