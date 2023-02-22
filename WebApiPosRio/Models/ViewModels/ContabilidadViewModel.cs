using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPosRio.Models.ViewModels
{
    public class ContabilidadViewModel
    {
    }

    public class ResumenCajaFiltro
    {
        public string TiendaId { get; set; }
        public string Fecha { get; set; }
        public int FormaPagoId { get; set; }
        public int EstatusDepo { get; set; }
        public int EstatusTurno { get; set; }
        public int IdCaja { get; set; }
        public string Turno { get; set; }
        public string Caja { get; set; }
        public int Ordenar { get; set; }
    }
}
