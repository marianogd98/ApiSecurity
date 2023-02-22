using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiPosRio.Models.DB;
using WebApiPosRio.Models.ViewModels;

namespace WebApiPosRio.Models.Repository
{
    public interface IInsideRepository
    {
        BigLineChart Consolidado(string fechaIni, string fechaFin);
        //envia data de una tienda
        List<SpMostrarFacturaPromedio> MostrarFacturaPromedios(string fechaIni);
        List<SpListaResumenVentaXFormaPago> MostrarFormasPagoInside(string fechaIni, string fechaFin, int tiendaId);
        List<SpMostrarConsolidado> MostrarConsolidadoTienda(string fechaIni, string fechaFin);
        List<SpMostrarVentasXDep> MostrarVentaDepartamento(string fechaIni, string fechaFin);
        List<SpListadoResumenVenta> MostrarVentaDepartamento(InsideVentaViewModel insideFiltro);
        List<SpListadoResumenVentaDepartamento> ListadoVentaDepartamentoInside(InsideVentaViewModel insideFiltro);
        object ListadoResumenTopVentaProducto(InsideVentaViewModel insideFiltro);
        List<SpReporteFormasPago> ListadoTesoreriaFormasPagoPorTienda(TesoreriaViewModel tesoreriaViewModel);
    }
}