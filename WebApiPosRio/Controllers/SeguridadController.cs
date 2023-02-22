using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class SeguridadController : ControllerBase
    {
        private readonly IUsuarioRepository _dbContextUser;
        private readonly IAccionRepository _dbContextAccion;
        private readonly ISpRepository _SpRepo;
        public SeguridadController(ISpRepository spRepo, IUsuarioRepository contextUser, IAccionRepository accionRepository)
        {
            _dbContextUser = contextUser;
            _dbContextAccion = accionRepository;
            _SpRepo = spRepo;
        }

        [HttpGet("[action]/{idUser}")]
        public ResponseData Perfil(int idUser)
        {
            return _dbContextUser.GetProfileUser(idUser);
        }

        [HttpGet("[action]")]
        public ResponseData Acciones()
        {
            var lAcciones = _dbContextAccion.GetAccionsProfile();
            return new ResponseData { Success= (lAcciones != null)?1:0, Data=lAcciones, Message=(lAcciones!=null)?"Datos encontrados":"No hay acciones disponibles" };
        }

        [HttpGet("[action]/{idUser}/{Item}")]
        public ResponseData PermisosAccion(int idUser, string Item)//aqui item 16
        {
            var lAcciones = _dbContextAccion.GetAccionsProfile(idUser, Item);            
            return new ResponseData { Success = (lAcciones!=null) ?1:0, Data = lAcciones, Message = (lAcciones!=null) ? "Datos encontrados" : "No hay acciones disponibles" };
        }

        [HttpGet("[action]/{idUser}/{item}")]
        public ResponseData PermisoUser(int idUser, string item)
        {
            var pUser = _dbContextUser.GetPermisionUser(idUser, item);
            return pUser; // new ResponseData { Success = 1, Data = pUser, Message = (pUser != null) ? "Datos encontrados" : "No hay accones disponibles" };
        }

        [HttpPost("[action]")]
        public ResponseData PermisoUser([FromBody] UserPermissionViewModel userData)
        {
            return _dbContextUser.GetPermissionLogin(userData);
        }

        [HttpGet("[action]/fecha/{Fecha}/caja/{IdCaja}/turno/{IdTurno}")]
        public IActionResult ReporteCierreTurno(string Fecha, int IdCaja, int IdTurno)
        {
            //DetalleFormaPagoCaja
            var data = _SpRepo.ReporteCierreCaja(IdCaja, Fecha, IdTurno);
            ResponseData response = new ResponseData { Success = 0, Data = data, Message = "No hay data para mostrar" };
            if (data != null)
            {
                response.Success = 1;
                response.Message = "Data Encontrada";
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpGet("[action]/fecha/{Fecha}/caja/{IdCaja}")]
        public IActionResult ReporteCierreCaja(string Fecha, int IdCaja )
        {
            var data = _SpRepo.ReporteCierreCaja(IdCaja, Fecha);
            ResponseData response = new ResponseData { Success = 0, Data = data, Message = "No hay data para mostrar" };
            if (data != null)
            {
                response.Success = 1;
                response.Message = "Data Encontrada";
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }            
        }


    }
}
