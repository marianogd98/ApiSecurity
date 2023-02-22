using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPosRio.Models.DB;
using WebApiPosRio.Models.Repository;
using WebApiPosRio.Models.ViewModels;
using X.PagedList;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiPosRio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonacionesController : ControllerBase
    {
        private IDonacionRepository _donacionRepository;

        public DonacionesController(IDonacionRepository donacionRepository)
        {
            _donacionRepository = donacionRepository;
        }

        // GET: api/<DonacionesController>
        [HttpGet]
        public IActionResult Get([FromQuery] ParamsLfViewModel paramsLf)
        {
            List<SpListadoDonaciones> data = _donacionRepository.ListadoDonaciones(paramsLf);
            var listdonaciones = data.ToPagedList(paramsLf.Page, paramsLf.PerPage);
            var paginateData = new
            {
                Donaciones = listdonaciones,
                totalrow = listdonaciones.TotalItemCount
            };
            return (data != null) ? Ok(paginateData) : BadRequest("No hay data");
        }

        // GET: api/<DonacionesController>
        [HttpGet("[action]/Fecha/{Fecha}/Cliente/{ClienteId?}/Organizacion/{OrganizacionId?}/Caja/{CajaId?}/Turno/{TurnoId?}")]
        public IActionResult GetFilter(string Fecha, int ClienteId = 0, int OrganizacionId = 0, int CajaId = 0, int TurnoId = 0)
        {
            var data = _donacionRepository.GetDonaciones(Fecha, ClienteId,OrganizacionId,CajaId,TurnoId);
            return (data != null) ? Ok(data) : BadRequest("No hay data");
        }

        // GET: api/<DonacionesController>
        [HttpGet("[action]/Fecha/{Fecha}/Cliente/{ClienteId?}/Organizacion/{OrganizacionId?}/Caja/{CajaId?}/Turno/{TurnoId?}")]
        public IActionResult GetResumen(string Fecha, int ClienteId = 0, int OrganizacionId = 0, int CajaId = 0, int TurnoId = 0)
        {
            var data = _donacionRepository.GetResumenDonaciones(Fecha, ClienteId, OrganizacionId, CajaId, TurnoId);
            return (data != null) ? Ok(data) : BadRequest("No hay data");
        }

        // GET api/<DonacionesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<DonacionesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DonacionesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DonacionesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
