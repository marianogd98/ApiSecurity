using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPosRio.Models.ViewModels
{
    public class ParamsLfViewModel: FiltroPagedList
    {
        public int Turno { get; set; }
        public string Caja { get; set; }
        public double Tasa { get; set; }
        public string Fecha { get; set; }
        public string Tienda { get; set; }
        public string TiendaId { get; set; }
        public string NombreCajera { get; set; }
    }
}
