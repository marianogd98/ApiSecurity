using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebApiPosRio.Models.Repository;
using WebApiPosRio.Models.Response;
using WebApiPosRio.Models.ViewModels;

namespace WebApiPosRio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetaController : ControllerBase
    {
        private readonly IMetaRepository _metaDashboard;

        public MetaController(IMetaRepository metaRepository)
        {
            _metaDashboard = metaRepository;
        }

        // GET: api/<MetaController>
        [HttpGet]
        public IActionResult Get([FromQuery]InsideVentaViewModel insideVentaViewModel)
        {
            var data = _metaDashboard.GetListMeta(insideVentaViewModel);
            return data!=null? Ok(data):BadRequest(data);
        }

        // GET: api/<MetaController>
        [HttpGet("Dia")]
        public IActionResult GetDia([FromQuery] InsideVentaViewModel insideVentaViewModel)
        {
            var data = _metaDashboard.GetDataMeta(insideVentaViewModel);
            return data != null ? Ok(data) : BadRequest(data);
        }

        // GET api/<MetaController>/5
        [HttpGet("Mes")]
        public IActionResult GetMes([FromQuery] InsideVentaViewModel insideVentaViewModel)
        {
            var data = _metaDashboard.GetDataMetaAcum(insideVentaViewModel);
            return data != null ? Ok(data) : BadRequest(data);
        }

        // GET api/<MetaController>/5
        [HttpGet("Grafica")]
        public IActionResult GetGrafica([FromQuery] InsideVentaViewModel insideVentaViewModel)
        {
            var data = _metaDashboard.GetGrafica(insideVentaViewModel);
            return data != null ? Ok(data) : BadRequest(data);
        }

        // POST api/<MetaController>
        [HttpPost]
        public IActionResult Post([FromBody] DataMeta dataMeta)
        {
            bool result = _metaDashboard.CargaDataExcel(dataMeta);
            ResponseData response = new ResponseData() { Data = null, Message = (result ? "Data Cargada" : "Data no cargada"), Success = result?1:0 };
            return result?Ok(response):BadRequest(response);
        }

        // PUT api/<MetaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MetaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
