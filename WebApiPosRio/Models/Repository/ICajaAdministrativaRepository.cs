using System.Collections.Generic;
using WebApiPosRio.Models.DB;
using WebApiPosRio.Models.Response;
using WebApiPosRio.Models.ViewModels;

namespace WebApiPosRio.Models.Repository
{
    public interface ICajaAdministrativaRepository
    {
        //ResponseData DetallePagos();
        ResponseData DetalleMoneda();
        ResponseData DetalleFormaPago(string CodMoneda, int IdTurno);
        ResponseData DetalleFormaPagoTurnoVerificado(string codMoneda, int IdTurno);
        ResponseData FacturadoXCajaXFecha(ParamsLfViewModel paramsLfViewModel);
        ResponseData FacturadoXTurnoXFecha(string fechaActual, int page, int perPage, string CodigoCaja = "0");
        ResponseData ListadoFactura(ParamsLfViewModel paramsLFViewModel);
        ResponseData DetalleFactura(DetalleFacturaviewModel detalleFacturaviewModel);
        ResponseData UpdateFacturaEstatus(CajaAdmViewModel Factura);
        ResponseData AnularFactura(CajaAdmViewModel Factura);
        ResponseData AddArqueo(ArqueoViewModel arqueoViewModel);
        ResponseData UpdateArqueoEstatus(int IdTurno, int IdUsuario);
        ResponseData ReporteCierreTurno(int cierreTurno);
        ResponseData ReporteCierreTurnosS4(string fecha);
        ResponseData ReporteCierreCaja(string codCaja, string fecha);
        ResponseData DesgloseDolarArqueo(int IdArqueo);
        ResponseData ListadoFaltantesSobrantes(int IdTurno, int IdCaja, int IdUsuario, string nombreCajera, string FormaPagoId,string fechaI, string fechaF, int page, int perPage);
        List<SpListadoFacturaRangFech> ListadoFacturaRangFech(FiltroListadoFactura filtroListadoFactura);
        List<SpReporteFormasPago> ReporteFormasPagoRangFech(TesoreriaViewModel tesoreriaViewModel);
        ResponseData DetalleFormaPagoFacturaId(int IdFactura);
        List<SpResumenxCajaxFecha> ResumenCajaXTurnosEstatus(string fecha, string CodCaja = "0");
        ResponseData ListadoDeclaradoProcesado(ResumenCajaFiltro resumenCajaFiltro, int page, int perPage);
        List<FormaPagoConfViewModel> GetFormasPago();
        List<ReportViewModel> ListadoDecProcExcel(ResumenCajaFiltro resumenCajaFiltro);
        ArqueoData GetArqueo(int TurnoId);
        ResponseData ResetArqueo(int idTurno, int idUsuario);
        List<SpDesglosePuntos> ResumenDesglosePuntos(string Fecha, int TurnoId, int CajaId);
        List<SpDescloseDolar> ResumenDesgloseDolar(string Fecha, int TurnoId);
    }
}