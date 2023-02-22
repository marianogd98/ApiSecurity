using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiPosRio.Models.DB
{
    public partial class Turnos
    {
        public int IdTurno { get; set; }
        public string Descripcion { get; set; }
        public int Ciclo { get; set; }
        public int Activo { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }

    }
}
