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
    public class AreaController : ControllerBase
    {
        private readonly IAreaRepository _contextArea;

        public AreaController(IAreaRepository contextArea)
        {
            _contextArea = contextArea;
        }
        // GET: api/<AreaController>
        [HttpGet]
        public IEnumerable<AreaViewModel> Get()
        {
            return _contextArea.GetArea();
        }

        [HttpGet("[action]/{tiendaId}")]
        public IEnumerable<AreaSelectViewModel> GetSelectArea(int tiendaId)
        {
            return _contextArea.GetSelectArea(tiendaId);
        }

        // GET api/<AreaController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AreaController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AreaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AreaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
