using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OPERACION.CORE.Entities
{
    public partial class Cuenta
    {
        public Cuenta()
        {
            Movimiento = new HashSet<Movimiento>();
        }

        [MaxLength(14, ErrorMessage ="Maximo 14 digitos")]
        public string NroCuenta { get; set; }
        public string Tipo { get; set; }
        public string Moneda { get; set; }
        public string Nombre { get; set; }
        public decimal? Saldo { get; set; }

        public virtual ICollection<Movimiento> Movimiento { get; set; }
    }
}
