using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiPosRio.Models.DB
{
    public partial class ErrorFactura
    {
        public int Id { get; set; }
        public string NumeroFactura { get; set; }
        public int ClienteId { get; set; }
        public int CajaId { get; set; }
        public double MontoBruto { get; set; }
        public double MontoNeto { get; set; }
        public double MontoIva { get; set; }
        public double MontoDescuento { get; set; }
        public double MontoPagado { get; set; }
        public int CajeraId { get; set; }
        public int CodigoTurno { get; set; }
        public double Tasa { get; set; }
        public int TiendaId { get; set; }
        public string Serialfilcal { get; set; }
        public string Productos { get; set; }
        public int NumeroError { get; set; }
        public string MensajeError { get; set; }
        public DateTime Fecha { get; set; }
    }
}
