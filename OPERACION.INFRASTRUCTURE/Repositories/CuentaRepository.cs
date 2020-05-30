using Microsoft.EntityFrameworkCore;
using OPERACION.CORE.Common;
using OPERACION.CORE.DTOs;
using OPERACION.CORE.Entities;
using OPERACION.CORE.Interfaces;
using OPERACION.INFRASTRUCTURE.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OPERACION.INFRASTRUCTURE.Repositories
{
    public class CuentaRepository : ICuentaRepository
    {
        private readonly BD_TRANSACCIONESContext _db;

        public CuentaRepository(BD_TRANSACCIONESContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Cuenta>> GetAll()
        {
            return await _db.Cuenta.ToListAsync();
        }

        public async Task<Cuenta> GetCuenta(string nrocuenta)
        {
            var cuenta = await _db.Cuenta.FindAsync(nrocuenta);
            return cuenta;
        }

        public void CrearCuenta(CuentaRequestDTO cuentaRequestDTO)
        {
            string tipoCuenta = string.Empty;

            if(cuentaRequestDTO.NroCuenta.Length == 13)
            {
                tipoCuenta = TipoCuenta.CTE.ToString();
            }
            if (cuentaRequestDTO.NroCuenta.Length == 14)
            {
                tipoCuenta = TipoCuenta.AHO.ToString();
            }

            var cuenta = new Cuenta
            {
                NroCuenta = cuentaRequestDTO.NroCuenta,
                Tipo = tipoCuenta,
                Moneda = cuentaRequestDTO.Moneda,
                Nombre= cuentaRequestDTO.Nombre,
                Saldo= cuentaRequestDTO.Saldo
            };

            _db.Cuenta.Add(cuenta);
            _db.SaveChanges();
        }

    }
}
