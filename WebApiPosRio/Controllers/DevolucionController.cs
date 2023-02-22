using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
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
    public class DevolucionController : ControllerBase
    {
        private readonly IDevolucionRepository _devolRep;
        public DevolucionController(IDevolucionRepository devolucionRepository)
        {
            _devolRep = devolucionRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetDevolucion([FromQuery] DevolucionViewModel devolucionViewModel)
        {
           ResponseData value = _devolRep.GetDevoluciones(devolucionViewModel);
           return (value.Success == 1) ? Ok(value) : BadRequest(value);
        }

        // GET api/<DevolucionController>/5
        [HttpGet("{id}/caja/{codCaja}/turno/{IdTurno}")]
        [AllowAnonymous]
        public IActionResult GetDevolucionTurnoCajaId(string id, string codCaja, int IdTurno)
        {
            ResponseData value = _devolRep.GetIdDevolucionAsync(id, codCaja, IdTurno);
            return (value.Success == 1) ? Ok(value) : BadRequest(value);
        }

        [HttpGet("{IdTurno}")]
        [AllowAnonymous]
        public IActionResult GetDevoluciones(int IdTurno)
        {
            ResponseData value = _devolRep.BuscarDevoluciones(IdTurno);
            return (value.Success == 1) ? Ok(value) : BadRequest(value);
        }

        [HttpGet("[action]")]
        [AllowAnonymous]
        public IActionResult ListadoDevoluciones([FromQuery] DevolucionViewModel devolucionViewModel, int page = 1, int perPage = 10)
        {
            var value = _devolRep.ListadoDevoluciones(devolucionViewModel);


            if (devolucionViewModel.Monto>0)
            {
                value = value.Where(d => d.MontoDevolucion == devolucionViewModel.Monto).ToList();
            }

            try
            {
                var paginacion = value.ToPagedList(page, perPage);

                var paginateData = new
                {
                    Devoluciones = paginacion,
                    totalrow = paginacion.TotalItemCount
                };
                return (paginateData != null) ? Ok(paginateData) : BadRequest(paginateData);
            }
            catch
            {
                return  BadRequest(new { Devoluciones="", totalrow=0 });
            }
            
        }
    }
}
