using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiPosRio.Models.DB
{
    public partial class DesgloseDolar
    {
        public int Id { get; set; }
        public int Denominacion { get; set; }
        public int Cantidad { get; set; }
        public int ArqueoId { get; set; }
        public double Monto { get; set; }
        public int Moneda { get; set; }
    }
}
