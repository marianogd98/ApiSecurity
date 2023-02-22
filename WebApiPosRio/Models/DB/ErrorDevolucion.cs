using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiPosRio.Models.DB
{
    public partial class ErrorDevolucion
    {
        public int Id { get; set; }
        public int? FacturaId { get; set; }
        public int? TiendaId { get; set; }
        public int? Cajaid { get; set; }
        public double? Monto { get; set; }
        public int? TurnoId { get; set; }
        public string SerialFiscal { get; set; }
        public int? UsuarioId { get; set; }
        public string FormaPago { get; set; }
        public string Productos { get; set; }
        public string NumeroDevolucion { get; set; }
    }
}
