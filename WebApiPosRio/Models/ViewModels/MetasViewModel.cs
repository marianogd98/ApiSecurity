using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPosRio.Models.ViewModels
{
    public class PresupuestoViewModel
    {
        public string Fecha { get; set; }
        public string Tienda { get; set; }
        public double Presupuesto { get; set; }
    }

    public class MetaViewModel
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int TiendaId { get; set; }
        public double Presupuesto { get; set; }
    }

    public class DataMeta
    {
        public List<PresupuestoViewModel> data { get; set; }
        public int IdUser { get; set; }
    }

}
