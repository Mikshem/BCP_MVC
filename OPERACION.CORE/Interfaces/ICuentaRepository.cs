using OPERACION.CORE.DTOs;
using OPERACION.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OPERACION.CORE.Interfaces
{
    public interface ICuentaRepository
    {
        Task<IEnumerable<Cuenta>> GetAll();
        void CrearCuenta(CuentaRequestDTO cuentaRequestDTO);
        Task<Cuenta> GetCuenta(string nrocuenta);
    }


}
