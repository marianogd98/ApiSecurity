using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPosRio.Models.ViewModels
{
    public class WalletViewModel
    {
        public int TiendaId { get; set; }
        public DateTime FechaIni { get; set; }
        public DateTime FechaFin { get; set; }
        public string Rif { get; set; }
        public int IdTurno { get; set; }
        public string CodigoCaja { get; set; }

        public double Saldo { get; set; }
        public double Pago { get; set; }
        public double Dep { get; set; }
        public string NombreCliente { get; set; }
        public string NombreCajera { get; set; }
        public int Estatus { get; set; }
    }
}
