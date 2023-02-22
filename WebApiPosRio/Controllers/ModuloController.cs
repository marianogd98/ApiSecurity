using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPosRio.Models.Repository;
using WebApiPosRio.Models.Response;
using WebApiPosRio.Models.ViewModels;

namespace WebApiPosRio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ModuloController : ControllerBase
    {
        private readonly IModuloRepository _dbContextMod;

        public ModuloController(IModuloRepository contextMod)
        {
            _dbContextMod = contextMod;
        }

        // GET: api/<RolController>
        [HttpGet]
        public IEnumerable<ModuloViewModel> Get()
        {
            return _dbContextMod.GetModulos();
        }

        // GET: api/<RolController>
        [HttpGet("page/{page}/perPage/{perpage}")]
        public IEnumerable<ModuloViewModel> Get(int page = 1, int perPage = 10)
        {
            return _dbContextMod.GetModulos(page, perPage);
        }


        // GET api/<RolController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ModuloViewModel>> Get(int id)
        {
            ModuloViewModel rolData = await _dbContextMod.GetIdModuloAsync(id);

            return (rolData != null)?rolData: NotFound();
        }

        // POST api/<RolController>
        [HttpPost]
        public ResponseData Post([FromBody] ModuloViewModel moduloViewModel)
        {
            return _dbContextMod.AddModulo(moduloViewModel);
        }

        // PUT api/<ModuloController>/5
        [HttpPut("{id}")]
        public ResponseData Put(int id, [FromBody] ModuloViewModel moduloViewModel)
        {
            return _dbContextMod.UpdateModulo(moduloViewModel, id);
        }

        // DELETE api/<ModuloController>/5
        [HttpDelete("{id}")]
        public ResponseData Delete(int id)
        {
            return _dbContextMod.DeleteModulo(id);
        }
    }
}
