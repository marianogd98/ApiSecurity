using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiPosRio.Models.DB
{
    public partial class TmpMerchan
    {
        public long Id { get; set; }
        public string Afiliacion { get; set; }
        public string Terminal { get; set; }
        public int? Lote { get; set; }
        public DateTime? Fecha { get; set; }
        public DateTime? Hora { get; set; }
        public string Tipopago { get; set; }
        public double? Bruto { get; set; }
        public double? Comision { get; set; }
        public double? Retencion { get; set; }
        public double? Neto { get; set; }
        public string Banco { get; set; }
        public int? Actualizado { get; set; }
    }
}
