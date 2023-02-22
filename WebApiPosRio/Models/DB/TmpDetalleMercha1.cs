using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiPosRio.Models.DB
{
    public partial class TmpDetalleMercha1
    {
        public string Afiliacion { get; set; }
        public string Tarjeta { get; set; }
        public string Fecha1 { get; set; }
        public string Hora1 { get; set; }
        public string Fecha2 { get; set; }
        public string Hora2 { get; set; }
        public string Aprobacion { get; set; }
        public string Banco { get; set; }
        public string TipoTarjeta { get; set; }
        public string Lote { get; set; }
        public string Vtid { get; set; }
        public string Vtide { get; set; }
        public string MontoBruto { get; set; }
        public string Comision { get; set; }
        public string Retencion { get; set; }
        public string Neto { get; set; }
    }
}
