using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPosRio.Models.ViewModels
{
    public class CajaViewModel
    {
        public int Id { get; set; }
        public int TiendaId { get; set; }
        public string TiendaNombre { get; set; }
        public string CodigoCaja { get; set; }
        public int Consecutivo { get; set; }
        public string PuertoBalanza { get; set; }
        public string PuertoCodigoBarra { get; set; }
        public string PuertoImpresora { get; set; }
        public string SerialImpresora { get; set; }
        public string Vtid { get; set; }
        public int AreaId { get; set; }
        public string AreaNombre { get; set; }
        public bool AbrirGaveta { get; set; }
        public bool FacturaAlMayor
        {
            get; set;
        }
        public bool Estatus { get; set; }
    }

    public class Cajafiltro
    {
        public string CodCaja { get; set; }
        public int CajaId { get; set; }
        public int AreaId { get; set; }
        public int TiendaId { get; set; }
        public bool Estatus { get; set; }
    }
}
