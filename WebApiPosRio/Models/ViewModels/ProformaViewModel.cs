using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPosRio.Models.ViewModels
{
    public class ProformaViewModel
    {
        public int Id { get; set; }
        public int CantidadContainer { get; set; }
        public string NumeroProforma { get; set; }
        public string Descripcion { get; set; }
        public string CodigoProveedor { get; set; }
        public string FechaLlegada { get; set; }
        public string FechaEmbarque { get; set; }
        public string FechaProforma { get; set; }
        public double Monto { get; set; }
        public int Estatus { get; set; }
        public int MonedaId { get; set; }
        public int UserId { get; set; }
        public string DescripcionProduccion { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public List<PagoViewModel> PagosProformas { get; set; }
        public string Created_at { get; set; }
        public string Updated_at { get; set; }
    }

    public class FiltroProformaViewModel
    {
        public int Id { get; set; }
        //public int CantContainer { get; set; }
        public string NumeroProforma { get; set; }
        public string CodigoProveedor { get; set; }
        //public string FechaLlegada { get; set; }
        //public string FechaEmbarque { get; set; }
        public string FechaIni { get; set; }
        public string FechaFin { get; set; }
        //public double Monto { get; set; }
        //public int Estatus { get; set; }
        //public List<PagoViewModel> PagosProformas { get; set; }
       // public string Created_at { get; set; }
        //public string Updated_at { get; set; }
    }

    public class PagoViewModel
    {
        public int Id { get; set; }
        public int ProformaId { get; set; }
        public double MontoPago { get; set; }
        public string Observacion { get; set; }
        public DateTime FechaPago { get; set; }
        public int Estatus { get; set; }
        public int FormaPagoId { get; set; }
        public int AcreedorId { get; set; }
        public int UserId { get; set; }
        public string Created_at { get; set; }
        public string Updated_at { get; set; }
    }

    /*public class PagosViewModel
    {
        public int Id { get; set; }
        public int ProformaId { get; set; }
        public string Observacion { get; set; }
        public DateTime Fecha { get; set; }
        public double MontoPago { get; set; }
        public int Estatus { get; set; }
    }*/

    public class FiltroPagoViewModel
    {
        public int ProformaId { get; set; }
        public string FechaIni { get; set; }
        public string FechaFin { get; set; }
    }

    public class AcreedorViewModel
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int Estatus { get; set; }
    }

    public class FacturaProformaViewModel
    {
        public int Id { get; set; }
        public int ProformaId { get; set; }
        public string NumeroFactura { get; set; }
        public double Monto { get; set; }
        public string FechaFactura { get; set; }
        public int Estatus { get; set; }
    }
}
