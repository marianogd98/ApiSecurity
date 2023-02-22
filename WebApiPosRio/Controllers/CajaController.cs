using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPosRio.Models.Repository;
using WebApiPosRio.Models.Response;
using WebApiPosRio.Models.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiPosRio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CajaController : ControllerBase
    {
        private readonly ICajaRepository _contextCaja;

        public CajaController(ICajaRepository contextCaja)
        {
            _contextCaja = contextCaja;
        }

        // GET: api/<CajaController>
        [HttpGet]
        public IActionResult Get(int page, int perPage, [FromQuery] Cajafiltro filtro)
        {
            var lstCajas = _contextCaja.GetCajas(filtro);


            var listaCajas = lstCajas.ToPagedList(page, perPage);
            var paginate = new
            {
                Cajas = listaCajas,
                Totalrow = listaCajas.TotalItemCount
            };

            return (lstCajas != null) ? Ok(paginate) : BadRequest(new { Success = 0, Message = "No hay informacion Disponible" });
        }

        // GET api/<CajaController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // GET api/<CajaController>/5
        [HttpGet("tienda/{TiendaId}")]
        [AllowAnonymous]
        public IActionResult GetCajaTienda(int TiendaId)
        {
            var dataCajas = _contextCaja.GetCajas(TiendaId);
            return (dataCajas != null) ? Ok(dataCajas) : BadRequest(dataCajas);
        }

        // POST api/<CajaController>
        [HttpPost]
        public IActionResult Post([FromBody] CajaViewModel cajaViewModel)
        {
            var dataCajas = _contextCaja.AddCaja(cajaViewModel);

            return (dataCajas.Success==1) ? Ok(dataCajas) : BadRequest(dataCajas);
        }

        // PUT api/<CajaController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CajaViewModel dataCaja)
        {
            ResponseData value = _contextCaja.UpdateCaja(dataCaja, id);
            return (value.Success == 1) ? Ok(value) : BadRequest(value);
        }

        // DELETE api/<CajaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
