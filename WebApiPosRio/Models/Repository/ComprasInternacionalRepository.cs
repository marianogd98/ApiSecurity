using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPosRio.Models.DB;
using WebApiPosRio.Models.ViewModels;

namespace WebApiPosRio.Models.Repository
{
    public class ComprasInternacionalRepository : IComprasInternacionalRepository
    {
        private readonly ISpRepository _SpRepo;

        public ComprasInternacionalRepository(ISpRepository spRepo)
        {
            _SpRepo = spRepo;
        }

        public List<SpListadoFormaPago> GetFormaPago(string descripcion)
        {
            return _SpRepo.GetFormaPago(descripcion);
        }

        public List<SpListadoAcreedor> GetAcreedor(string descripcion)
        {
            return _SpRepo.GetAcreedor(descripcion);
        }

        public object /*List<SpPagoProforma>*/ GetPagosProformas(FiltroPagoViewModel pagoProforma)
        {
            var _pagos = _SpRepo.GetPagos(pagoProforma);
            var fp = GetFormaPago("");

            var pagosList = new
            {
                pagosAcum = _pagos.GroupBy(xpp => xpp.FormaPagoId).Select(
                    g => new
                    {
                        Key = g.Key,
                        Acum = g.Sum(s => s.MontoPago),
                        AcumDolar = fp.FirstOrDefault(x => x.Id==g.Key).FactorCambio * g.Sum(s => s.MontoPago)
                        //FormaPagoId = g.Select(x =>x.FormaPagoId).FirstOrDefault()
                    }
                    ),
                Pagos = _pagos
            };
             return pagosList;
            //return _SpRepo.GetPagos(pagoProforma);
        }

        public int SavePagosProforma(PagoViewModel pagoProformaViewModel)
        {
            return _SpRepo.SaveUpdatePagoProforma(pagoProformaViewModel);
        }

        public List<SpProformaViewModel> GetProformas(FiltroProformaViewModel proforma)
        {      
            var dataFilter = _SpRepo.GetProformas(proforma).OrderByDescending(p => p.Updated_at).ToList();

            if (proforma.NumeroProforma != null)
            {
                dataFilter = dataFilter.FindAll(df => df.NumeroProforma.Contains(proforma.NumeroProforma.ToUpper())).ToList();
            }

            if (proforma.CodigoProveedor != null)
            {
                if(proforma.CodigoProveedor != "-1")
                    dataFilter = dataFilter.FindAll(df => df.CodigoProveedor.Contains(proforma.CodigoProveedor.ToUpper())).ToList();
            }

            return dataFilter;
        }

        public List<SpListadoProveedor> GetProveedores(ProveedorViewModel proveedorViewModel)
        {
            return _SpRepo.GetProveedores(proveedorViewModel.Rif, proveedorViewModel.Nombre).OrderBy(p => p.C_descripcio).ToList();
        }

        public List<SpListadoFactProfroma> Getfacturas(int ProformaId)
        {
            return _SpRepo.GetFactura(ProformaId);
        }

        public int SaveFacturas(FacturaProformaViewModel facturaViewModel)
        {

            int RespSUPro = _SpRepo.SaveUpdateFactura(facturaViewModel);

            return RespSUPro;
        }

        public int SaveProforma(ProformaViewModel proformaViewModel)
        {
            if(proformaViewModel.Monto == proformaViewModel.PagosProformas.Sum(pvm => pvm.MontoPago))
            {
                proformaViewModel.Estatus = 2;
            }

            int RespSUPro = _SpRepo.SaveUpdateProforma(proformaViewModel);

            if(proformaViewModel.Id >0 && proformaViewModel.PagosProformas.Count == 0)
            {
                int RespSUPPago = _SpRepo.DeleteProforma(proformaViewModel.Id,-1,1);
                return RespSUPro;
            }

            foreach (var item in proformaViewModel.PagosProformas)
            {
                item.UserId = proformaViewModel.UserId;
                item.ProformaId = RespSUPro;
                int RespSUPPago = _SpRepo.SaveUpdatePagoProforma(item);
            } 
                                 
            return RespSUPro;
        }

        public int SaveAcreedor(AcreedorViewModel acreedorViewModel)
        {

            int RespSUPro = _SpRepo.SaveUpdateAcreedor(acreedorViewModel);

            return RespSUPro;
        }

        public int AnularProforma(int Id)
        {
            int RespSUPro = _SpRepo.DeleteProforma(Id, -1);

            return RespSUPro;
        }

        public int EliminarProforma(int Id)
        {
            int RespSUPro = _SpRepo.DeleteProforma(Id, -1);

            return RespSUPro;
        }


    }
}
