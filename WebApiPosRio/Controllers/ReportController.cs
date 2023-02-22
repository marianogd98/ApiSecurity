using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using WebApiPosRio.Models.Repository;
using WebApiPosRio.Models.ViewModels;
using WebApiPosRio.Models.DB;
using Newtonsoft.Json;

namespace WebApiPosRio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;
        private readonly ICajaAdministrativaRepository _dbContextCA;
        private readonly IWalletRepository _devolWall;
        private readonly IMetaRepository _reportMeta;
        private readonly IInsideRepository _reportInside;
        private readonly ITiendaRepository _tiendas;
        private readonly IApiConectorRepository _apiConectorRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private List<TiendaViewModel> dataListTiendas;


        public ReportController(IReportService reportService, ICajaAdministrativaRepository dbContextCA, IWalletRepository walletRepository, IMetaRepository metaRepository, IInsideRepository insideRepository, ITiendaRepository tiendaRepository, IApiConectorRepository apiConector, IUsuarioRepository usuarioRepository)
        {
            _reportService = reportService;
            _dbContextCA = dbContextCA;
            _devolWall = walletRepository;
            _reportMeta = metaRepository;
            _reportInside = insideRepository;
            _tiendas = tiendaRepository;
            _apiConectorRepository = apiConector;
            _usuarioRepository = usuarioRepository;
            dataListTiendas = _tiendas.GetTiendas();
        }
        [HttpGet]
        public  IActionResult Get()
        {
            var workbook = new XLWorkbook();
            try
            {
                //workbook.SaveAs("Budget.xlsx");
                return Ok("Report");
            }
            catch
            {
                return BadRequest("mio");
            }
        }

        [HttpGet("[action]")]
        public IActionResult DownloadExcelDocument([FromQuery] ResumenCajaFiltro resumenCajaFiltro)
        {
            List<ReportViewModel> dataListarqueos = _dbContextCA.ListadoDecProcExcel(resumenCajaFiltro);
            List<FormaPagoConfViewModel> dataListFormaPago = _dbContextCA.GetFormasPago();            
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = "TurnosArqueados.xlsx";
            int index = 1;
            try
            {
                using (var workbook = new XLWorkbook())

                {
                    IXLWorksheet worksheet =
                    workbook.Worksheets.Add("Arqueos");

                    //-----------Le damos el formato a la cabecera----------------
                    var rango = worksheet.Range("A1:I1"); //Seleccionamos un rango
                    rango.Style.Border.SetOutsideBorder(XLBorderStyleValues.Thick); //Generamos las lineas exteriores
                    rango.Style.Border.SetInsideBorder(XLBorderStyleValues.Medium); //Generamos las lineas interiores
                    rango.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center; //Alineamos horizontalmente
                    rango.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;  //Alineamos verticalmente
                    rango.Style.Font.FontSize = 14; //Indicamos el tamaño de la fuente
                    rango.Style.Fill.BackgroundColor = XLColor.AliceBlue; //Indicamos el color de background
                    //Ajustamos el ancho de las columnas para que se muestren todos los contenidos
                    worksheet.Column("A").Width = 15;
                    worksheet.Column("B").Width = 12;
                    worksheet.Column("C").Width = 12;
                    worksheet.Column("E").Width = 15;
                    worksheet.Column("F").Width = 18;
                    worksheet.Column("G").Width = 18;
                    worksheet.Column("H").Width = 18;
                    worksheet.Column("I").Width = 18;
                    worksheet.Column("J").Width = 18;


                    worksheet.Cell(1, 1).Value = "Depositado";
                    worksheet.Cell(1, 2).Value = "Fecha";
                    worksheet.Cell(1, 3).Value = "Tienda";
                    worksheet.Cell(1, 4).Value = "Caja";
                    worksheet.Cell(1, 5).Value = "Turno";
                    worksheet.Cell(1, 6).Value = "Forma Pago";
                    worksheet.Cell(1, 7).Value = "Procesado Bs";
                    worksheet.Cell(1, 8).Value = "Procesado";
                    worksheet.Cell(1, 9).Value = "Declarado";
                    worksheet.Cell(1, 10).Value = "Diferencia";
                    
                    worksheet.RangeUsed().SetAutoFilter();

                    foreach (var item in dataListarqueos)
                    {  
                        worksheet.Cell(index + 1, 1).Value = (item.EstatusArqueo == 1)?"Por Depositar":(item.EstatusArqueo==2)?"Depositado":"---" ;

                        worksheet.Cell(index + 1, 2).Style.NumberFormat.Format = "dd/mm/yyyy";
                        worksheet.Cell(index + 1, 2).Value = resumenCajaFiltro.Fecha;

                        var dato = dataListTiendas.Find(t => t.Id==item.TiendaId).Nombre;
                        worksheet.Cell(index + 1, 3).Value = dato;

                        worksheet.Cell(index + 1, 4).Style.NumberFormat.Format = "000";
                        worksheet.Cell(index + 1, 4).Value = item.Caja;

                        worksheet.Cell(index + 1, 5).Value = item.Turno;

                        worksheet.Cell(index + 1, 6).Value = dataListFormaPago.Find(fp => fp.Id==item.FormaPagoId).Nombre;

                        worksheet.Cell(index + 1, 7).Style.NumberFormat.Format = "#,##0.00";
                        worksheet.Cell(index + 1, 7).DataType = XLDataType.Number;
                        worksheet.Cell(index + 1, 7).Value = (item.FormaPagoId == 7 || item.FormaPagoId == 3 || item.FormaPagoId == 1 || item.FormaPagoId == 8) ? item.Procesado : item.ProcesadoBs;

                        //worksheet.Cell(index + 1, 6).Style.NumberFormat.SetFormat("#.##0,#0");
                        worksheet.Cell(index + 1, 8).Style.NumberFormat.Format = "#,##0.00";
                        worksheet.Cell(index + 1, 8).DataType = XLDataType.Number;
                        worksheet.Cell(index + 1, 8).Value = item.Procesado;

                        worksheet.Cell(index + 1, 9).Style.NumberFormat.Format = "#,##0.00";
                        worksheet.Cell(index + 1, 9).DataType = XLDataType.Number;
                        worksheet.Cell(index + 1, 9).Value = item.Declarado;


                        worksheet.Cell(index + 1, 10).Style.NumberFormat.Format = "#,##0.00";
                        worksheet.Cell(index + 1, 10).DataType = XLDataType.Number;
                        worksheet.Cell(index + 1, 10).Value = (item.Declarado - item.Procesado);

                        index++;
                    }

                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return File(content, contentType, fileName);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Error No: "+index+" -"+ex.Message);
            }
        }

        [HttpGet("[action]")]
        public IActionResult DownloadExcelWallet([FromQuery] WalletViewModel filtroWallet)
        {
            List<SpListadoWallet> dataListarqueos = _devolWall.ListadoWallets(filtroWallet); //_dbContextCA.ListadoDecProcExcel(resumenCajaFiltro);
            //List<FormaPagoConfViewModel> dataListFormaPago = _dbContextCA.GetFormasPago();           
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = "ResumenWallets.xlsx";
            int index = 1;
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    IXLWorksheet worksheet =
                    workbook.Worksheets.Add("Wallet");

                    //-----------Le damos el formato a la cabecera----------------
                    var rango = worksheet.Range("A1:G1"); //Seleccionamos un rango
                    rango.Style.Border.SetOutsideBorder(XLBorderStyleValues.Thick); //Generamos las lineas exteriores
                    rango.Style.Border.SetInsideBorder(XLBorderStyleValues.Medium); //Generamos las lineas interiores
                    rango.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center; //Alineamos horizontalmente
                    rango.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;  //Alineamos verticalmente
                    rango.Style.Font.FontSize = 14; //Indicamos el tamaño de la fuente
                    rango.Style.Fill.BackgroundColor = XLColor.AliceBlue; //Indicamos el color de background
                    //Ajustamos el ancho de las columnas para que se muestren todos los contenidos
                    worksheet.Column("A").Width = 15;
                    worksheet.Column("B").Width = 12;
                    worksheet.Column("C").Width = 12;
                    worksheet.Column("D").Width = 15;
                    worksheet.Column("E").Width = 15;
                    worksheet.Column("F").Width = 20;
                    worksheet.Column("G").Width = 18;
                    //worksheet.Column("H").Width = 18;
                    //worksheet.Column("I").Width = 18;


                    worksheet.Cell(1, 1).Value = "Fecha/Hora";
                    worksheet.Cell(1, 2).Value = "Ced/Rif";
                    worksheet.Cell(1, 3).Value = "Cliente";
                    worksheet.Cell(1, 4).Value = "Tienda";
                    worksheet.Cell(1, 5).Value = "Pagado $(+)";
                    worksheet.Cell(1, 6).Value = "Depositado $(-)";
                    worksheet.Cell(1, 7).Value = "Saldo";
                    //worksheet.Cell(1, 8).Value = "Declarado";
                    //worksheet.Cell(1, 9).Value = "Diferencia";

                    worksheet.RangeUsed().SetAutoFilter();

                    foreach (var item in dataListarqueos)
                    {
                        worksheet.Cell(index + 1, 1).Style.NumberFormat.Format = "dd/mm/yyyy";
                        worksheet.Cell(index + 1, 1).Value = item.Fecha;

                        
                        worksheet.Cell(index + 1, 2).Value = item.RIF;
                        worksheet.Cell(index + 1, 3).Value = item.NombreCliente;

                        var dato = dataListTiendas.ElementAt(item.TiendaId - 1);
                        worksheet.Cell(index + 1, 4).Value = dato;

                        //worksheet.Cell(index + 1, 4).Style.NumberFormat.Format = "000";
                        //worksheet.Cell(index + 1, 4).Value = item.Caja;

                        //worksheet.Cell(index + 1, 5).Value = item.Turno;

                        //worksheet.Cell(index + 1, 6).Value = dataListFormaPago.Find(fp => fp.Id == item.FormaPagoId).Nombre;

                        //worksheet.Cell(index + 1, 6).Style.NumberFormat.SetFormat("#.##0,#0");
                        worksheet.Cell(index + 1, 5).Style.NumberFormat.Format = "#,##0.00";
                        worksheet.Cell(index + 1, 5).DataType = XLDataType.Number;
                        worksheet.Cell(index + 1, 5).Value = item.Pago;

                        worksheet.Cell(index + 1, 6).Style.NumberFormat.Format = "#,##0.00";
                        worksheet.Cell(index + 1, 6).DataType = XLDataType.Number;
                        worksheet.Cell(index + 1, 6).Value = item.Dep;

                        worksheet.Cell(index + 1, 7).Style.NumberFormat.Format = "#,##0.00";
                        worksheet.Cell(index + 1, 7).DataType = XLDataType.Number;
                        worksheet.Cell(index + 1, 7).Value = item.Saldo;

                        index++;
                    }

                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return File(content, contentType, fileName);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Error No: " + index + " -" + ex.Message);
            }
        }


        [HttpGet("[action]")]
        public IActionResult DownloadExcelVentasDepartamentos([FromQuery] InsideVentaViewModel filtroData)
        {
            List<SpListadoResumenVentaDepartamento> dataVentaDep = _reportInside.ListadoVentaDepartamentoInside(filtroData);           

            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = "VentasPorDepartamento_"+filtroData.FechaIni+"_"+filtroData.FechaFin+".xlsx";
            int index = 1;
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    IXLWorksheet worksheet =
                    workbook.Worksheets.Add("VentasPorDepartamento");

                    //-----------Le damos el formato a la cabecera----------------
                    var rango = worksheet.Range("A1:J1"); //Seleccionamos un rango
                    rango.Style.Border.SetOutsideBorder(XLBorderStyleValues.Thick); //Generamos las lineas exteriores
                    rango.Style.Border.SetInsideBorder(XLBorderStyleValues.Medium); //Generamos las lineas interiores
                    rango.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center; //Alineamos horizontalmente
                    rango.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;  //Alineamos verticalmente
                    rango.Style.Font.FontSize = 14; //Indicamos el tamaño de la fuente
                    rango.Style.Fill.BackgroundColor = XLColor.AliceBlue; //Indicamos el color de background
                    //Ajustamos el ancho de las columnas para que se muestren todos los contenidos
                    worksheet.Column("A").Width = 15;//tienda
                    worksheet.Column("B").Width = 25;//departamento
                    worksheet.Column("C").Width = 25;//procedencia
                    worksheet.Column("D").Width = 15;//total Nac
                    worksheet.Column("E").Width = 15;//total int
                    worksheet.Column("F").Width = 20;//total
                    worksheet.Column("G").Width = 18;//utilidad Nac
                    worksheet.Column("H").Width = 18;//utilidad Int                    
                    worksheet.Column("I").Width = 18;//utilidad
                    worksheet.Column("J").Width = 18;//margen

                    worksheet.Cell(1, 1).Value = "Tienda";
                    worksheet.Cell(1, 2).Value = "Departamento";
                    worksheet.Cell(1, 3).Value = "Procedencia";

                    worksheet.Cell(1, 4).Value = "Tot. Nac";
                    worksheet.Cell(1, 5).Value = "Tot. Inter.";
                    worksheet.Cell(1, 6).Value = "Total";

                    worksheet.Cell(1, 7).Value = "Utilidad Nac.";
                    worksheet.Cell(1, 8).Value = "Utilidad Inter.";

                    worksheet.Cell(1, 9).Value = "Utilidad Total";
                    worksheet.Cell(1, 10).Value = "Margen Total";


                    worksheet.RangeUsed().SetAutoFilter();
                    foreach (var item in dataVentaDep)
                    {
                        var dato = (filtroData.tiendaId==0)?"Todas las tiendas":dataListTiendas.Find(t => t.Id == filtroData.tiendaId).Nombre;
                        worksheet.Cell(index + 1, 1).Value = dato;


                        worksheet.Cell(index + 1, 2).Value = item.Departamento;

                        if (filtroData.Procedencia[0] !=null)
                        {
                            worksheet.Cell(index + 1, 3).Value = (filtroData.Procedencia[0] =="nacional")?"Nacional":"Internacional";
                        }
                        else
                        worksheet.Cell(index + 1, 3).Value = "Nacional/Internacional";

                        worksheet.Cell(index + 1, 4).Style.NumberFormat.Format = "#,##0.00";
                        worksheet.Cell(index + 1, 4).DataType = XLDataType.Number;
                        worksheet.Cell(index + 1, 4).Value = item.TotalNac;

                        worksheet.Cell(index + 1, 5).Style.NumberFormat.Format = "#,##0.00";
                        worksheet.Cell(index + 1, 5).DataType = XLDataType.Number;
                        worksheet.Cell(index + 1, 5).Value = item.TotalInt;

                        worksheet.Cell(index + 1, 6).Style.NumberFormat.Format = "#,##0.00";
                        worksheet.Cell(index + 1, 6).DataType = XLDataType.Number;
                        worksheet.Cell(index + 1, 6).Value = item.Total;

                        worksheet.Cell(index + 1, 7).Style.NumberFormat.Format = "#,##0.00";
                        worksheet.Cell(index + 1, 7).DataType = XLDataType.Number;
                        worksheet.Cell(index + 1, 7).Value = item.UtilidadNac;

                        worksheet.Cell(index + 1, 8).Style.NumberFormat.Format = "#,##0.00";
                        worksheet.Cell(index + 1, 8).DataType = XLDataType.Number;
                        worksheet.Cell(index + 1, 8).Value = item.UtilidadInt;

                        worksheet.Cell(index + 1, 9).Style.NumberFormat.Format = "#,##0.00";
                        worksheet.Cell(index + 1, 9).DataType = XLDataType.Number;
                        worksheet.Cell(index + 1, 9).Value = item.Utilidad;

                        worksheet.Cell(index + 1, 10).Style.NumberFormat.Format = "#,##0.00";
                        worksheet.Cell(index + 1, 10).DataType = XLDataType.Number;
                        worksheet.Cell(index + 1, 10).Value = item.Margen;

                        index++;
                    }

                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return File(content, contentType, fileName);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Error No: " + index + " -" + ex.Message);
            }
        }



        [HttpGet("[action]/fecha/{Fecha}/caja/{CodCaja}")]
        public IActionResult ResumenCajaFecha(string Fecha, string CodCaja="")
        {
            var dataResumen = _reportService.ResumenCajaFecha(Fecha, CodCaja);
            return Ok(dataResumen);
        }

        [HttpGet("[action]")]
        public IActionResult DownloadExcelDonaciones([FromQuery] ParamsLfViewModel filtroLF)
        {
            List<SpListadoDonaciones> dataListDonaciones = _reportService.ListadoDonaciones(filtroLF);            
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = "ListadoDonaciones.xlsx";
            int index = 1;
            try
            {
                using (var workbook = new XLWorkbook())

                {
                    IXLWorksheet worksheet =
                    workbook.Worksheets.Add("Donaciones");

                    //-----------Le damos el formato a la cabecera----------------
                    var rango = worksheet.Range("A1:I1"); //Seleccionamos un rango
                    rango.Style.Border.SetOutsideBorder(XLBorderStyleValues.Thick); //Generamos las lineas exteriores
                    rango.Style.Border.SetInsideBorder(XLBorderStyleValues.Medium); //Generamos las lineas interiores
                    rango.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center; //Alineamos horizontalmente
                    rango.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;  //Alineamos verticalmente
                    rango.Style.Font.FontSize = 14; //Indicamos el tamaño de la fuente
                    rango.Style.Fill.BackgroundColor = XLColor.AliceBlue; //Indicamos el color de background
                    //Ajustamos el ancho de las columnas para que se muestren todos los contenidos
                    worksheet.Column("A").Width = 12;
                    worksheet.Column("B").Width = 23;
                    worksheet.Column("C").Width = 18;
                    worksheet.Column("D").Width = 16;
                    worksheet.Column("E").Width = 15;
                    worksheet.Column("F").Width = 15;
                    worksheet.Column("G").Width = 18;
                    worksheet.Column("H").Width = 18;
                    worksheet.Column("I").Width = 18;
                    worksheet.Column("J").Width = 18;


                    worksheet.Cell(1, 1).Value = "Fecha";
                    worksheet.Cell(1, 2).Value = "Fundación";
                    worksheet.Cell(1, 3).Value = "Nombre Cliente";
                    worksheet.Cell(1, 4).Value = "Nro. Caja";
                    worksheet.Cell(1, 5).Value = "Turno";
                    worksheet.Cell(1, 6).Value = "Forma Pago";
                    worksheet.Cell(1, 7).Value = "Monto";
                    worksheet.Cell(1, 8).Value = "Tasa";
                    worksheet.Cell(1, 9).Value = "Tienda";

                    worksheet.RangeUsed().SetAutoFilter();

                    foreach (var item in dataListDonaciones)
                     {
                         worksheet.Cell(index + 1, 1).Style.NumberFormat.Format = "dd/mm/yyyy";
                         worksheet.Cell(index + 1, 1).Value = item.Fecha;

                         worksheet.Cell(index + 1, 2).Value = item.Fundacion;

                         worksheet.Cell(index + 1, 3).Value = item.NombreCliente;

                         worksheet.Cell(index + 1, 4).Style.NumberFormat.Format = "000";
                         worksheet.Cell(index + 1, 4).Value = item.CodigoCaja;

                         worksheet.Cell(index + 1, 5).Value = item.TurnoId;

                        worksheet.Cell(index + 1, 6).Value = item.TipoPago;

                         worksheet.Cell(index + 1, 7).Style.NumberFormat.Format = "#,##0.00";
                         worksheet.Cell(index + 1, 7).DataType = XLDataType.Number;
                         worksheet.Cell(index + 1, 7).Value = item.MontoDonado;

                         worksheet.Cell(index + 1, 8).Style.NumberFormat.Format = "#,##0.00";
                         worksheet.Cell(index + 1, 8).DataType = XLDataType.Number;
                         worksheet.Cell(index + 1, 8).Value = item.Tasa;

                         worksheet.Cell(index + 1, 9).Value = item.Tienda;

                         index++;
                     }

                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return File(content, contentType, fileName);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Error No: " + index + " -" + ex.Message);
            }
        }

        [HttpGet("Metas")]
        public IActionResult DownloadExcelMetas([FromQuery] InsideVentaViewModel filtroLF)
        {
            List<VentasMetasPorDia> dataListMetas = _reportMeta.GetListMeta(filtroLF);
            
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = "ListadoMetas.xlsx";
            int index = 1;
            try
            {
                using (var workbook = new XLWorkbook())

                {
                    IXLWorksheet worksheet =
                    workbook.Worksheets.Add("Metas");

                    //-----------Le damos el formato a la cabecera----------------
                    var rango = worksheet.Range("A1:I1"); //Seleccionamos un rango
                    rango.Style.Border.SetOutsideBorder(XLBorderStyleValues.Thick); //Generamos las lineas exteriores
                    rango.Style.Border.SetInsideBorder(XLBorderStyleValues.Medium); //Generamos las lineas interiores
                    rango.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center; //Alineamos horizontalmente
                    rango.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;  //Alineamos verticalmente
                    rango.Style.Font.FontSize = 14; //Indicamos el tamaño de la fuente
                    rango.Style.Fill.BackgroundColor = XLColor.AliceBlue; //Indicamos el color de background
                    //Ajustamos el ancho de las columnas para que se muestren todos los contenidos
                    worksheet.Column("A").Width = 12;
                    worksheet.Column("B").Width = 15;
                    worksheet.Column("C").Width = 16;
                    worksheet.Column("D").Width = 16;
                    worksheet.Column("E").Width = 15;
                    worksheet.Column("F").Width = 16;
                    worksheet.Column("G").Width = 16;
                    worksheet.Column("H").Width = 15;


                    worksheet.Cell(1, 1).Value = "Tienda";
                    worksheet.Cell(1, 2).Value = "Fecha";
                    worksheet.Cell(1, 3).Value = "Presp. Día";
                    worksheet.Cell(1, 4).Value = "Eject. Día";
                    worksheet.Cell(1, 5).Value = "Variación";
                    worksheet.Cell(1, 6).Value = "Presp. Acum.";
                    worksheet.Cell(1, 7).Value = "Eject. Acum.";
                    worksheet.Cell(1, 8).Value = "Variación Acum.";

                    worksheet.RangeUsed().SetAutoFilter();

                    foreach (var item in dataListMetas)
                    {

                        worksheet.Cell(index + 1, 1).Value = item.VentaTienda.Nombre;

                        worksheet.Cell(index + 1, 2).Style.NumberFormat.Format = "dd/mm/yyyy";
                        worksheet.Cell(index + 1, 2).Value = item.Fecha;

                        worksheet.Cell(index + 1, 3).Style.NumberFormat.Format = "#,##0.00";
                        worksheet.Cell(index + 1, 3).DataType = XLDataType.Number;
                        worksheet.Cell(index + 1, 3).Value = item.Metas.Monto;

                        worksheet.Cell(index + 1, 4).Style.NumberFormat.Format = "#,##0.00";
                        worksheet.Cell(index + 1, 4).DataType = XLDataType.Number;
                        worksheet.Cell(index + 1, 4).Value = item.VentaTienda.TotalDolares;

                        worksheet.Cell(index + 1, 5).Style.NumberFormat.Format = "###,##%";
                        worksheet.Cell(index + 1, 5).Value = item.Variacion/100;

                        worksheet.Cell(index + 1, 6).Style.NumberFormat.Format = "#,##0.00";
                        worksheet.Cell(index + 1, 6).DataType = XLDataType.Number;
                        worksheet.Cell(index + 1, 6).Value = item.PresupuestoAcum;

                        worksheet.Cell(index + 1, 7).Style.NumberFormat.Format = "#,##0.00";
                        worksheet.Cell(index + 1, 7).DataType = XLDataType.Number;
                        worksheet.Cell(index + 1, 7).Value = item.EjecutadoAcum;

                        worksheet.Cell(index + 1, 8).Style.NumberFormat.Format = "###,##%";
                        worksheet.Cell(index + 1, 8).Value = item.VariacionAcum/100;

                        index++;
                    }

                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return File(content, contentType, fileName);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Error No: " + index + " -" + ex.Message);
            }
        }


        [HttpGet("tesoreria/venta/forma/pago")]
        public IActionResult DownloadExcelDocumentTesoreriaFormasPagoPorTienda([FromQuery] TesoreriaViewModel tesoreriaViewModel)
        {
            List<ReporteExcelFormasPago> data = null;
            if (tesoreriaViewModel.TiendaId > 0)
            {
                string Url = _tiendas.GetUrlTienda(tesoreriaViewModel.TiendaId) + "inside/interno/reporte/excel/formas/pago?tiendaId=" + tesoreriaViewModel.TiendaId + "&formaPagoId=" + tesoreriaViewModel.FormaPagoId + "&fechaI=" + tesoreriaViewModel.FechaI +
                "&fechaF=" + tesoreriaViewModel.FechaF + "&agruparFp=" + tesoreriaViewModel.AgruparFp +
                "&idCaja=" + tesoreriaViewModel.IdCaja + "&idTurno=" + tesoreriaViewModel.IdTurno;
                data = JsonConvert.DeserializeObject<List<ReporteExcelFormasPago>>(_apiConectorRepository.AjaxRequest(Url));
            }else return BadRequest("Error: Por Favor Seleccione una Tienda");

            List<FormaPagoConfViewModel> dataListFormaPago = _dbContextCA.GetFormasPago();
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = "Tesoreria_FormasPago.xlsx";
            int index = 1;
            try
            {
                using (var workbook = new XLWorkbook())

                {
                    IXLWorksheet worksheet =
                    workbook.Worksheets.Add("VentasPorFormasDePago");

                    //-----------Le damos el formato a la cabecera----------------
                    var rango = worksheet.Range("A1:H1"); //Seleccionamos un rango
                    rango.Style.Border.SetOutsideBorder(XLBorderStyleValues.Thick); //Generamos las lineas exteriores
                    rango.Style.Border.SetInsideBorder(XLBorderStyleValues.Medium); //Generamos las lineas interiores
                    rango.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center; //Alineamos horizontalmente
                    rango.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;  //Alineamos verticalmente
                    rango.Style.Font.FontSize = 14; //Indicamos el tamaño de la fuente
                    rango.Style.Fill.BackgroundColor = XLColor.AliceBlue; //Indicamos el color de background
                    //Ajustamos el ancho de las columnas para que se muestren todos los contenidos
                    worksheet.Column("A").Width = 15;
                    worksheet.Column("B").Width = 15;
                    worksheet.Column("C").Width = 15;
                    worksheet.Column("D").Width = 15;
                    worksheet.Column("E").Width = 18;
                    worksheet.Column("F").Width = 18;
                    worksheet.Column("G").Width = 18;


                    worksheet.Cell(1, 1).Value = "Tienda";
                    worksheet.Cell(1, 2).Value = "Caja";
                    worksheet.Cell(1, 3).Value = "Turno";
                    worksheet.Cell(1, 4).Value = "Fecha";
                    worksheet.Cell(1, 5).Value = "Forma de Pago";
                    worksheet.Cell(1, 6).Value = "Monto";
                    worksheet.Cell(1, 7).Value = "Tipo Moneda";


                    worksheet.RangeUsed().SetAutoFilter();

                    foreach (var item in data)
                    {
                        var dato = dataListTiendas.Find(t => t.Id == item.TiendaId).Nombre;
                        worksheet.Cell(index + 1, 1).Value = dato;

                        worksheet.Cell(index + 1, 2).Style.NumberFormat.Format = "000";
                        worksheet.Cell(index + 1, 2).Value = item.Caja;

                        worksheet.Cell(index + 1, 3).Value = item.TurnoId;

                        worksheet.Cell(index + 1, 4).Style.NumberFormat.Format = "dd/mm/yyyy";
                        worksheet.Cell(index + 1, 4).Value = item.Fecha;

                        worksheet.Cell(index + 1, 5).Value = dataListFormaPago.Find(fp => fp.Id == item.FormaPagoId).Nombre;

                        worksheet.Cell(index + 1, 6).Style.NumberFormat.Format = "#,##0.00";
                        worksheet.Cell(index + 1, 6).DataType = XLDataType.Number;
                        worksheet.Cell(index + 1, 6).Value = item.Monto;

                        if(item.FormaPagoId == 12)
                            worksheet.Cell(index + 1, 7).Value = "Euro";
                        else
                            worksheet.Cell(index + 1, 7).Value = (item.CodigoMoneda == "001") ? "BsD" : "Dolar";

                        index++;
                    }

                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return File(content, contentType, fileName);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Error No: " + index + " -" + ex.Message);
            }
        }


        [HttpGet("[action]")]
        public IActionResult DownloadExcelUsuariosPos([FromQuery] ResumenCajaFiltro resumenCajaFiltro)
        {
            List<UsuarioConfViewModel> dataListUser = _usuarioRepository.GetUsuarios();
            List<FormaPagoConfViewModel> dataListFormaPago = _dbContextCA.GetFormasPago();
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = "ListadoUsuarioPOS.xlsx";
            int index = 1;
            try
            {
                using (var workbook = new XLWorkbook())

                {
                    IXLWorksheet worksheet =
                    workbook.Worksheets.Add("UsuariosPos");

                    //-----------Le damos el formato a la cabecera----------------
                    var rango = worksheet.Range("A1:G1"); //Seleccionamos un rango
                    rango.Style.Border.SetOutsideBorder(XLBorderStyleValues.Thick); //Generamos las lineas exteriores
                    rango.Style.Border.SetInsideBorder(XLBorderStyleValues.Medium); //Generamos las lineas interiores
                    rango.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center; //Alineamos horizontalmente
                    rango.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;  //Alineamos verticalmente
                    rango.Style.Font.FontSize = 14; //Indicamos el tamaño de la fuente
                    rango.Style.Fill.BackgroundColor = XLColor.AliceBlue; //Indicamos el color de background
                    //Ajustamos el ancho de las columnas para que se muestren todos los contenidos
                    worksheet.Column("A").Width = 15;
                    worksheet.Column("B").Width = 12;
                    worksheet.Column("C").Width = 12;
                    worksheet.Column("E").Width = 15;
                    worksheet.Column("F").Width = 18;
                    worksheet.Column("G").Width = 18;


                    worksheet.Cell(1, 1).Value = "Cedula";
                    worksheet.Cell(1, 2).Value = "Nombres";
                    worksheet.Cell(1, 3).Value = "Apellidos";
                    worksheet.Cell(1, 4).Value = "Usuario";
                    worksheet.Cell(1, 5).Value = "Clave";
                    worksheet.Cell(1, 6).Value = "Perfil";
                    worksheet.Cell(1, 7).Value = "Sucursal";

                    worksheet.RangeUsed().SetAutoFilter();

                    foreach (var item in dataListUser)
                    {
                        if ((item.Tipo >= 2 && item.Tipo <= 3) && item.Estatus == 1 )
                        {
                            worksheet.Cell(index + 1, 1).Value = item.Cedula;

                            //worksheet.Cell(index + 1, 2).Style.NumberFormat.Format = "dd/mm/yyyy";
                            worksheet.Cell(index + 1, 2).Value = item.Nombre;

                            worksheet.Cell(index + 1, 3).Value = item.Apellido;

                            worksheet.Cell(index + 1, 4).Value = item.Nick;

                            worksheet.Cell(index + 1, 5).Value = item.Nombre.Trim().ElementAt(0).ToString() + item.Apellido.Split(' ')[0];

                            string Perfil = "";
                            if (item.Tipo == 3 && item.RolId == 5)
                            {
                                Perfil = "CAJERA";
                            }
                            else
                            if (item.Tipo == 3 && item.RolId == 3)//Supervisira de caja
                            {
                                Perfil = "SUPERVISOR DE CAJA OPERATIVA";
                            }
                            else
                            if (item.Tipo == 3 && item.RolId == 2)//caja farmacia
                            {
                                Perfil = "CAJERA DE FARMACIA";
                            }
                            else
                            if (item.Tipo == 3 && item.RolId == 8)//Regente farmacia
                            {
                                Perfil = "REGENTE DE FARMACIA";
                            }
                            else
                            if (item.Tipo == 3 && item.RolId == 3)//RCaja Admin
                            {
                                Perfil = "SUPERVISOR DE CAJA OPERATIVA";
                            }
                            else
                            if (item.Tipo == 2 && item.RolId == 3)//RCaja Admin
                            {
                                Perfil = "JEFE DE CAJA OPERATIVA";
                            }
                            else
                            if (item.Tipo == 21 && item.RolId == 3)//RCaja Admin
                            {
                                //dmu.Perfil.Contains("")
                                Perfil = "ANALISTA DE RECAUDACION Y CONTROL";
                            }

                            worksheet.Cell(index + 1, 6).Value = Perfil;

                            var dato = dataListTiendas.Find(t => t.Id > 0).Nombre;
                            worksheet.Cell(index + 1, 7).Value = dato;

                            //worksheet.Cell(index + 1, 6).Style.NumberFormat.SetFormat("#.##0,#0");
                            //worksheet.Cell(index + 1, 8).Style.NumberFormat.Format = "#,##0.00";
                            //worksheet.Cell(index + 1, 8).DataType = XLDataType.Number;
                            //worksheet.Cell(index + 1, 8).Value = item.Procesado;

                            //worksheet.Cell(index + 1, 9).Style.NumberFormat.Format = "#,##0.00";
                            //worksheet.Cell(index + 1, 9).DataType = XLDataType.Number;
                            //worksheet.Cell(index + 1, 9).Value = item.Declarado;


                            //worksheet.Cell(index + 1, 10).Style.NumberFormat.Format = "#,##0.00";
                            //worksheet.Cell(index + 1, 10).DataType = XLDataType.Number;
                            //worksheet.Cell(index + 1, 10).Value = (item.Declarado - item.Procesado);

                            index++;
                        }
                    }

                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return File(content, contentType, fileName);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Error No: " + index + " -" + ex.Message);
            }
        }
        
    }


}
