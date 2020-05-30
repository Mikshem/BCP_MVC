using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPERACION.CORE.DTOs;
using OPERACION.CORE.Entities;
using OPERACION.CORE.Interfaces;

namespace OPERACION_MVC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentasController : ControllerBase
    {
        private readonly ICuentaRepository _cuentaRepository;

        public CuentasController(ICuentaRepository cuentaRepository)
        {
            _cuentaRepository = cuentaRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cuenta>>> ObtenerCuentas()
        {
            var cuentas= await _cuentaRepository.GetAll();
            return Ok(cuentas);
        }

        [HttpGet("{nro_cuenta}", Name = "obtenerCuenta")]
        public async Task<ActionResult<Cuenta>> ObtenerCuenta(string nro_cuenta)
        {
            try
            {
                var cuenta = await _cuentaRepository.GetCuenta(nro_cuenta);
                if(cuenta == null)
                {
                    return NotFound("No existe la cuenta");
                }

                return Ok(cuenta);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Data Base Failure");
            }
        }

        [HttpPost]
        public ActionResult RegistrarCuenta([FromBody] CuentaRequestDTO cuentaRequestDTO)
        {
            try
            {
                _cuentaRepository.CrearCuenta(cuentaRequestDTO);

                return new CreatedAtRouteResult("obtenerCuenta", new { nro_cuenta = cuentaRequestDTO.NroCuenta }, cuentaRequestDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


    }
}
