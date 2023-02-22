using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPosRio.Models.DB;
using WebApiPosRio.Models.Repository;
using WebApiPosRio.Models.Response;
using WebApiPosRio.Models.ViewModels;

namespace WebApiPosRio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QqmController : ControllerBase
    {
        private readonly IQqmRepository _qqmRepository;

        public QqmController(IQqmRepository qqmRepository)
        {
            _qqmRepository = qqmRepository;
        }

        [HttpGet("[action]")]
        public IActionResult GetConfig()
        {
            var data = _qqmRepository.GetConfig();

            if (data != null)
            {
                return Ok(data);
            }
            else
            {
                return BadRequest("No hay datos");
            }
        }

        [HttpPost("[action]")]
        public IActionResult SaveConfig([FromBody] List<SpListadoConstante> config)
        {
            int resp = _qqmRepository.SaveConfig(config);
            ResponseData rd = new ResponseData() { Data = null, Message = "", Success = 0 };
            if (resp >= 1)
            {
                rd.Message = "Configuración Almacenada correctamente";
                rd.Success = 1;
                return Ok(rd);
            }
            else
            {
                rd.Message = "Ha ocurrido un error: Configuración del Juego";
                return BadRequest(rd);
            }
        }

        [HttpGet("[action]")]
        public IActionResult SendQuestions()
        {
            //LoadData()
            var data = _qqmRepository.LoadData();

            if (data != null)
            {
                return Ok(data);
            }
            else
            {
                return BadRequest("No hay datos");
            }
        }

        [HttpGet("[action]")]
        public IActionResult GetQuestions()
        {
            //LoadData()
            var data = _qqmRepository.ProcesarPreguntas();

            if (data != null)
            {
                return Ok(data);
            }
            else
            {
                return BadRequest("No hay datos");
            }
        }

        [HttpPost("[action]")]
        public IActionResult SendGame([FromBody] Game game)
        {
            int resp = _qqmRepository.SaveGame(game);
            ResponseData rd = new ResponseData() { Data = null, Message = "", Success=0};
            if (resp >= 1)
            {
                rd.Message = "Juego Almacenado correctamente";
                rd.Success = 1;
                return Ok(rd);
            }
            else
            {
                rd.Message = "Ha ocurrido un error: Guardar Juego";
                return BadRequest(rd);
            }
        }


        [HttpPut("[action]/{id}")]
        public IActionResult SaveQuestions(int Id,[FromBody] QQmForm qQmForm)
        {
            int resp = _qqmRepository.SaveQuestions(Id, qQmForm);

            if (resp == 1)
            {
                return Ok("Las Preguntas se guardaron correctamente");
            }
            else
            {
                return BadRequest("Ha ocurrido un error: Guardar Preguntas");
            }
        }

        [HttpPut("[action]/{id}")]
        public IActionResult SaveGifts(int Id, [FromBody] QQmForm qQmForm)
        {
            int resp = _qqmRepository.SaveGifts(Id, qQmForm);

            if (resp == 1)
            {
                return Ok("Los Premios se guardaron correctamente");
            }
            else
            {
                return BadRequest("Ha ocurrido un error: Guardar Preguntas");
            }
        }

        // DELETE api/<QQMController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            int resp = _qqmRepository.SaveQuestions(id, new QQmForm());

            if (resp == 1)
            {
                return Ok("Las Preguntas se guardaron correctamente");
            }
            else
            {
                return BadRequest("Ha ocurrido un error: Guardar Preguntas");
            }
        }

    }
}
