using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiPosRio.Models.DB
{
    public partial class DetalleArqueo
    {
        public int Id { get; set; }
        public int ArqueoId { get; set; }
        public double Monto { get; set; }
        public int FormaPagoId { get; set; }
        public int Tipo { get; set; }

        public virtual Arqueo Arqueo { get; set; }
    }
}
