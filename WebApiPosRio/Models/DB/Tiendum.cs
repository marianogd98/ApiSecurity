using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiPosRio.Models.DB
{
    public partial class Tiendum
    {
        public Tiendum()
        {
            Areas = new HashSet<Area>();
            Arqueos = new HashSet<Arqueo>();
            Devolucions = new HashSet<Devolucion>();
        }

        public int Id { get; set; }
        public string CodigoTienda { get; set; }
        public string CodigoArea { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        public bool AplicaIva { get; set; }
        public string Urlapi { get; set; }
        public double Tasa { get; set; }
        public double DescuentoMax { get; set; }

        public virtual ICollection<Area> Areas { get; set; }
        public virtual ICollection<Arqueo> Arqueos { get; set; }
        public virtual ICollection<Devolucion> Devolucions { get; set; }
    }
}
