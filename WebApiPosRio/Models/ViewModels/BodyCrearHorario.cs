using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPosRio.Models.ViewModels
{
    public class BodyCrearHorario
    {
        public string Descripcion { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
        public int Ciclo { get; set; }
        public int Activo { get; set; }

    }
}
