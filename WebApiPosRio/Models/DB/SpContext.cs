using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPosRio.Models.DB
{
    public class SpPruebaData
    {
        public int Id { get; set; }
        public int Estatus { get; set; }
    }


    public class SpDetalleMoneda
    {
        public string CodigoMoneda { get; set; }
        public double TotalMoneda { get; set; }
    }

    public class SpDetalleFormaPago
    {
        public int CajaId { get; set; }
        public string CodigoCaja { get; set; }
        public int Turno { get; set; }
        public int FormaPagoId { get; set; }
        public string Descripcion { get; set; }
        public double TotalFormaPago { get; set; }
    }

    public class SpDetalleFormaPagoCajaAdm
    {
        public int CajaId { get; set; }
        public string CodigoCaja { get; set; }
        public int Turno { get; set; }
        public int FormaPagoId { get; set; }
        public string Descripcion { get; set; }
        public double TotalFormaPago { get; set; }
        public double TotalPagoSegunCaja { get; set; }
    }

    public class SpFacturadoXTurnoXFecha
    {
        public string Fecha { get; set; }
        public int IdTurno { get; set; }
        public string Caja { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime? Fin { get; set; }
        public string FacturaInicial { get; set; }
        public string? FacturaFinal { get; set; }
        public double Totalfacturadobs { get; set; }
        public double Totalfacturadodolar { get; set; }
        public int Estatus { get; set; }

    }

    public class SpFacturadoXCajaXFecha
    {
        public string Fecha { get; set; }
        public string NombreTienda { get; set; }
        public string CodigoCaja { get; set; }
        public int AreaId { get; set; }
        public string Area { get; set; }
        public bool EstatusActual { get; set; }
        public double Totalfacturadobs { get; set; }
        public double Totalfacturadodolar { get; set; }
    }

    public class SpListadoFactura
    {
        public string NumeroFactura { get; set; }
        public string NumeroControl { get; set; }
        public DateTime Fecha { get; set; }
        public double MontoPagadobs { get; set; }
        public double MontoBrutobs { get; set; }
        public double MontoDescuentobs { get; set; }
        public double MontoIVAbs { get; set; }
        public double MontoPagadodolar { get; set; }
        public double MontoBrutodolar { get; set; }
        public double MontoDescuentodolar { get; set; }
        public double MontoIVAdolar { get; set; }
        public int Estatus { get; set; }
        public string Tienda { get; set; }
        public string Caja { get; set; }
        public int Turno { get; set; }
        public double Tasa { get; set; }
    }

    public class SpListadoDetalleFactura
    {
        public string NumeroFactura { get; set; }
        public string NumeroControl { get; set; }
        public DateTime Fecha { get; set; }
        public string CodigoProducto { get; set; }
        public string Descripcion { get; set; }
        public double PrecioBolivar { get; set; }
        public double PrecioDolar { get; set; }
        public string Cantidad { get; set; }
        public string Total { get; set; }
        public string Tasa { get; set; }
    }

    public class SpMostrarCierresToC {
        public int FormaPagoId { get; set; }
        public int Orden { get; set; }
        public string Origen { get; set; }
        public string Descripcion { get; set; }
        public double Monto { get; set; }
    }

    public class SpDeclaradoProcesado
    {
        public int TiendaId { get; set; }
        public int EstatusArqueo { get; set; }
        public int EstatusTurno { get; set; }
        public int FormaPagoId { get; set; }
        public int Turno { get; set; }
        public string Caja { get; set; }
        public int Orden { get; set; }
        public string Origen { get; set; }
        public string Descripcion { get; set; }
        public double Monto { get; set; }
        public double MontoBs { get; set; }
    }

    public class SpMostrarCierresTurnoS4
    {
        public int FormaPagoId { get; set; }
        public string Origen { get; set; }
        public string Descripcion { get; set; }
        public double Monto { get; set; }
    }

    public class SpReporteCierresToC
    {
        public string lote { get; set; }
        public string Descripcion { get; set; }
        public double Monto { get; set; }
    }

    public class CrudAccion
    {
        public int a { get; set; }
    }

    public class SpAcciones
    {
    }

    public class SpDescloseDolar
    {
        public int Denominacion { get; set; }
        public int Cantidad { get; set; }
        public double Monto { get; set; }
        public int Moneda { get; set; }
    }

    public class SpArqueo
    {
    }

    public class SpMostrarCierreCaja
    {
    }

    public class SpFaltanteSobrante
    {
        public string Caja { get; set; }
        public string Cajera { get; set; }
        public int Turno { get; set; }
        public string Descripcion { get; set; }
        public double Diferencia { get; set; }
        public double Declarado { get; set; }
        public double Procesado { get; set; }
    }

    public class SpListadoFacturaRangFech
    {
        public string NumeroFactura { get; set; }
        public string NumeroControl { get; set; }
        public string FormaPagoId { get; set; }
        public double MontoPagado { get; set; }
        public int Estatus { get; set; }
        public int FacturaId { get; set; }
        public int ClienteId { get; set; }
        public int TiendaId { get; set; }
        public int CajeraId { get; set; }
        public int TurnoId { get; set; }
        public int CajaId { get; set; }
        public DateTime Fecha { get; set; }
        public double tasa { get; set; }
    }

    public class SpReporteFormasPago
    {
        public int TiendaId { get; set; }
        public string Tienda { get; set; }
        public int CajaId { get; set; }
        public string Caja { get; set; }
        public int TurnoId { get; set; }
        public int FormaPagoId { get; set; }
        public string FormaPago { get; set; }
        public string CodigoMoneda { get; set; }
        public double Monto { get; set; }
        public DateTime Fecha { get; set; }

    }

    public class ReporteExcelFormasPago
    {
        public int TiendaId { get; set; }
        public string Tienda { get; set; }
        public int CajaId { get; set; }
        public string Caja { get; set; }
        public int TurnoId { get; set; }
        public int FormaPagoId { get; set; }
        public string FormaPago { get; set; }
        public string CodigoMoneda { get; set; }
        public double Monto { get; set; }
        public string Fecha { get; set; }

    }

    public class SpFormasPagoFacturaId
    {
        public int Id { get; set; }
        public int FormaPagoId { get; set; }
        public string Descripcion { get; set; }
        public string CodigoMoneda { get; set; }
        public double Monto { get; set; }
    }

    public class SpDevolucionByTurnoId
    {
        public int Cant { get; set; }
        public double Total { get; set; }
    }

    public class SpResumenxCajaxFecha
    {
        public string Fecha { get; set; }
        public string Tienda { get; set; }
        public int CajaId { get; set; }
        public string Caja { get; set; }
        public int CantTurno { get; set; }
        public int EstatusTurno { get; set; }
        public int EstatusArqueo { get; set; }
        public double TotalNetobs { get; set; }
        public double TotalNetodolar { get; set; }
        public double TotalDevolucionbs { get; set; }
        public double TotalDevoluciondolar { get; set; }
        public int CantFactura { get; set; }
        public int CantDevoluciones { get; set; }
    }


    public class SpListadoDevoluciones
    {
        public int TiendaId { get; set; }
        public DateTime Fecha { get; set; }
        public string NumeroDevolucion { get; set; }
        public string NumeroFactura { get; set; }
        public int IdFactura { get; set; }
        public int IdDevolucion { get; set; }
        public int IdTurno { get; set; }
        public int IdCaja { get; set; }
        public int IdCliente { get; set; }
        public double Tasa { get; set; }
        public double MontoDevolucion { get; set; }
        public double MontoFactura { get; set; }
    }

    public class SpListadoWallet
    {
        public int TiendaId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Saldo { get; set; }
        public decimal Pago { get; set; }
        public decimal Dep { get; set; }
        public int IdCliente { get; set; }
        public string RIF { get; set; }
        public string NombreCliente { get; set; }
    }

    public class SpMostrarConsolidado
    {
        public int Mes { get; set; }
        public int Anio { get; set; }
        public int TiendaId { get; set; }
        public double Totalfacturadobs { get; set; }
        public double Totalfacturadodolar { get; set; }
        public int CantFactura { get; set; }
        public double TotalDevolucion { get; set; }
        public int CantDevolucion { get; set; }
        public double IVABs { get; set; }
        public double IVADls { get; set; }
    }

    public class SpMostrarQQM
    {
        public int Id { get; set; }
        public int Nivel { get; set; }
        public string PreguntaRespuesta { get; set; }
        public int? IdPremio { get; set; }
        public string? Premio { get; set; }
        public string Created_at { get; set; }
        public string Updated_at { get; set; }
    }

    public class SpListadoConstante
    {
        public int Id { get; set; }
        public int Valor { get; set; }
    }

    public class SpGame
    {
        public int SorteoId { get; set; }
    }

    public class SpListadoProveedor
    {
        public string C_codproveed { get; set; }
        public string C_descripcio { get; set; }
        public string C_rif { get; set; }
        public string C_ciudad { get; set; }
        public string C_estado { get; set; }
        public string C_pais { get; set; }
        public string C_telefono { get; set; }
        public string C_fax { get; set; }
        public string C_web { get; set; }
        public int N_activo { get; set; }
        public DateTime Update_date { get; set; }
        public DateTime Add_date { get; set; }
        public string C_razon { get; set; }
        public string C_UsuarioAdd { get; set; }
        public string C_UsuarioUpd { get; set; }
    }
    public class SpProformaViewModel
    {
        public int Id { get; set; }
        public int CantidadContainer { get; set; }
        public string Descripcion { get; set; }
        public string NumeroProforma { get; set; }
        public string CodigoProveedor { get; set; }
        public int MonedaId { get; set; }
        public string FechaLlegada { get; set; }
        public string FechaEmbarque { get; set; }
        public string FechaProforma { get; set; }
        public double Monto { get; set; }
        public int Estatus { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        //Produccion
        public string DescripcionProduccion { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public double MontoConversion { get; set; }
        public double MontoPagado { get; set; }
        public double MontoResta { get; set; }
    }

    public class SpPagoProforma
    {
        public int Id { get; set; }
        public int ProformaId { get; set; }
        public double MontoPago { get; set; }
        public string Observacion { get; set; }
        public string FechaPago { get; set; }
        public int FormaPagoId { get; set; }
        public int AcreedorId { get; set; }
        public int Estatus { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }

    public class SpProfomaSave
    {
        public int IdProforma { get; set; }
    }

    public class SpSaveAcreedor
    {
        public int IdAcreedor { get; set; }
    }

    public class SpSaveFacturaProforma
    {
        public int IdFactura { get; set; }
    }

    public class SpListadoFormaPago
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int MonedaId { get; set; }
        public double FactorCambio { get; set; }
        public int Estatus { get; set; }
    }

    public class SpListadoAcreedor
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int Estatus { get; set; }
    }

    public class SpListadoFactProfroma
    {
        public int Id { get; set; }
        public int ProformaId { get; set; }
        public string NumeroFactura { get; set; }
        public double Monto { get; set; }
        public string FechaFactura { get; set; }
        public int Estatus { get; set; }
    }

    public class SpDesglosePuntos
    {
        public string Banco { get; set; }
        public int FormaPagoId { get; set; }        
        public string FormaPago { get; set; }
        public string TipoTarjeta { get; set; }
        public double Monto { get; set; }
        public string Lote { get; set; }
        public DateTime Fecha { get; set; }
    }

    public class SpMostrarVentasXDep
    {
        public string Dpto { get; set; }
        public double Monto { get; set; }
        public double Costo { get; set; }
        public double Utilidad { get; set; }
        public double Montousd { get; set; }
        public double Costousd { get; set; }
        public double Utilidadusd { get; set; }
        public double PorcUtil { get; set; }
    }

    public class SpMostrarFacturaPromedio
    {
        public DateTime fecha { get; set; }
        public int TiendaId { get; set; }
        public string Tienda { get; set; }
        public int CantFactura { get; set; }
        public double TicketPromedio { get; set; }
        public int CantFacturaAcum { get; set; }
        public double TicketPromedioAcum { get; set; }
    }

    public class SpListaResumenVentaXFormaPago
    {
        public int TiendaId { get; set; }
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public double VentaNeta { get; set; }
        public double VentaCredito { get; set; }
        public double VentaBolivares { get; set; }
        public double VentaDolares { get; set; }
        public double VentaEuro { get; set; }
        public double VentaZelle { get; set; }
        public double VentaWalletDpto { get; set; }
        public double VentaWalletPago { get; set; }
    }


    public class SpListadoResumenVenta
    {
        public int TiendaId { get; set; }
        public string Nombre { get; set; }
        //public DateTime Fecha { get; set; }
        public int CantFactura { get; set; }
        public double TotalBolivares { get; set; }
        public double TotalDolares { get; set; }
        public double TicketPromedio { get; set; }
        public double meta { get; set; }
        public double ProdPromedio { get; set; }
        public double IVABs { get; set; }
        public double IVADls { get; set; }
    }

    public class SpListadoResumenVentaDepartamento
    {
        public string Departamento { get; set; }
        public double Total { get; set; }
        public double TotalNac { get; set; }
        public double TotalInt { get; set; }
        public double Utilidad { get; set; }
        public double UtilidadNac { get; set; }
        public double UtilidadInt { get; set; }
        public double Margen { get; set; }
    }

    public class SpListadoResumenTopVentaProducto
    {
        public string Tienda { get; set; }
        public string Codigo { get; set; }
        public string Departamento { get; set; }
        public string Descripcion { get; set; }
        public string Procedencia { get; set; }
        public double Cantidad { get; set; } /*si los producos soninside se suma por lo tanto no se sabe la tienda*/
        public double TotalBolivares { get; set; }
        public double TotalDolares { get; set; }
    }

    public class SpListadoDonaciones
    {
        public DateTime Fecha { get; set; }
        public string Fundacion { get; set; }
        public string NombreCliente { get; set; }
        public string CodigoCaja { get; set; }
        public int TurnoId { get; set; }
        public string TipoPago { get; set; }
        public double MontoDonado { get; set; }
        public double Tasa { get; set; }
        public int TiendaId { get; set; }
        public string Tienda { get; set; }
    }
    public class SpBuscarTasas
    {
        public double monto { get; set; }
        public string simbolo { get; set; }
        public string descripcion { get; set; }
        public int monedaId { get; set; }
        public DateTime fecha { get; set; }
    }
    public class SpGuardarTasa
    {
        public int r { get; set; }
    }

    public class SpCrearHorarios
    {
        public int IdTurno { get; set; }
    }
    public class SpAsignarDiasxTurnos
    {
        public int r { get; set; }
    }

    public class SpAsignarHorariosxEmpleados
    {
        public int r { get; set; }
    }

    public class SpBuscarTurnos
    {
        public int IdTurno { get; set; }
        public string Descripcion { get; set; }
        public string Fecha_Creacion { get; set; }
        public string Fecha_Modificacion { get; set; }
        public int Activo { get; set; }

    }

    public class SpBuscarEmpleados
    {
        public int Cedula { get; set; }
        public string Nombre { get; set; }
        public string Cargo { get; set; }
        public int Activo { get; set; }

    }


    public class SpContext : RIOPOSContext
    {
        public DbSet<SpPruebaData> SpDatabase { get; set; }
        public DbSet<SpAcciones> spActAcciones { get; set; }
        public DbSet<CrudAccion> SpCrudAccion { get; set; }
        public DbSet<SpDetalleMoneda> SpDetalleMoneda { get; set; }
        public DbSet<SpDetalleFormaPago> SpDetalleFormaPago { get; set; }
        public DbSet<SpFacturadoXCajaXFecha> SpFacturadoXCajaXFecha { get; set; }
        public DbSet<SpFacturadoXTurnoXFecha> SpFacturadoXTurnoXFecha { get; set; }
        public DbSet<SpListadoFactura> SpListadoFactura { get; set; }
        public DbSet<SpListadoDetalleFactura> SpListadoDetalleFactura { get; set; }
        public DbSet<SpAcciones> SpAddArqueo { get; set; }
        public DbSet<SpMostrarCierresToC> SpCierreTurno { get; set; }
        public DbSet<SpMostrarCierresTurnoS4> SpCierreTurnoS4 { get; set; }
        public DbSet<SpMostrarCierresToC> SpCierreCaja { get; set; }
        public DbSet<SpReporteCierresToC> SpCierreCajaPos { get; set; }
        public DbSet<SpReporteCierresToC> SpCierreTurnoPos { get; set; }
        public DbSet<SpDeclaradoProcesado> SpDeclaradoProcesado { get; set; }
        public DbSet<SpDescloseDolar> SpDesgloseDolarArqueo { get; set; }
        public DbSet<SpFaltanteSobrante> SpFaltanteSobrante { get; set; }
        public DbSet<SpListadoFacturaRangFech> ListadoFacturaRangFech { get; set; }
        public DbSet<SpReporteFormasPago> ReporteFormaPagoRangFech { get; set; }
        public DbSet<SpFormasPagoFacturaId> FormaPagoFacturaId { get; set; }
        public DbSet<SpDevolucionByTurnoId> BuscarDevoluciones { get; set; }
        public DbSet<SpResumenxCajaxFecha> ResumenxCajaxFechas { get; set; }
        public DbSet<SpListadoDevoluciones> ListadoDevoluciones { get; set; }
        public DbSet<SpListadoWallet> ListadoWallets { get; set; }
        public DbSet<SpMostrarConsolidado> MostrarConsolidado { get; set; }
        public DbSet<SpMostrarQQM> MostrarQQMs { get; set; }
        public DbSet<SpListadoConstante> MostrarConstantes { get; set; }
        public DbSet<SpGame> SetGame { get; set; }
        public DbSet<SpListadoProveedor> ListadoProveedor { get; set; }
        public DbSet<SpProformaViewModel> ListadoProforma { get; set; }
        public DbSet<SpPagoProforma> ListadoPagoProforma { get; set; }
        public DbSet<SpProfomaSave> SpSaveProforma { get; set; }
        public DbSet<SpSaveFacturaProforma> SpSaveFacturaProformas { get; set; }        
        public DbSet<SpSaveAcreedor> SpSaveAcreedors { get; set; }
        public DbSet<SpListadoFormaPago> SpListadoFormaPago { get; set; }
        public DbSet<SpListadoAcreedor> SpListadoAcreedor { get; set; }
        public DbSet<SpListadoFactProfroma> SpListadoFactProfromas { get; set; }
        public DbSet<SpDesglosePuntos> SpDesglosePunto { get; set; } 
        public DbSet<SpMostrarVentasXDep> MostrarVentasXDep { get; set; }
        public DbSet<SpMostrarFacturaPromedio> MostrarFacturaPromedio { get; set; }
        public DbSet<SpListaResumenVentaXFormaPago> FormaPagoInside { get; set; }
        public DbSet<SpListadoResumenVenta> listadoResumenVentasGeneral { get; set; }
        public DbSet<SpListadoResumenVentaDepartamento> ListadoResumenVentaDepartamentos { get; set; }
        public DbSet<SpListadoResumenTopVentaProducto> ListadoResumenTopVentaProducto { get; set; }
        public DbSet<SpListadoDonaciones> ListadoDonaciones { get; set; }        
        public DbSet<SpBuscarTasas> BuscarTasas { get; set; }        
        public DbSet<SpGuardarTasa> GuardarTasa { get; set; }

    }

    public class SpContextB : BIOContext
    {
        public DbSet<SpCrearHorarios> CrearHorario { get; set; }
        public DbSet<SpAsignarDiasxTurnos> AsignarDiasxTurnos { get; set; }
        public DbSet<SpAsignarHorariosxEmpleados> AsignarHorariosxEmpleado { get; set; }
        public DbSet<SpBuscarTurnos> BuscarTurnos { get; set; }
        public DbSet<SpBuscarEmpleados> BuscarEmpleados { get; set; }

    }
}
