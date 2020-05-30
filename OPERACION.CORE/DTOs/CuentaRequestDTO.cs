using System;
using System.Collections.Generic;
using System.Text;

namespace OPERACION.CORE.DTOs
{
    public class CuentaRequestDTO
    {
        public string NroCuenta { get; set; }
        public string Moneda { get; set; }
        public string Nombre { get; set; }
        public decimal? Saldo { get; set; }
     
    }
}
