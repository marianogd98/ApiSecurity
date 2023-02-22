using System.Collections.Generic;
using WebApiPosRio.Models.DB;
using WebApiPosRio.Models.ViewModels;

namespace WebApiPosRio.Models.Repository
{
    public interface IComprasInternacionalRepository
    {
        List<SpListadoProveedor> GetProveedores(ProveedorViewModel proveedorViewModel);
        int SaveProforma(ProformaViewModel proformaViewModel);

        List<SpProformaViewModel> GetProformas(FiltroProformaViewModel proforma);
        List<SpListadoFormaPago> GetFormaPago(string descripcion);
        List<SpListadoAcreedor> GetAcreedor(string descripcion);
        //List<SpPagoProforma> GetPagosProformas(FiltroPagoViewModel pagoProforma);
        int SavePagosProforma(PagoViewModel pagoProformaViewModel);
        int SaveAcreedor(AcreedorViewModel acreedorViewModel);
        int AnularProforma(int Id);
        int EliminarProforma(int Id);
        List<SpListadoFactProfroma> Getfacturas(int ProformaId);
        int SaveFacturas(FacturaProformaViewModel acreedorViewModel);
        public object GetPagosProformas(FiltroPagoViewModel pagoProforma);
    }
}