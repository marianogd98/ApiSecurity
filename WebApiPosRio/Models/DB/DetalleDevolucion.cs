using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiPosRio.Models.DB
{
    public partial class DetalleDevolucion
    {
        public int Id { get; set; }
        public int DevolucionId { get; set; }
        public int TiendaId { get; set; }
        public int ProductoId { get; set; }
        public double Cantidad { get; set; }
        public int FacturaId { get; set; }
        public double Total { get; set; }
        public DateTime Fecha { get; set; }
        public int TipoProducto { get; set; }

        public virtual Devolucion Devolucion { get; set; }
        public virtual Factura Factura { get; set; }
    }
}
