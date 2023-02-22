using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPosRio.Models.Repository;
using WebApiPosRio.Models.Response;
using WebApiPosRio.Models.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiPosRio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _dbContextUser;
        public UsuarioController(IUsuarioRepository contextUser)
        {
            _dbContextUser = contextUser;
        }

        // GET: api/<UsuarioController>
        [HttpGet]
        public IActionResult/*IEnumerable<UsuarioConfViewModel>*/ Get(string cedula, string nombreUser, int estatus, string fechaI, string fechaF,int rolId ,int page = 1, int perPage = 10 )
        {
            var dataUsers = _dbContextUser.GetUsuarios(page, perPage, new Userfiltro { Cedula =cedula, NombreUser = nombreUser, Estatus=estatus, FechaF=fechaF, FechaI=fechaI, RolId=rolId },3);
            var listaUsuarios = dataUsers.ToPagedList(page, perPage);
            var paginate = new
            {
                usuarios = listaUsuarios,
                totalrow = listaUsuarios.TotalItemCount
            };

            return (dataUsers != null) ? Ok(paginate) : BadRequest(new { Success = 0, Message = "No hay informacion Disponible" });
        }

        [HttpGet("Admin")]
        public IActionResult/*IEnumerable<UsuarioConfViewModel>*/ UsersAdmins(string cedula, string nombreUser, string Nick ,int estatus, string fechaI, string fechaF, int rolId, int Tipo=3, int page = 1, int perPage = 10)
        {
            var dataUsers = _dbContextUser.GetUsuarios(page, perPage, new Userfiltro { Cedula = cedula, NombreUser = nombreUser, Nick=Nick,Estatus = estatus, FechaF = fechaF, FechaI = fechaI, RolId = rolId },Tipo);
            var listaUsuarios = dataUsers.ToPagedList(page, perPage);
            var paginate = new
            {
                usuarios = listaUsuarios,
                totalrow = listaUsuarios.TotalItemCount
            };

            return (dataUsers != null) ? Ok(paginate) : BadRequest(new { Success = 0, Message = "No hay informacion Disponible" });
        }

        [HttpGet("[action]")]
        public IEnumerable<RolConfViewModel> RolConfig()
        {
            return _dbContextUser.GetRols();
        }

        [HttpGet("[action]")]
        public IEnumerable<ModuloConfViewModel> ModuloConfig()
        {
            return _dbContextUser.GetModulos();
        }

        [HttpGet("[action]/{IdRol}")]
        public IEnumerable<ModuloConfViewModel> ModuloAccionRolConfig(int IdRol)
        {
            return _dbContextUser.GetModulosAccionByRol(IdRol);
        }

        // GET api/<UsuarioController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet("[action]/{idUser}")]
        public IActionResult PerfilUsuario(int idUser)
        {
            var profileUser = _dbContextUser.GetProfileUser(idUser);
            return (profileUser.Success == 1) ? Ok (profileUser) :BadRequest(profileUser);
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public IActionResult Post([FromBody] UsuarioConfViewModel usuarioConfViewModel)
        {
            var responseAddUser = _dbContextUser.AddUsuario(usuarioConfViewModel);
            return (responseAddUser.Success == 1) ? Ok(responseAddUser) : BadRequest(responseAddUser);
            //return Ok("ok");
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginSecurityViewModel userData)
        {
            ResponseData value = _dbContextUser.Login(userData);
            return (value.Success == 1) ? Ok(value) : BadRequest(value);
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public IActionResult LoginAdmin([FromBody] AuthRequest userData)
        {
            ResponseData value = _dbContextUser.Login(userData);
            return (value.Success==1)?Ok(value):BadRequest(value);
        }

        // PUT api/<UsuarioController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UsuarioConfViewModel usuarioConfViewModel)
        {
            var response = _dbContextUser.UpdateUser(usuarioConfViewModel, id);

            return (response.Success == 1) ? Ok(response) :  BadRequest(response);
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public ResponseData Delete(int id)
        {
            return _dbContextUser.DeleteUser(id);
        }

        // POST api/<UsuarioController>
        [HttpPost("carga")]
        public IActionResult Post([FromBody] DataMasivaUser dataUser)
        {
            bool result = _dbContextUser.CargaDataExcelUser(dataUser);
            ResponseData response = new ResponseData() { Data = null, Message = (result ? "Data Cargada" : "Data no cargada"), Success = result ? 1 : 0 };
            return result ? Ok(response) : BadRequest(response);
        }
    }
}
