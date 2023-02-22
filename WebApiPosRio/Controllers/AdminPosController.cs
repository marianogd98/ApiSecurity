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
    public class AdminPosController : ControllerBase
    {
        private readonly ISpRepository _storeProcedures;

        public AdminPosController(ISpRepository storeProcedures)
        {
            _storeProcedures = storeProcedures;
        }


        // GET: api/<AdminPosController>
        [HttpGet("tasas")]
        public IActionResult GetTasas([FromQuery] string fecha)
        {
            try
            {
                var data = _storeProcedures.BuscarTasas(fecha);
                if (data != null)
                    return Ok(data);
                else
                    return BadRequest();

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // GET api/<AdminPosController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<AdminPosController>
        [HttpPost]
        public IActionResult Post([FromBody] BodyGuardarTasa data)
        {

            try
            {
                var guardar = _storeProcedures.GuardarTasa(data.Fecha, data.TasaUsd, data.TasaEur, data.UsuarioId);

                if (guardar == 1)
                    return Ok(new {data = guardar , message = "Tasa Guardada con exito" });
                else
                    return BadRequest(new { data = guardar, message = "Tasa Guardada con exito" });

            }
            catch(Exception ex)
            {
                return BadRequest();
            }

        }
    }
}
