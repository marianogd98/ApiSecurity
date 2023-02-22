using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiPosRio.Models.DB
{
    public partial class Accion
    {
        public Accion()
        {
            RolAccions = new HashSet<RolAccion>();
            UsuarioAccions = new HashSet<UsuarioAccion>();
        }

        public int Id { get; set; }
        public int ModuloId { get; set; }
        public string Descripcion { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Modulo Modulo { get; set; }
        public virtual ICollection<RolAccion> RolAccions { get; set; }
        public virtual ICollection<UsuarioAccion> UsuarioAccions { get; set; }
    }
}
