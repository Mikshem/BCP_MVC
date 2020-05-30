using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OPERACION.CORE.Common;
using OPERACION.CORE.DTOs;
using OPERACION.CORE.Entities;
using OPERACION.CORE.Interfaces;
using OPERACION.INFRASTRUCTURE.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPERACION.INFRASTRUCTURE.Repositories
{
    public class MovimientoRepository: IMovimientoRepository
    {
        private readonly BD_TRANSACCIONESContext _db;
        private readonly StoreProcedureRepository _spRepository;

        public MovimientoRepository(BD_TRANSACCIONESContext db, StoreProcedureRepository spRepository)
        {
            _db = db;
            _spRepository = spRepository;
        }

        public async Task Abonar(MovimientoRequestDTO movimientoRequestDTO)
        {
            var movimiento = new Movimiento
            {
                NroCuenta = movimientoRequestDTO.NroCuenta,
                Fecha = DateTime.UtcNow,
                Tipo = TipoMovimiento.A.ToString(),
                Importe = movimientoRequestDTO.Importe, 
            };

            _db.Movimiento.Add(movimiento);
            _db.SaveChanges();
            await _spRepository.AbonarSaldo(movimientoRequestDTO.Importe, movimientoRequestDTO.NroCuenta);


        }

        public async Task<IEnumerable<Movimiento>> GetAll()
        {
            return await _db.Movimiento.ToListAsync();
        }

        public async Task<IEnumerable<Movimiento>> ObtenerMovimientosPorNroCuenta(string nrocuenta)
        {
            var movimiento = await _db.Movimiento.Where(x => x.NroCuenta == nrocuenta).ToListAsync();
            return movimiento;
        }

        public async Task Ritiro(MovimientoRequestDTO movimientoRequestDTO)
        {
            var movimiento = new Movimiento
            {
                NroCuenta = movimientoRequestDTO.NroCuenta,
                Fecha = DateTime.UtcNow,
                Tipo = TipoMovimiento.D.ToString(),
                Importe = movimientoRequestDTO.Importe,
            };

            _db.Movimiento.Add(movimiento);
            _db.SaveChanges();
            await _spRepository.DebitarSaldo(movimientoRequestDTO.Importe, movimientoRequestDTO.NroCuenta);
        }

        public async Task<bool> Transferir(TransferenciaDTO transferenciaDTO)
        {
            if (await ObtenerSaldo(transferenciaDTO))
            {
                var DebitarA = new Movimiento
                {
                    NroCuenta = transferenciaDTO.TransferirDeNroCuenta,
                    Fecha = DateTime.UtcNow,
                    Tipo = TipoMovimiento.D.ToString(),
                    Importe = transferenciaDTO.Importe,
                };
                _db.Movimiento.Add(DebitarA);
                await _spRepository.DebitarSaldo(transferenciaDTO.Importe, transferenciaDTO.TransferirDeNroCuenta);

                var AbonarA = new Movimiento
                {
                    NroCuenta = transferenciaDTO.NroCuentaATransferir,
                    Fecha = DateTime.UtcNow,
                    Tipo = TipoMovimiento.A.ToString(),
                    Importe = transferenciaDTO.Importe,
                };
                _db.Movimiento.Add(AbonarA);
                await _db.SaveChangesAsync();
                await _spRepository.AbonarSaldo(transferenciaDTO.Importe, transferenciaDTO.NroCuentaATransferir);

                return true;
            }

            return false;
           
        }

        private async Task<bool> ObtenerSaldo(TransferenciaDTO transferenciaDTO)
        {
            var cuenta =await _db.Cuenta.FirstOrDefaultAsync(x => x.NroCuenta == transferenciaDTO.TransferirDeNroCuenta);
            decimal? saldo = cuenta.Saldo;

            if (saldo > transferenciaDTO.Importe)
            {
                return true;
            }
            else return false;
        }
    }
}
