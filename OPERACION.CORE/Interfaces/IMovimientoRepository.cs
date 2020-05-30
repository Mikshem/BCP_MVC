using OPERACION.CORE.DTOs;
using OPERACION.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OPERACION.CORE.Interfaces
{
    public interface IMovimientoRepository
    {
        Task<IEnumerable<Movimiento>> GetAll();
        Task Abonar(MovimientoRequestDTO movimientoRequestDTO);
        Task Ritiro(MovimientoRequestDTO movimientoRequestDTO);
        Task<bool> Transferir(TransferenciaDTO transferenciaDTO);
        Task<IEnumerable<Movimiento>> ObtenerMovimientosPorNroCuenta(string nrocuenta);
    }
}
