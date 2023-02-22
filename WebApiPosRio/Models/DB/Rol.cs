using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiPosRio.Models.DB
{
    public partial class Rol
    {
        public Rol()
        {
            RolAccions = new HashSet<RolAccion>();
            Usuarios = new HashSet<Usuario>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }
        public bool? Activo { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<RolAccion> RolAccions { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
