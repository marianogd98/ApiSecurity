using System;
using System.Collections.Generic;
using System.Xml;
using WebApiPosRio.Models.DB;
using WebApiPosRio.Models.ViewModels;

namespace WebApiPosRio.Models.Repository
{
    public interface ISpRepository
    {
        void SetAccionesUsuario(XmlDocument xml);
        SpPruebaData VerificarTurno(int IdUsuario, int IdCaja);
        List<SpDetalleMoneda> DetalleMoneda();
        List<SpDetalleFormaPago> DetalleFormaPago(string tipoMoneda, int idTurno);
        List<SpFacturadoXCajaXFecha> FacturadoXCajaXFecha(string fechaActual);
        List<SpFacturadoXTurnoXFecha> FacturadoXTurnoXFecha(string fechaActual, string caja);
        List<SpListadoFactura> ListadoFactura(ParamsLfViewModel filtroLF);
        List<SpListadoDetalleFactura> DetalleFactura(DetalleFacturaviewModel filtroLF);
        int ActualizarFactura(int idFactura, int idUser, int EstatusActualiza);
        int CrudRol(RolViewModel crudAccion, int AccionCrud = 1);
        int SpAddArqueo(ArqueoViewModel arqueo, string XmlDetalleArqueo);
        List<SpMostrarCierresToC> MostrarCierreTurno(int idTurno);
        List<SpMostrarCierresToC> MostrarCierreCaja(string codCaja, string fecha);
        int VerificaTurno(int idTurno, int idUser);
        //List<SpReporteCierresToC> ReporteCierreTurno(int IdTurno);
        List<SpMostrarCierresTurnoS4> ReporteCierreTurnosS4(string fecha);
        List<SpReporteCierresToC> ReporteCierreCaja(int IdCaja, string fecha, int IdTurno = 0);
        List<SpDescloseDolar> DesgloseDolarArqueo(int ArqueoId);
        List<SpFaltanteSobrante> ListadoSobrantesFaltantes(int IdTurno, int IdCaja, int IdUsuario, string fechaI, string fechaF);
        List<SpListadoFacturaRangFech> ListadoFacturaRangFech(string NroControl, string NroFactura, string ZetaSerial, string fechaI, string fechaF);
        List<SpReporteFormasPago> ReporteFormasPagoRangFech(int IdTienda, int IdCaja, int Idturno, string fechaI, string fechaF);
        List<SpFormasPagoFacturaId> FormasPagoFacturaId(int IdFactura);
        List<SpDevolucionByTurnoId> BuscarDevoluciones(int TurnoId);
        List<SpResumenxCajaxFecha> ResumenxCajaxFechas(string fecha, string CodCaja = "0");
        List<SpDeclaradoProcesado> DeclaradoyProcesado(string codCaja, string fecha);
        List<SpListadoDevoluciones> ListadoDevoluciones(DateTime Fecha, string NumDevolucion = "0", string NumFactura = "0", int IdFactura = 0);
        List<SpListadoWallet> ListadoWallets(DateTime FechaIni, DateTime FechaFin, string Rif = "0");
        int ResetArqueo(int idTurno, int idUser);
        List<SpMostrarConsolidado> MostrarConsolidado(string fechaIni, string fechaFin);
        List<SpMostrarQQM> GetQQM(int IdQQM);
        int SaveQuestionsQQM(int Id, SpMostrarQQM mostrarQQM);
        int SaveGiftsQQM(int Id, SpMostrarQQM mostrarQQM);
        List<SpListadoConstante> GetConstantes(int Id = 0);
        int SetConstantes(int Id, int Valor);
        SpGame SetGame(int Id = 0, int NivelId = 0, string Pregunta = "");
        List<SpListadoProveedor> GetProveedores(string Id = "", string Nombre = "");
        int SaveUpdateProforma(ProformaViewModel proforma);
        List<SpProformaViewModel> GetProformas(FiltroProformaViewModel proformaViewModel);
        List<SpListadoFormaPago> GetFormaPago(string Descripcion);
        List<SpListadoAcreedor> GetAcreedor(string Descripcion);
        List<SpPagoProforma> GetPagos(FiltroPagoViewModel pagoProformaViewModel);
        int SaveUpdatePagoProforma(PagoViewModel pagoProforma);
        int SaveUpdateAcreedor(AcreedorViewModel acreedor);
        int DeleteProforma(int Id, int Estatus, int All=0);
        int SaveUpdateFactura(FacturaProformaViewModel factProforma);
        List<SpListadoFactProfroma> GetFactura(int ProformaId);
        List<SpDesglosePuntos> GetDesglosePuntos(string Fecha, int TurnoId, int CajaId);
        List<SpDescloseDolar> GetDesgloseEfectivoDE(string Fecha, int TurnoId);
        List<SpMostrarFacturaPromedio> MostrarFacturaPromedios(string fechaIni);
        List<SpListaResumenVentaXFormaPago> MostrarFormasPagoInside(string fechaIni, string fechaFin, int tiendaId);
        List<SpMostrarVentasXDep> MostrarVentasXDep(string fechaIni, string fechaFin);
        List<SpListadoResumenVenta> ListadoResumenVentaInside(int tiendaId, string fechaIni, string fechaFin = "");
        List<SpListadoResumenVentaDepartamento> ListadoResumenVentaDepartamentoInside(int tiendaId, string fechaIni, string fechaFin);
        List<SpListadoResumenTopVentaProducto> ListadoResumenTopVentaProducto(int tiendaId, string fechaIni, string fechaFin);
        List<SpListadoResumenTopVentaProducto> ListadoResumenNoTopVentaProducto(int tiendaId, string fechaIni, string fechaFin);
        List<SpListadoDonaciones> ListadoDonacion(ParamsLfViewModel filtroLF);
        //retorna las tasas de un mes, se debe enviar el primer dia del mes
        List<SpBuscarTasas> BuscarTasas(string pFecha);
        int GuardarTasa(string pFecha, double pTasaUsd , double pTasaEur, int pUsuario);

    }
}