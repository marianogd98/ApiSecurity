using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPosRio.Models.ViewModels
{
    public class ReportViewModel
    {
        public double Procesado { get; set; }
        public double ProcesadoBs { get; set; }
        public double Declarado { get; set; }
        public string Caja { get; set; }
        public int Turno { get; set; }
        public int FormaPagoId { get; set; }
        public string Descripcion { get; set; }
        public int EstatusArqueo { get; set; }
        public int EstatusTurno { get; set; }
        public int TiendaId { get; set; }
    }
}
