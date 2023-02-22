using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiPosRio.Models.DB
{
    public partial class Devolucion
    {
        public Devolucion()
        {
            DetalleDevolucions = new HashSet<DetalleDevolucion>();
        }

        public int Id { get; set; }
        public int FacturaId { get; set; }
        public int Tipo { get; set; }
        public double Monto { get; set; }
        public int TurnoId { get; set; }
        public string SerialFiscal { get; set; }
        public int UsuarioId { get; set; }
        public string FormaPago { get; set; }
        public int TiendaId { get; set; }
        public int CajaId { get; set; }
        public DateTime Fecha { get; set; }
        public string NumeroDevolucion { get; set; }

        public virtual Caja Caja { get; set; }
        public virtual Factura Factura { get; set; }
        public virtual Tiendum Tienda { get; set; }
        public virtual Turno Turno { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<DetalleDevolucion> DetalleDevolucions { get; set; }
    }
}
