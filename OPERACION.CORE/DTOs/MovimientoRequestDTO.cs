using System;
using System.Collections.Generic;
using System.Text;

namespace OPERACION.CORE.DTOs
{
    public class MovimientoRequestDTO
    {
        public string NroCuenta { get; set; }
        public decimal? Importe { get; set; }
    }
}
