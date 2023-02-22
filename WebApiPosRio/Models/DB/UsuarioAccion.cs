using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiPosRio.Models.DB
{
    public partial class UsuarioAccion
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int AccionId { get; set; }

        public virtual Accion Accion { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
