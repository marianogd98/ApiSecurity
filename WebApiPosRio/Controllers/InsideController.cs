using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiPosRio.Models.Repository;
using WebApiPosRio.Models.ViewModels;

namespace WebApiPosRio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class InsideController : ControllerBase
    {
        private readonly IInsideRepository _insideDashboard;
        private IPromocionesRepository _promociones;
        private readonly ICajaAdministrativaRepository _dbContextCA;
        private IWebHostEnvironment _hostingEnv;
        private ITiendaRepository _tiendaRepository;
        private IApiConectorRepository _apiConectorRepository;

        [Obsolete]
        public InsideController(IInsideRepository insideRepository, IPromocionesRepository promociones, IWebHostEnvironment webHostEnvironment, IApiConectorRepository apiConectorRepository, ICajaAdministrativaRepository cajaAdministrativaRepository, ITiendaRepository tiendaRepository)
         {
             _insideDashboard = insideRepository;
            _promociones = promociones;
            _hostingEnv = webHostEnvironment;
            _dbContextCA = cajaAdministrativaRepository;
            _apiConectorRepository = apiConectorRepository;
            _tiendaRepository = tiendaRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetVentasMensuales([FromQuery] InsideViewModel  insideFiltro)
        {  //grafica de ventas mensuales
            var data = _insideDashboard.Consolidado(insideFiltro.FechaIni, insideFiltro.FechaFin);

            if (data != null)
            {
                return Ok(data);
            }
            else
            {
                return BadRequest("No hay datos");
            }
        }

        [HttpGet("interno")]
        [AllowAnonymous]
        public IActionResult GetInternoVentasMensuales([FromQuery] InsideViewModel insideFiltro)
        {  //grafica de ventas mensuales
            //insideFiltro.Url = _apiConectorRepository.GetUrlTienda(insideFiltro.TiendaId) + "Inside?FechaIni="+ insideFiltro.FechaIni + "&FechaFin="+ insideFiltro.FechaFin; 
            //var data = _apiConectorRepository.AjaxRequest(insideFiltro.Url); //solo para inside
            var data = _insideDashboard.Consolidado(insideFiltro.FechaIni, insideFiltro.FechaFin); //solo para tienda

            if (data != null)
            {
                return Ok(data);
            }
            else
            {
                return BadRequest("No hay datos");
            }
        }

        [HttpGet("[action]")]
        [AllowAnonymous]
        public IActionResult ConsolidadoTienda([FromQuery] InsideViewModel insideFiltro)
        {
            //insideFiltro.Url = "http://" + insideFiltro.Url + "?FechaIni=" + insideFiltro.FechaIni + "&FechaFin=" + insideFiltro.FechaFin;
            //var data = _apiConectorRepository.AjaxRequest(insideFiltro.Url); //solo para inside
            var data = _insideDashboard.MostrarConsolidadoTienda(insideFiltro.FechaIni, insideFiltro.FechaFin); //solo para tienda

            if (data != null)
            {
                return Ok(data);
            }
            else
            {
                return BadRequest("No hay datos");
            }
        }

        [HttpPost("galeria/upload")]
        public async Task<IActionResult> UploadGaleria(IFormFile file)
        {
            if(file==null) return BadRequest(new
            {
                status = 0,
                message = "Debe subir una imagen en formato JPG"
            });

            if (Path.GetExtension(file.FileName).ToLower() == ".jpg" || Path.GetExtension(file.FileName).ToLower() == ".jpeg")
            {
                try
                {
                    string contentRootPath = _hostingEnv.ContentRootPath;

                    string pathToFiles = Path.Combine(contentRootPath, "images\\galeria\\");
                    var path = Path.Combine(pathToFiles, $"{DateTime.Now.ToString("ddMMyyyyfff")}{Path.GetExtension(file.FileName)}");
                    var stream = new FileStream(path, FileMode.Create);
                    await file.CopyToAsync(stream);
                    return Ok(new
                    {
                        status = 1,
                        message = "Imagen subida con exito"
                    });
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return BadRequest(new
                {
                    status = 0,
                    message = "Las imágenes deben estar en formato JPG o JPEG"
                });
            }
               
            
        }

        [HttpPost("galeria/activa/upload")]
        public async Task<IActionResult> UploadGaleriaActiva([FromBody] BodyUploadImageStore ImagesList)
        {
            if (!(ImagesList.ImagesList.Count > 0))
                return BadRequest(new
                {
                    status = 0,
                    message = "No ha subido ninguna imagen"
                });

            string contentRootPath = _hostingEnv.ContentRootPath;// _hostingEnvironment.ContentRootPath;

            string pathGaleria = Path.Combine(contentRootPath, "images\\galeria\\");

            string destFile = Path.Combine(contentRootPath, "images\\galeria-activa\\");
            if (!(Directory.Exists(destFile)))
            {
                Directory.CreateDirectory(destFile);
            }

            try
            {

                var imagesActuality = Directory.EnumerateFiles(destFile, "*");
                foreach (var img in imagesActuality)
                {
                    System.IO.File.Delete(img);
                }


                var imagesFile = Directory.EnumerateFiles(pathGaleria, "*");
                ImagesList.ImagesList.ForEach(img => {
                    foreach (var i in imagesFile)
                    {
                        var filename = i.Substring(pathGaleria.Length);
                        if (filename == img.Slug)
                            System.IO.File.Copy(i, Path.Combine(destFile, $"{DateTime.Now.ToString("ddMMyyyyHHmmssfff")}{Path.GetExtension(filename)}"));
                    }
                });

                return Ok(new
                {
                    status = 1,
                    message = "Imágenes actualizadas con éxito"
                });
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("galeria")]
        public async Task<IActionResult> GetGaleria()
        {
            try
            {
                string webRootPath = _hostingEnv.WebRootPath;

                string pathGaleria = Path.Combine(_hostingEnv.ContentRootPath, "images\\galeria");

                if (!(Directory.Exists(pathGaleria)))
                {
                    Directory.CreateDirectory(pathGaleria);
                }

                var imageFiles = Directory.EnumerateFiles(pathGaleria, "*");

                List<ImageDataViewModel> images = new List<ImageDataViewModel>();

                foreach (string currentFile in imageFiles)
                {
                    string fileName = currentFile.Substring(pathGaleria.Length + 1);
                    images.Add(new ImageDataViewModel() { Url = Path.Combine(webRootPath, "images/galeria/", fileName.Trim('"')), Slug = fileName.Trim('"'), Estatus = false });
                }

                if (images.Count <= 0)
                    return BadRequest("No hay imagenes en la Galería");

                return Ok(images);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("galeria/activa")]
        public async Task<IActionResult> GetGaleriaActiva()
        {
            try
            {
                string webRootPath = _hostingEnv.WebRootPath; 

                string pathGaleria = Path.Combine(_hostingEnv.ContentRootPath /*_hostingEnvironment.ContentRootPath*/, "images\\galeria-activa");
                
                if (!(Directory.Exists(pathGaleria)))
                {
                    Directory.CreateDirectory(pathGaleria);
                }

                var imageFiles = Directory.EnumerateFiles(pathGaleria, "*");

                List<ImageDataViewModel> images = new List<ImageDataViewModel>();

                foreach (string currentFile in imageFiles)
                {
                    string fileName = currentFile.Substring(pathGaleria.Length + 1);
                    images.Add(new ImageDataViewModel() { Url = Path.Combine(webRootPath, "images/galeria-activa/", fileName.Trim('"')), Slug = fileName.Trim('"'), Estatus = false });
                }

                if (images.Count <= 0)
                    return BadRequest("No hay imagenes en la galeria");

                return Ok(images);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("promocion")]
        public IActionResult CrearPromocion()
        {
            var file = HttpContext.Request.Form.Count > 0 ? HttpContext.Request.Form : null;

            PromocionDto promo = new PromocionDto();
            promo.Tipo = int.Parse(HttpContext.Request.Form["tipo"]);
            promo.Titulo = HttpContext.Request.Form["titulo"];
            promo.FechaInicial = Convert.ToDateTime(HttpContext.Request.Form["fechaI"]);
            promo.FechaFinal = Convert.ToDateTime(HttpContext.Request.Form["fechaF"]);
            promo.monto = Convert.ToDouble(HttpContext.Request.Form["monto"]);
            promo.ProductsCvs = HttpContext.Request.Form.Files[0];

            var result = new StringBuilder();
            List<string[]> lines = new List<string[]>();
            using (var reader = new StreamReader(promo.ProductsCvs.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                {
                    var line = reader.ReadLine().Trim();
                    line = string.Join("", line.Split('@', '.', ';', '\'', '\\', '/'));
                    //result.AppendLine(reader.ReadLine() + "\n");
                    lines.Add(line.Split(','));
                }
            }

            return Ok(lines);
        }

        [HttpGet("promocion/sorpresa")]
        public IActionResult PromocionMesSorpresaFecha([FromQuery] DateTime fechaI, DateTime fechaF)
        {
            var promo = _promociones.GetPromoSeptiembreFechas(fechaI, fechaF);

            if (promo.Count() > 0)
                return Ok(promo);
            else
                return BadRequest("No hay una promocion activa");

        }

        [HttpPut("promocion/sorpresa")]
        public IActionResult ActualizarPromociones([FromBody] UpdatePromocion dia)
        {
            var promocionDia = _promociones.GetPromoById(dia.id);
            if (promocionDia == null)
                return BadRequest("No existe promocion para el dia");

            promocionDia.TiendaId = dia.tienda;
            promocionDia.Hora = dia.hora;
            promocionDia.UpdatedAt = DateTime.Now;
            return (_promociones.ActualizarPromo(promocionDia)) ? Ok("Actualizacion realizada...") : BadRequest();

        }

        [HttpGet("venta/departamentos")]
        [AllowAnonymous]
        public IActionResult GetVentasDepartamento([FromQuery] InsideViewModel insideFiltro)        
        {  //Ventas por departamento
            var data = _insideDashboard.MostrarVentaDepartamento(insideFiltro.FechaIni, insideFiltro.FechaFin);

            if (data != null)
            {
                return Ok(data);
            }
            else
            {
                return BadRequest("No hay datos");
            }
        }

        [HttpGet("listado/facturas")]
        [AllowAnonymous]
        public IActionResult GetListadoFacturas([FromQuery] string fechaIni)
        {  //Listado de facturas y tickets
            var data = _insideDashboard.MostrarFacturaPromedios(fechaIni);

            if (data != null)
            {
                return Ok(data);
            }
            else
            {
                return BadRequest("No hay datos");
            }
        }

        [HttpGet("listado/formapago")]
        [AllowAnonymous]
        public IActionResult GetListadoFormaPago([FromQuery] string fechaIni, string fechaFin, int tiendaId)
        {  //Listado de facturas y tickets
            var data = _insideDashboard.MostrarFormasPagoInside(fechaIni, fechaFin, tiendaId);

            if (data != null)
            {
                return Ok(data);
            }
            else
            {
                return BadRequest("No hay datos");
            }
        }

        [HttpGet("listado/ventas")]
        [AllowAnonymous]
        public IActionResult GetListadoVentas([FromQuery] InsideVentaViewModel insideFiltro)
        {   //Ventas por departamento
            var data = _insideDashboard.MostrarVentaDepartamento(insideFiltro);

            if (data != null)
            {
                return Ok(data);
            }
            else
            {
                return BadRequest("No hay datos");
            }
        }

        [HttpGet("ventas/departamento/inside")]
        [AllowAnonymous]
        public IActionResult GetListadoDepartamentosVentas([FromQuery] InsideVentaViewModel insideFiltro)
        {   //Ventas por departamento Inside
            var data = _insideDashboard.ListadoVentaDepartamentoInside(insideFiltro);

            if (data != null)
            {
                return Ok(data);
            }
            else
            {
                return BadRequest("No hay datos");
            }
        }

        [HttpGet("ventas/top/productos")]
        [AllowAnonymous]
        public IActionResult GetListadoResumenTopVentaProducto([FromQuery] InsideVentaViewModel insideFiltro)
        {  //Top Ventas x productos
            var data = _insideDashboard.ListadoResumenTopVentaProducto(insideFiltro);

            if (data != null)
            {
                return Ok(data);
            }
            else
            {
                return BadRequest("No hay datos");
            }
        }


        #region Interno
        [HttpGet("[action]")]
        [AllowAnonymous]
        public IActionResult InternoConsolidadoTienda([FromQuery] InsideViewModel insideFiltro)
        {
            //insideFiltro.Url = insideFiltro.Url + "?FechaIni=" + insideFiltro.FechaIni + "&FechaFin=" + insideFiltro.FechaFin; 
            //var data = _apiConectorRepository.AjaxRequest(insideFiltro.Url); //solo para inside
            var data = _insideDashboard.MostrarConsolidadoTienda(insideFiltro.FechaIni, insideFiltro.FechaFin); //solo para tienda

            if (data != null)
            {
                return Ok(data);
            }
            else
            {
                return BadRequest("No hay datos");
            }
        }

        [HttpGet("interno/venta/departamentos")]
        public IActionResult GetInternoVentasDepartamento([FromQuery] InsideViewModel insideFiltro)
        {  //Ventas por departamento
            
            insideFiltro.Url = _apiConectorRepository.GetUrlTienda(insideFiltro.TiendaId) + "inside/venta/departamentos?FechaIni=" + insideFiltro.FechaIni + "&FechaFin=" + insideFiltro.FechaFin;
            var data = _apiConectorRepository.AjaxRequest(insideFiltro.Url);
            if (data != null)
            {
                if (data.Contains("(400) Bad Request")) return BadRequest("Error en servidor Externo consultando tienda: "+ insideFiltro.TiendaId);

                return Ok(data);
            }
            else
            {
                return BadRequest("No hay datos");
            }
        }

        [HttpGet("CajaAdministrativa/Reporte/Caja")]
        public IActionResult GetReporteCajaAdministrativa([FromQuery] InsideViewModel insideFiltro)
        {  //Ventas por departamento
            //insideFiltro.Url = _apiConectorRepository.GetUrlTienda(insideFiltro.TiendaId) + "CajaAdministrativa/ReporteCierreCaja/fecha/" + insideFiltro.FechaIni + "/caja/0"; 
            //var data = _apiConectorRepository.AjaxRequest(insideFiltro.Url); //solo para inside
            var data = _insideDashboard.MostrarVentaDepartamento(insideFiltro.FechaIni, insideFiltro.FechaFin); //solo para tienda

            if (data != null)
            {
                return Ok(data);
            }
            else
            {
                return BadRequest("No hay datos");
            }
        }

        [HttpGet("interno/resumen/caja/formapago")]
        public IActionResult GetResumenCajaFormaPago([FromQuery] ResumenCajaFiltro resumenCajaFiltro, string Url, int page = 1, int perPage = 10)
        {  //Resumen Caja Forma Pago
            string urlData = JsonConvert.SerializeObject(resumenCajaFiltro);

            Url = Url + "Contabilidad/resumen/caja/formapago?page=" + page + "&perPage=" + perPage + "&" + _apiConectorRepository.CleanUrl(urlData);
            var data = _apiConectorRepository.AjaxRequest(Url);

            if (data != null)
            {
                return Ok(data);
            }
            else
            {
                return BadRequest("No hay datos");
            }
        }

        [HttpGet("interno/CajaAdministrativa/FacturadoXCajaXFecha")]
        public IActionResult FacturadoXCajaXFecha([FromQuery] ParamsLfViewModel paramsLfViewModel, int page = 1, int perPage = 10)
        {//Arqueo de Caja
            if (paramsLfViewModel.TiendaId != null)
            {
                string urlData = JsonConvert.SerializeObject(paramsLfViewModel);

                string Url = _tiendaRepository.GetUrlTienda(int.Parse(paramsLfViewModel.TiendaId)) + "CajaAdministrativa/FacturadoXCajaXFecha?page=" + page + "&perPage=" + perPage + "&" + _apiConectorRepository.CleanUrl(urlData);
                var data = _apiConectorRepository.AjaxRequest(Url);

                if (data != null)
                {
                    return Ok(data);
                }
                else
                {
                    return BadRequest("No hay datos");
                }
            }
            return BadRequest("No ha seleccionado tienda");
        }

        [HttpGet("interno/CajaAdministrativa/ResumenDesgloseDE")]
        public IActionResult ResumenDesgloseDE([FromQuery] string Fecha, string TiendaId)
        {//Arqueo de Caja
            if (TiendaId != null)
            {
                string Url = _tiendaRepository.GetUrlTienda(int.Parse(TiendaId)) + "CajaAdministrativa/ResumenDesgloseDE?Fecha=" + Fecha;
                var data = _apiConectorRepository.AjaxRequest(Url);

                if (data != null)
                {
                    return Ok(data);
                }
                else
                {
                    return BadRequest("No hay datos");
                }
            }

            return BadRequest("No ha seleccionado tienda");

        }

        [HttpGet("interno/CajaAdministrativa/ResumenDesglosePuntos/Fecha/{Fecha}/TiendaId/{TiendaId}")]
        public IActionResult ResumenDesglosePuntos(string Fecha, string TiendaId)
        {//Arqueo de Caja
            if (TiendaId != null)
            {
                string Url = _tiendaRepository.GetUrlTienda(int.Parse(TiendaId)) + "CajaAdministrativa/ResumenDesglosePuntos?Fecha=" + Fecha;
                var data = _apiConectorRepository.AjaxRequest(Url);

                if (data != null)
                {
                    return Ok(data);
                }
                else
                {
                    return BadRequest("No hay datos");
                }
            }

            return BadRequest("No ha seleccionado tienda");

        }

        [HttpGet("interno/CajaAdministrativa/ReporteCierreCaja/fecha/{Fecha}/caja/{caja}/TiendaId/{TiendaId}")]
        public IActionResult ReporteCierreCaja(string Fecha, string Caja,string TiendaId )
        {//Arqueo de Caja
            if (TiendaId != null)
            {
                string Url = _tiendaRepository.GetUrlTienda(int.Parse(TiendaId)) + "CajaAdministrativa/ReporteCierreCaja/Fecha/" + Fecha +"/caja/0";
                var data = _apiConectorRepository.AjaxRequest(Url);

                if (data != null)
                {
                    return Ok(data);
                }
                else
                {
                    return BadRequest("No hay datos");
                }
            }

            return BadRequest("No ha seleccionado tienda");

        }

        // GET api/<CajaController>/5
        [HttpGet("interno/tienda/{TiendaId}")]
        public IActionResult GetInternoCajaTienda(int TiendaId)
        {
            if (TiendaId > 0)
            {
                string Url = _tiendaRepository.GetUrlTienda(TiendaId) + "Caja/tienda/" + TiendaId;
                var data = _apiConectorRepository.AjaxRequest(Url);

                if (data != null)
                {
                    return Ok(data);
                }
                else
                {
                    return BadRequest("No hay datos");
                }
            }

            return BadRequest("No ha seleccionado tienda");
        }

        [HttpGet("interno/Contabilidad/reporte/formas/pago")]
        public IActionResult ContabilidadReportesFormasPago([FromQuery] TesoreriaViewModel tesoreriaViewModel, int page = 1, int perPage = 10)
        {//Arqueo de Caja
            if (tesoreriaViewModel.TiendaId >0)
            {
                string Url = _tiendaRepository.GetUrlTienda(tesoreriaViewModel.TiendaId) + "Contabilidad/reporte/formas/pago?page=" + page +"&perPage="+ perPage+
                            "&tiendaId="+ tesoreriaViewModel.TiendaId + "&formaPagoId="+tesoreriaViewModel.FormaPagoId+ "&fechaI="+ tesoreriaViewModel.FechaI+
                            "&fechaF="+ tesoreriaViewModel.FechaF + "&agruparFp="+ tesoreriaViewModel.AgruparFp +
                            "&idCaja="+ tesoreriaViewModel.IdCaja + "&idTurno="+ tesoreriaViewModel.IdTurno;
                var data = _apiConectorRepository.AjaxRequest(Url);

                if (data != null)
                {
                    return Ok(data);
                }
                else
                {
                    return BadRequest("No hay datos");
                }
            }

            return BadRequest("No ha seleccionado tienda");

        }

        [HttpGet("interno/reporte/excel/formas/pago")]
        public IActionResult ExcelReportesFormasPago([FromQuery] TesoreriaViewModel tesoreriaViewModel)
        {//Arqueo de Caja
            if (tesoreriaViewModel.TiendaId > 0)
            {
                var data = _insideDashboard.ListadoTesoreriaFormasPagoPorTienda(tesoreriaViewModel);

                if (data != null)
                {
                    return Ok(data);
                }
                else
                {
                    return BadRequest("No hay datos");
                }
            }

            return BadRequest("No ha seleccionado tienda");

        }

        #endregion

    }
}
