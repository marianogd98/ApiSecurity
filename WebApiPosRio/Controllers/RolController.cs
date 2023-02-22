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
    [Authorize]
    public class RolController : ControllerBase
    {
        private readonly IRolRepository _dbContextRol;

        public RolController(IRolRepository contextRol)
        {           
            _dbContextRol = contextRol;
        }

        // GET: api/<RolController>
        [HttpGet]
        public IEnumerable<RolViewModel> Get(int page = 1, int perPage = 10)
        {
            return _dbContextRol.GetRols(page, perPage);
        }

        // GET api/<RolController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RolViewModel>> Get(int id)
        {
            RolViewModel rolData = await _dbContextRol.GetIdRolAsync(id);

            return (rolData != null)?rolData: NotFound();
        }

        // POST api/<RolController>
        [HttpPost]
        public ResponseData Post([FromBody] RolViewModel rolViewModel)
        {
            return _dbContextRol.AddRol(rolViewModel);
        }

        // PUT api/<RolController>/5
        [HttpPut("{id}")]
        public ResponseData Put(int id, [FromBody] RolViewModel rolViewModel)
        {
            return _dbContextRol.UpdateRol(rolViewModel, id);
        }

        // DELETE api/<RolController>/5
        [HttpDelete("{id}")]
        public ResponseData Delete(int id)
        {
            return _dbContextRol.DeleteRol(id);
        }
    }
}
