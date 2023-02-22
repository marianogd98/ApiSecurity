using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPosRio.Models.Repository;
using WebApiPosRio.Models.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiPosRio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TiendaController : ControllerBase
    {
        private readonly ITiendaRepository _dbContextTienda;

        public TiendaController(ITiendaRepository dbContextTienda)
        {
            _dbContextTienda = dbContextTienda;
        }
        // GET: api/<TiendaController>
        [HttpGet]
        public IEnumerable<TiendaViewModel> Get()
        {
            return _dbContextTienda.GetTiendas();
        }

        [HttpGet("[action]")]
        public IEnumerable<TiendaSelectViewModel> SelectTiendas()
        {
            return _dbContextTienda.GetTiendasSelect();
        }

        // GET api/<TiendaController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TiendaController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TiendaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TiendaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
