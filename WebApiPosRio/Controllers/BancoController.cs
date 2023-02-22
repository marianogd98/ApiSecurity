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
    public class BancoController : ControllerBase
    {
        private IBancoRepository _bancoRepository;

        public BancoController(IBancoRepository bancoRepository)
        {
            _bancoRepository = bancoRepository;
        }
        // GET: api/<BancoController>
        [HttpGet]
        public IEnumerable<BancoViewModel> Get()
        {
            return _bancoRepository.GetBancos();
        }

        // GET api/<BancoController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BancoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<BancoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BancoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
