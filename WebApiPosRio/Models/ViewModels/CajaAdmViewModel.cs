using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPosRio.Models.ViewModels
{
    public class CajaAdmViewModel
    {
        public int FacturaId { get; set; }
        public int UserId { get; set; }
    }

    public class DataViewModel
    {
        public int CampoId { get; set; }
        public int UserId { get; set; }
        public int Estatus { get; set; }
    }

    public class FiltroListadoFactura
    {
        public string NroControl { get; set; }
        public string NroFactura { get; set; }
        public string ZetaSerial { get; set; }
        public string NombreCajera { get; set; }
        public double Monto { get; set; }
        public string Cedula { get; set; }
        public string NroDocumento { get; set; }
        public int Estatus { get; set; }
        public string FechaI { get; set; }
        public string FechaF { get; set; }
        public int FormaPagoId { get; set; }
        public string CodigoCaja { get; set; }
        public int IdTurno { get; set; }
    }

    public class TesoreriaViewModel
    {
        public int IdTienda { get; set; }
        public int TiendaId { get; set; }
        public int IdCaja { get; set; }
        public string IdTurno { get; set; }
        public string FormaPagoId { get; set; }
        public string NombreCajera { get; set; }
        public string FechaI { get; set; }
        public string FechaF { get; set; }
        public int AgruparFp { get; set; }
    }
}
