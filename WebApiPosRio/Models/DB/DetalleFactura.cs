using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiPosRio.Models.DB
{
    public partial class DetalleFactura
    {
        public int Id { get; set; }
        public int FacturaId { get; set; }
        public int TiendaId { get; set; }
        public int ProductoId { get; set; }
        public int TipoProducto { get; set; }
        public double PrecioBolivar { get; set; }
        public double CostoBolivar { get; set; }
        public double Descuento { get; set; }
        public double CostoDolar { get; set; }
        public double PrecioDolar { get; set; }
        public double Iva { get; set; }
        public string Serial { get; set; }
        public double Cantidad { get; set; }
        public double Total { get; set; }
        public DateTime Fecha { get; set; }

        public virtual Factura Factura { get; set; }
    }
}
