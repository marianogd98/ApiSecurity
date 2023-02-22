using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPosRio.Models.DB;
using WebApiPosRio.Models.Repository;
using WebApiPosRio.Models.Response;
using WebApiPosRio.Models.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiPosRio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ContabilidadController : ControllerBase
    {
        private readonly ICajaAdministrativaRepository _dbContextCA;
        private readonly IUsuarioRepository _usuarioRep;
        private readonly ICajaRepository _cajaRep;

        public ContabilidadController(ICajaAdministrativaRepository caRepository, IUsuarioRepository usuarioRepository, ICajaRepository cajaRepository)
        {
            _dbContextCA = caRepository;
            _usuarioRep = usuarioRepository;
            _cajaRep = cajaRepository;
        }

        [HttpGet("reporte/formas/pago")]//busca Formas Pago por filtro y muestra listado
        public IActionResult ReportesFormasPago([FromQuery]  TesoreriaViewModel tesoreriaViewModel, int page = 1, int perPage = 10)
        {
            try
            {
                List<SpReporteFormasPago> dataFormasPago = _dbContextCA.ReporteFormasPagoRangFech(tesoreriaViewModel);

                if (tesoreriaViewModel.AgruparFp==1)
                {
                    var result = (from item in dataFormasPago
                                 group item by item.FormaPagoId into g
                                 select new SpReporteFormasPago()
                                 {
                                     FormaPagoId = g.Key,
                                     Monto = g.Sum(x => x.Monto),
                                     TurnoId = 0,
                                     Tienda = g.Max(x => x.Tienda),
                                     CajaId = 0,
                                     FormaPago = g.Max(x=>x.FormaPago),
                                     CodigoMoneda = g.Max(x => x.CodigoMoneda),
                                     TiendaId = g.Max(x => x.TiendaId),
                                     Fecha = g.Max(x => x.Fecha)
                                 }).ToList();

                    dataFormasPago = result;
                }

                //sumatoria tipo moneda
                var MontoTotalBs = dataFormasPago.Where(dfp => dfp.CodigoMoneda=="001").Sum(x => x.Monto);
                var MontoTotalDolar = dataFormasPago.Where(dfp => dfp.CodigoMoneda == "002").Sum(x => x.Monto);
                var paginacion = dataFormasPago.ToPagedList(page, perPage);
                var paginateData = new
                {
                    ListadoFormasPago = paginacion,
                    TotalDolar = MontoTotalDolar,
                    TotalBs = MontoTotalBs,
                    totalrow = paginacion.TotalItemCount
                };

                return (paginateData != null) ? Ok(paginateData) : BadRequest(paginateData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("resumen/turnos")]//busca Formas Pago por filtro y muestra listado
        public IActionResult ResumenCajaXTurnosEstatus([FromQuery] ResumenCajaFiltro resumenCajaFiltro, int page = 1, int perPage = 10)
        {
            try
            {
                List<SpResumenxCajaxFecha> dataResumen = _dbContextCA.ResumenCajaXTurnosEstatus(resumenCajaFiltro.Fecha);
               
                if (resumenCajaFiltro.IdCaja > 0)
                {
                    dataResumen = dataResumen.Where(dfp => dfp.CajaId == resumenCajaFiltro.IdCaja).ToList();
                }

                if (resumenCajaFiltro.EstatusDepo > 0)
                {
                    dataResumen = dataResumen.Where(dfp => dfp.EstatusArqueo == resumenCajaFiltro.EstatusDepo).ToList();
                }

                if (resumenCajaFiltro.EstatusTurno > 0)
                {
                    dataResumen = dataResumen.Where(dfp => dfp.EstatusTurno == resumenCajaFiltro.EstatusTurno).ToList();
                }

                //paginacion de data
                var paginacion = dataResumen.ToPagedList(page, perPage);
                 var paginateData = new
                 {
                     ResumenCajasTurno = paginacion,
                     totalrow = paginacion.TotalItemCount
                 };

                 return (paginateData != null) ? Ok(paginateData) : BadRequest(paginateData);                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("resumen/caja/formapago")]//busca Formas Pago por filtro y muestra listado
        public IActionResult ResumenCajaXTurnosDecProc([FromQuery] ResumenCajaFiltro resumenCajaFiltro, int page = 1, int perPage = 10)
        {
            try
            {
                ResponseData dataP = _dbContextCA.ListadoDeclaradoProcesado(resumenCajaFiltro, page, perPage);

                return (dataP != null) ? Ok(dataP) : BadRequest(dataP);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // GET: api/<ContabilidadController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ContabilidadController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ContabilidadController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ContabilidadController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ContabilidadController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
