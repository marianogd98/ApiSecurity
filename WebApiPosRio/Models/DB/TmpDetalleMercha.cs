using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiPosRio.Models.DB
{
    public partial class TmpDetalleMercha
    {
        public string Afiliacion { get; set; }
        public string Tarjeta { get; set; }
        public DateTime? Fecha1 { get; set; }
        public TimeSpan? Hora1 { get; set; }
        public DateTime? Fecha2 { get; set; }
        public TimeSpan? Hora2 { get; set; }
        public string Aprobacion { get; set; }
        public string Banco { get; set; }
        public string TipoTarjeta { get; set; }
        public string Lote { get; set; }
        public string Vtid { get; set; }
        public string Vtide { get; set; }
        public double? MontoBruto { get; set; }
        public double? Comision { get; set; }
        public double? Retencion { get; set; }
        public double? Neto { get; set; }
    }
}
