using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiPosRio.Models.DB
{
    public partial class Caja
    {
        public Caja()
        {
            Devolucions = new HashSet<Devolucion>();
            Turnos = new HashSet<Turno>();
        }

        public int Id { get; set; }
        public int TiendaId { get; set; }
        public string CodigoCaja { get; set; }
        public int Consecutivo { get; set; }
        public string PuertoBalanza { get; set; }
        public string PuertoCodigoBarra { get; set; }
        public string PuertoImpresora { get; set; }
        public string SerialImpresora { get; set; }
        public string Vtid { get; set; }
        public int AreaId { get; set; }
        public bool AbrirGaveta { get; set; }
        public bool FacturaAlMayor { get; set; }
        public bool Estatus { get; set; }
        public int BancoId { get; set; }

        public virtual ICollection<Devolucion> Devolucions { get; set; }
        public virtual ICollection<Turno> Turnos { get; set; }
    }
}
