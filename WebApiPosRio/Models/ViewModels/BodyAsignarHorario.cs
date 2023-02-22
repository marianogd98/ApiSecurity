using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPosRio.Models.ViewModels
{
    public class BodyAsignarHorario
    {
        public int Cedula { get; set; }
        public int Turno { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int Activo { get; set; }
    }
}
