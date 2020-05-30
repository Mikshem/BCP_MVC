using System;
using System.Collections.Generic;

namespace OPERACION.CORE.Entities
{
    public partial class Movimiento
    {
        public string NroCuenta { get; set; }
        public DateTime Fecha { get; set; }
        public string Tipo { get; set; }
        public decimal? Importe { get; set; }

        public virtual Cuenta NroCuentaNavigation { get; set; }
    }
}
