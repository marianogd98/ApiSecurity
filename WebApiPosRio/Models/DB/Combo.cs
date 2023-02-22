using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiPosRio.Models.DB
{
    public partial class Combo
    {
        public Combo()
        {
            ComboProductos = new HashSet<ComboProducto>();
        }

        public int Id { get; set; }
        public string CodigoCombo { get; set; }
        public string Descripcion { get; set; }
        public double Costo { get; set; }
        public double Precio { get; set; }
        public DateTime? FechaIni { get; set; }
        public DateTime? FechaFin { get; set; }
        public string Barra { get; set; }
        public bool Activo { get; set; }

        public virtual ICollection<ComboProducto> ComboProductos { get; set; }
    }
}
