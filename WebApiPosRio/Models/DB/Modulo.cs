using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiPosRio.Models.DB
{
    public partial class Modulo
    {
        public Modulo()
        {
            Accions = new HashSet<Accion>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<Accion> Accions { get; set; }
    }
}
