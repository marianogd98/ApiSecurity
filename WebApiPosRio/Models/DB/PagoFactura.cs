using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiPosRio.Models.DB
{
    public partial class PagoFactura
    {
        public int Id { get; set; }
        public int FacturaId { get; set; }
        public int FormaPagoId { get; set; }
        public int? BancoadquirienteId { get; set; }
        public string Lote { get; set; }
        public string NumeroTransaccion { get; set; }
        public double Monto { get; set; }
        public string Nombre { get; set; }
        public string TipoTarjeta { get; set; }
        public string NroAutorizacion { get; set; }
        public string Vposdata { get; set; }

        public virtual FormaPago FormaPago { get; set; }
    }
}
