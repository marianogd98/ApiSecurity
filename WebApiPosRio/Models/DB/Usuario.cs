using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiPosRio.Models.DB
{
    public partial class Usuario
    {
        public Usuario()
        {
            Devolucions = new HashSet<Devolucion>();
            Turnos = new HashSet<Turno>();
            UsuarioAccions = new HashSet<UsuarioAccion>();
        }

        public int Id { get; set; }
        public string Nick { get; set; }
        public string Password { get; set; }
        public int Tipo { get; set; }
        public string RecordarToken { get; set; }
        public int Estatus { get; set; }
        public int RolId { get; set; }
        public int TiendaId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Cedula { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }

        public virtual Rol Rol { get; set; }
        public virtual ICollection<Devolucion> Devolucions { get; set; }
        public virtual ICollection<Turno> Turnos { get; set; }
        public virtual ICollection<UsuarioAccion> UsuarioAccions { get; set; }
    }
}
