using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiPosRio.Models.DB
{
    public partial class Factura
    {
        public Factura()
        {
            DetalleDevolucions = new HashSet<DetalleDevolucion>();
            DetalleFacturas = new HashSet<DetalleFactura>();
            Devolucions = new HashSet<Devolucion>();
        }

        public int Id { get; set; }
        public string CodigoArea { get; set; }
        public int ClienteId { get; set; }
        public string CodigoAfiliacion { get; set; }
        public int Estatus { get; set; }
        public string NumeroFactura { get; set; }
        public string NumeroControl { get; set; }
        public int CajaId { get; set; }
        public int PuntosGenerados { get; set; }
        public string GuiaLicor { get; set; }
        public double MontoBruto { get; set; }
        public double MontoNeto { get; set; }
        public double MontoIva { get; set; }
        public double MontoDescuento { get; set; }
        public double MontoPagado { get; set; }
        public int CajeraId { get; set; }
        public string SerialFiscal { get; set; }
        public DateTime Fecha { get; set; }
        public int TurnoId { get; set; }
        public double Tasa { get; set; }
        public int TiendaId { get; set; }

        public virtual Cliente Cliente { get; set; }
        public virtual Turno Turno { get; set; }
        public virtual ICollection<DetalleDevolucion> DetalleDevolucions { get; set; }
        public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; }
        public virtual ICollection<Devolucion> Devolucions { get; set; }
    }
}
