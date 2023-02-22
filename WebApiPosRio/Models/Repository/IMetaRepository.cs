using System.Collections.Generic;
using WebApiPosRio.Models.ViewModels;

namespace WebApiPosRio.Models.Repository
{
    public interface IMetaRepository
    {
        bool CargaDataExcel(DataMeta presupuestoViewModels);
        object GetDataMeta(InsideVentaViewModel insideVentaViewModel);
        object GetDataMetaAcum(InsideVentaViewModel insideVentaViewModel);
        List<VentasMetasPorDia> GetListMeta(InsideVentaViewModel insideVentaViewModel);
        object GetGrafica(InsideVentaViewModel insideVentaViewModel);
    }
}