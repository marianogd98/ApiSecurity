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
    public class FormaPagoController : ControllerBase
    {
        private readonly IFormaPagoRepository _dbContextFormaPago;
        public FormaPagoController(IFormaPagoRepository dbContextFormaPago)
        {
            _dbContextFormaPago = dbContextFormaPago;
        }
        // GET: api/<FormaPagoController>
        [HttpGet]
        public IEnumerable<FormaPagoViewModel> Get()
        {
            return _dbContextFormaPago.GetFormaPagos();
        }

        // GET api/<FormaPagoController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<FormaPagoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<FormaPagoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FormaPagoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
