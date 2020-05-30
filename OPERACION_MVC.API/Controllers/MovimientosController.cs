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
    public class MovimientosController : ControllerBase
    {
        private readonly IMovimientoRepository _movimientoRepository;

        public MovimientosController(IMovimientoRepository movimientoRepository)
        {
            _movimientoRepository = movimientoRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movimiento>>> GetAll()
        {
            var movimientos = await _movimientoRepository.GetAll();
            return Ok(movimientos);
        }

        [HttpGet("{nro_cuenta}")]
        public async Task<ActionResult<IEnumerable<Movimiento>>> ObtenerCuenta(string nro_cuenta)
        {
            try
            {
                var movimiento = await _movimientoRepository.ObtenerMovimientosPorNroCuenta(nro_cuenta);
                if (movimiento == null)
                {
                    return NotFound("No Existe movimientos para esa cuenta");
                }

                return Ok(movimiento);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Data Base Failure");
            }
        }

        [HttpPost("abonar/")]
        public async Task<ActionResult> Abonar([FromBody] MovimientoRequestDTO movimientoRequestDTO)
        {
            try
            {
                await _movimientoRepository.Abonar(movimientoRequestDTO);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Data Base Failure");
            }
        }
        [HttpPost("retiro/")]
        public async Task<ActionResult> Retiro([FromBody] MovimientoRequestDTO movimientoRequestDTO)
        {
            try
            {
                await _movimientoRepository.Ritiro(movimientoRequestDTO);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Data Base Failure");
            }
        }

        [HttpPost("transferir/")]
        public async Task<ActionResult> Transferir([FromBody] TransferenciaDTO transferenciaDTO)
        {
            try
            {

                var realizarTransferencia = await _movimientoRepository.Transferir(transferenciaDTO);
                if (!realizarTransferencia)
                {
                    return StatusCode(StatusCodes.Status304NotModified, "No se pudo realizar la transferencia Saldo insuficiente");
                }

                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Data Base Failure");
            }
        }
    }
}
