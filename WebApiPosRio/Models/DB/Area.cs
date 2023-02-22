using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiPosRio.Models.DB
{
    public partial class Area
    {
        public Area()
        {
            ConfigBotons = new HashSet<ConfigBoton>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int TiendaId { get; set; }

        public virtual Tiendum Tienda { get; set; }
        public virtual ICollection<ConfigBoton> ConfigBotons { get; set; }
    }
}
