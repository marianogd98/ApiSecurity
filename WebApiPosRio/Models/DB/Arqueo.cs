using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiPosRio.Models.DB
{
    public partial class Arqueo
    {
        public Arqueo()
        {
            DesglosePuntos = new HashSet<DesglosePunto>();
            DetalleArqueos = new HashSet<DetalleArqueo>();
        }

        public int Id { get; set; }
        public int TurnoId { get; set; }
        public string Observacion { get; set; }
        public DateTime Fecha { get; set; }
        public int Estatus { get; set; }
        public int TiendaId { get; set; }

        public virtual Tiendum Tienda { get; set; }
        public virtual Turno Turno { get; set; }
        public virtual ICollection<DesglosePunto> DesglosePuntos { get; set; }
        public virtual ICollection<DetalleArqueo> DetalleArqueos { get; set; }
    }
}
