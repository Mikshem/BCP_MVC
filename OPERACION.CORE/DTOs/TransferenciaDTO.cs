using System;
using System.Collections.Generic;
using System.Text;

namespace OPERACION.CORE.DTOs
{
    public class TransferenciaDTO
    {
        public string TransferirDeNroCuenta { get; set; }
        public string NroCuentaATransferir { get; set; }
        public decimal? Importe { get; set; }
    }
}
