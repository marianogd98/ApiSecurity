using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiPosRio.Models.DB
{
    public partial class DesglosePunto
    {
        public int Id { get; set; }
        public int ArqueoId { get; set; }
        public int CuentaBancariaId { get; set; }
        public double Monto { get; set; }
        public int Tipo { get; set; }

        public virtual Arqueo Arqueo { get; set; }
        public virtual CuentasBancaria CuentaBancaria { get; set; }
    }
}
