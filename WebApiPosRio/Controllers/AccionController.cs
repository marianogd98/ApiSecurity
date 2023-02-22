using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    //[Authorize]
    public class AccionController : ControllerBase
    {
        private readonly IAccionRepository _dbContextAccion;

        public AccionController(IAccionRepository contextMod)
        {
            _dbContextAccion = contextMod;
        }

        // GET: api/<RolController>
        [HttpGet]
        public IEnumerable<AccionViewModel> Get()
        {
            return _dbContextAccion.GetAccions();
        }

        // GET: api/<RolController>
        [HttpGet("/page/{page}/perPage/{perPage}")]
        public IEnumerable<AccionViewModel> Get(int page = 1, int perPage = 10)
        {
            return _dbContextAccion.GetAccions(page, perPage);
        }

        // GET api/<RolController>/5
        [HttpGet("modulo/{ModuloId}")]
        public IActionResult GetAccionesByModuloId(int ModuloId)
        {
            var rolData = _dbContextAccion.GetModuloIdAccionAsync(ModuloId);

            return (rolData != null) ? Ok(rolData) : NotFound();
        }

        // GET api/<RolController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccionViewModel>> Get(int id)
        {
            AccionViewModel rolData = await _dbContextAccion.GetIdAccionAsync(id);

            return (rolData != null) ? rolData : NotFound();
        }

        // POST api/<RolController>
        [HttpPost]
        public ResponseData Post([FromBody] AccionViewModel accionViewModel)
        {
            return _dbContextAccion.AddAccion(accionViewModel);
        }

        // PUT api/<ModuloController>/5
        [HttpPut("{id}")]
        public ResponseData Put(int id, [FromBody] AccionViewModel accionViewModel)
        {
            return _dbContextAccion.UpdateAccion(accionViewModel, id);
        }

        // DELETE api/<ModuloController>/5
        [HttpDelete("{id}")]
        public ResponseData Delete(int id)
        {
            return _dbContextAccion.DeleteAccion(id);
        }
    }
}
