using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiPosRio.Models.Repository;
using WebApiPosRio.Models.ViewModels;
namespace WebApiPosRio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandadoController : ControllerBase
    {
        private readonly ICandadoRepository _candadoRepository;

        public CandadoController (ICandadoRepository candadoRepository)
        {
            _candadoRepository = candadoRepository;
        }

        [HttpPost("crear/horario")]
        public IActionResult Post([FromBody] BodyCrearHorario data)
        {
            try
            {
                var guardar = _candadoRepository.CrearHorario(data.Descripcion, data.HoraInicio, data.HoraFin, data.Ciclo, data.Activo);

                if (guardar > 1)
                    return Ok(new { data = guardar, message = "Horario Creado con exito" });
                else
                    return BadRequest(new { data = guardar, message = "Problemas para crear horario" });

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost("asignar/dias")]
        public IActionResult PostDiasxTurnos([FromBody] BodyAsignarDias data)
        {
            try
            {
                var guardar = _candadoRepository.AsignarDias(data.IdTurno, data.Descripcion);

                if (guardar == 1)
                    return Ok(new { data = guardar, message = "Dias Asignados con exito" });
                else
                    return BadRequest(new { data = guardar, message = "Problemas para asignar dias" });

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost("asignar/horario")]
        public IActionResult PostHorarioxUsuario([FromBody] BodyAsignarHorario data)
        {
            try
            {
                var guardar = _candadoRepository.AsignarHorario(data.Cedula, data.Turno, data.FechaInicio, data.FechaFin, data.Activo);

                if (guardar == 1)
                    return Ok(new { data = guardar, message = "Horario Asignado con exito" });
                else
                    return BadRequest(new { data = guardar, message = "Problemas para asignar horario" });

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("turnos")]
        public IActionResult GetTurnos([FromQuery] int IdTurno, string Descripcion, string Fecha_Creacion, string Fecha_Modificacion, int Activo)
        {
            try
            {
                var data = _candadoRepository.BuscarTurnos(IdTurno, Descripcion, Fecha_Creacion, Fecha_Modificacion, Activo);
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

        [HttpGet("empleados")]
        public IActionResult GetEmpleados([FromQuery] int Cedula, string Nombre, string Cargo, int Activo)
        {
            try
            {
                var data = _candadoRepository.BuscarEmpleados(Cedula, Nombre, Cargo, Activo);
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
    }
}
