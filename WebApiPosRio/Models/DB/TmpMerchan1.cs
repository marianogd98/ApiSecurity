using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiPosRio.Models.DB
{
    public partial class TmpMerchan1
    {
        public string Afiliacion { get; set; }
        public string Terminal { get; set; }
        public string Lote { get; set; }
        public string Fecha { get; set; }
        public string Hora { get; set; }
        public string Tipopago { get; set; }
        public string Bruto { get; set; }
        public string Comision { get; set; }
        public string Retencion { get; set; }
        public string Neto { get; set; }
        public string Banco { get; set; }
    }
}
