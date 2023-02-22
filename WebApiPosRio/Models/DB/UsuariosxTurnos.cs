using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiPosRio.Models.DB
{
    public partial class UsuariosxTurnos
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public int IdTurno { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int Activo { get; set; }

    }
}
