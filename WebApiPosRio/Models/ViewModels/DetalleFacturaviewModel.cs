using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPosRio.Models.ViewModels
{
    public class DetalleFacturaviewModel
    {
        public string Factura { get; set; }
        public int CajaId { get; set; }
        public int ClienteId { get; set; }
        public string Fecha { get; set; }
        public double Tasa { get; set; }
        public int Estatus { get; set; }
    }
}
