using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using System.Linq;
using System;
using System.Collections.Generic;
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
    public class CajaAdministrativaController : ControllerBase
    {
        private readonly ICajaAdministrativaRepository _dbContextCA;
        private readonly IUsuarioRepository _usuarioRep;
        private readonly ICajaRepository _cajaRep;
        private readonly IClienteRepository _clienteRepo;
        public CajaAdministrativaController(ICajaAdministrativaRepository caRepository, IUsuarioRepository usuarioRepository, ICajaRepository cajaRepository, IClienteRepository clienteRepository)
        {
            _dbContextCA = caRepository;
            _usuarioRep = usuarioRepository;
            _cajaRep = cajaRepository;
            _clienteRepo = clienteRepository;
        }
        // GET: api/<CajaAdministrativaController>
        [HttpGet("[action]")]
        public ResponseData DetallePagos()
        {
            return null; // _dbContextCA.DetallePagos();
        }

        [HttpGet("[action]")]
        public ResponseData DetalleMoneda()
        {
            return _dbContextCA.DetalleMoneda();
        }

        [HttpGet("[action]")]
        public ResponseData DetalleFormaPago(int IdTurno, string CodMoneda="0")
        {
            return _dbContextCA.DetalleFormaPago(CodMoneda, IdTurno);
        }

        [HttpGet("[action]")]
        public ResponseData DetalleFormaPagoTurnoVerificado(int IdTurno, string CodMoneda = "0")
        {
            return _dbContextCA.DetalleFormaPagoTurnoVerificado(CodMoneda, IdTurno);
        }

        [HttpGet("[action]")]
        public ResponseData FacturadoXCajaXFecha([FromQuery] ParamsLfViewModel paramsLfViewModel)
        {

            return _dbContextCA.FacturadoXCajaXFecha(paramsLfViewModel);
        }

        [HttpGet("[action]")]
        public ResponseData FacturadoXTurnoXFecha(string fechaActual, string caja, int page= 1, int perPage = 10)
        {
            return _dbContextCA.FacturadoXTurnoXFecha(fechaActual, page, perPage, caja);
        }

        [HttpGet("[action]")]
        public ResponseData ListadoFactura([FromQuery]  ParamsLfViewModel paramsLfViewModel)
        {
            return _dbContextCA.ListadoFactura(paramsLfViewModel);
        }

        [HttpGet("[action]")]
        public ResponseData DetalleFactura([FromQuery] DetalleFacturaviewModel detalleFacturaviewModel)
        {
            return _dbContextCA.DetalleFactura(detalleFacturaviewModel);
        }

        [HttpGet("FormaPago/Factura/{IdFactura}")]
        public ResponseData FormaPagoFacturaId(int IdFactura)
        {
            return _dbContextCA.DetalleFormaPagoFacturaId(IdFactura);
        }

        [HttpGet("[action]/{turnoId}")]
        public ResponseData DetalleCajeraTurno(int turnoId)
        {
            var dataCajera = _usuarioRep.GetUserTurno(turnoId);
            var dataCaja = _cajaRep.GetIdCajaAsync(turnoId, true);

            if (dataCajera.Success == 1)
            {
                dataCajera.Data = new
                {
                    CajeraTurno = dataCajera.Data,
                    CajaTurno = dataCaja
                };
            }

            return dataCajera;
        }

        [HttpGet("[action]/{IdTurno}")]
        public ResponseData ReporteCierreTurno(int IdTurno)
        {
            return _dbContextCA.ReporteCierreTurno(IdTurno);
        }

        [HttpGet("[action]/fecha/{fecha}")]
        public ResponseData ReporteCierreTurnos(string fecha)
        {
            return _dbContextCA.ReporteCierreTurnosS4(fecha);
        }

        [HttpGet("[action]/fecha/{Fecha}/caja/{CodCaja}")]
        public ResponseData ReporteCierreCaja(string Fecha, string CodCaja = "")
        {
            return _dbContextCA.ReporteCierreCaja(CodCaja, Fecha);
        }

        [HttpGet("[action]")]
        public IActionResult ReporteFaltantesSobrantes(int IdTurno, int IdCaja, int IdUsuario, string nombreCajera, string FormaPagoId,string fechaI, string fechaF, int page=1, int perPage = 10)
        {
            var dataSobFalt = _dbContextCA.ListadoFaltantesSobrantes(IdTurno, IdCaja, IdUsuario, nombreCajera, FormaPagoId,fechaI, fechaF, page, perPage);
            return (dataSobFalt.Success == 1) ? Ok(dataSobFalt) : BadRequest(dataSobFalt);
        }

        [HttpGet("[action]")]//busca facturas por filtro y muestra listado
        public IActionResult ReporteFacturas([FromQuery] FiltroListadoFactura filtroListadoFactura/*string NroDocumento, string Cedula ,string NroFactura, string ZetaSerial,string codigoCaja, string nombreCajera, int Estatus, string fechaI, string fechaF*/, int page = 1, int perPage = 10)
        {
            try
            {
                int IdRecup = 0;
                if (filtroListadoFactura.CodigoCaja != null)
                {
                    IdRecup = _cajaRep.GetIdCajaByCodCaja(filtroListadoFactura.CodigoCaja);
                }

                List<SpListadoFacturaRangFech> dataFacturas = _dbContextCA.ListadoFacturaRangFech(filtroListadoFactura);

                if (IdRecup > 0 && dataFacturas !=null)//filtrar por id caja
                {
                    dataFacturas = dataFacturas.FindAll(df => df.CajaId == IdRecup);
                    IdRecup = 0;
                }

                if (filtroListadoFactura.FormaPagoId>0)
                {
                    dataFacturas = dataFacturas.FindAll(df => df.FormaPagoId.Contains(filtroListadoFactura.FormaPagoId.ToString()));
                }

                //--------------------------------------------------------------
                if (filtroListadoFactura.NombreCajera != null && dataFacturas != null)//buscar idcajera por nombre cajera y filtrar
                {
                    IdRecup = _usuarioRep.getIdUserbyName(filtroListadoFactura.NombreCajera);
                    dataFacturas = dataFacturas.FindAll(df => df.CajeraId == IdRecup);
                    IdRecup = 0;
                }

                //--------------------------------------------------------------
                if (filtroListadoFactura.Monto >0 && dataFacturas != null)//buscar idcajera por nombre cajera y filtrar
                {
                    dataFacturas = dataFacturas.FindAll(df => df.MontoPagado >= filtroListadoFactura.Monto ).OrderBy(x => x.MontoPagado).ToList();
                    IdRecup = 0;
                }

                //--------------------------------------------------------------
                if (filtroListadoFactura.Cedula != null && dataFacturas != null)//buscar idcliente por cedula y filtrar
                {
                    IdRecup = _clienteRepo.GetIdClienteByCedula(filtroListadoFactura.Cedula);
                    dataFacturas = dataFacturas.FindAll(df => df.ClienteId == IdRecup);
                    IdRecup = 0;
                }

                var paginacion = dataFacturas.ToPagedList(page, perPage);
                var paginateData = new
                {
                    Facturas = paginacion,
                    totalrow = paginacion.TotalItemCount
                };
                return (paginateData != null) ? Ok(paginateData) : BadRequest(paginateData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
            
        }

        // GET api/<CajaAdministrativaController>/5
        /*[HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }*/

        // POST api/<CajaAdministrativaController>
        [HttpPost]
        public IActionResult Post([FromBody] CajaAdmViewModel Factura)
        {
            ResponseData value = _dbContextCA.UpdateFacturaEstatus(Factura);
            return (value.Success == 1) ? Ok(value) : BadRequest(value);
        }

        // POST api/<CajaAdministrativaController>
        [HttpPost("[action]")]
        public IActionResult ResetArqueo([FromBody] DataViewModel Arqueo)
        {
            ResponseData value = _dbContextCA.ResetArqueo(Arqueo.CampoId, Arqueo.UserId);
            return (value.Success == 1) ? Ok(value) : BadRequest(value);
        }
        // POST api/<CajaAdministrativaController>
        [HttpPost("[action]")]
        public IActionResult AnularFactura([FromBody] CajaAdmViewModel Factura)
        {
            ResponseData value = _dbContextCA.AnularFactura(Factura);
            return (value.Success == 1) ? Ok(value) : BadRequest(value);
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public IActionResult ArqueoCaja([FromBody] ArqueoViewModel arqueoViewModel)
        {
            ResponseData value = _dbContextCA.AddArqueo(arqueoViewModel);
            return (value.Success == 1) ? Ok(value) : BadRequest(value);
        }

        [HttpGet("[action]/{idTurno}/user/{idUsuario}")]
        public IActionResult ValidarArqueoCaja(int idTurno, int idUsuario)
        {
            ResponseData value = _dbContextCA.UpdateArqueoEstatus(idTurno, idUsuario);
            return (value.Success == 1) ? Ok(value) : BadRequest(value);
        }

        [HttpGet("[action]/{idArqueo}")]
        public IActionResult DesgloseDolarArqueo(int idArqueo)
        {
            ResponseData value = _dbContextCA.DesgloseDolarArqueo(idArqueo);
            return (value.Success == 1) ? Ok(value) : BadRequest(value);
        }

        [HttpGet("[action]/{idTurno}")]
        public IActionResult DesgloseDolarArqueoByTurno(int idTurno)
        {
            var Arqueo = _dbContextCA.GetArqueo(idTurno);
            ResponseData value = _dbContextCA.DesgloseDolarArqueo(Arqueo.Id);
            return (value.Success == 1) ? Ok(value) : BadRequest(value);
        }

        [HttpGet("[action]")]
        public IActionResult ResumenDesglosePuntos(int TurnoId, int CajaId, string Fecha)
        {
            var data = _dbContextCA.ResumenDesglosePuntos(Fecha, TurnoId, CajaId);
            return (data!=null) ? Ok(data) : BadRequest(data);
        }

        [HttpGet("[action]")]
        public IActionResult ResumenDesgloseDE(int TurnoId, string Fecha)
        {
            var dataSobFalt = _dbContextCA.ResumenDesgloseDolar(Fecha, TurnoId);
            return (dataSobFalt != null) ? Ok(dataSobFalt) : BadRequest(dataSobFalt);
        }
    }
}
