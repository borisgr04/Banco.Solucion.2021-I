using Banco.Application;
using Banco.Domain;
using Banco.Domain.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banco.Infrastructure.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentaBancariaController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICuentaBancariaRepository _cuentaBancariaRepository;
        private readonly IMailServer _mailServer;
        public CuentaBancariaController
            (IUnitOfWork unitOfWork, 
            ICuentaBancariaRepository cuentaBancariaRepository,
            IMailServer mailServer)
        {
            
            _unitOfWork = unitOfWork;
            _cuentaBancariaRepository = cuentaBancariaRepository;
            _mailServer = mailServer;

            if (!cuentaBancariaRepository.GetAll().Any()) 
            {
                var cuenta = new CuentaAhorro("10001", "Cuenta ejemplo", "VALLEDUPAR", "cliente@bancoacme.com");
                cuentaBancariaRepository.Add(cuenta);
                unitOfWork.Commit();
            }
        }

        [HttpPost]
        public ActionResult<string> PostCrearCuenta(CuentaBancariaRequest request)
        {
            var service = new CrearCuentaBancariaService(_unitOfWork, _cuentaBancariaRepository, _mailServer);
            var response = service.CrearCuentaBancaria(request);
            return Ok(response);
        }

        [HttpPost("Consignacion")]
        public ActionResult<ConsignarResponse> PostConsignar(ConsignarRequest request)
        {
            var service = new ConsignarService(_unitOfWork, _cuentaBancariaRepository, _mailServer);
            var response = service.Consignar(request);
            return Ok(response);
        }
    }
}
