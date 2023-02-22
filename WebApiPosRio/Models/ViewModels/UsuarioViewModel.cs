using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPosRio.Models.ViewModels
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Password { get; set; }
        public int Estatus { get; set; }
        public int TiendaId { get; set; }
        public int Tipo { get; set; }
        public int RolId { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
    }

    public class UsuarioConfViewModel
    {
        public int Id { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Nick { get; set; }
        public string Password { get; set; }
        public int Estatus { get; set; }
        public int Sucursal { get; set; }
        public int Tipo { get; set; }
        public int RolId { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public List<AccionConfViewModel> Acciones { get; set; } 
    }

    public class LoginSecurityViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string IdCaja { get; set; }
    }

    public class AuthRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class UserPermissionViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Accion { get; set; }
    }

    public class UserData
    {
        public int Id { get; set; }
        public string Nombre {get; set;}
        public string Cedula { get; set; }
        public int Estatus { get; set; }
        public int IdTurno { get; set; }
        public string Token { get; set; }
    }

    public class UserDataAdm
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Estatus { get; set; }
        public string Nick { get; set; }
        public int Sucursal { get; set; }
        public int Lxvl {get; set;}
        public string Token { get; set; }
    }

    public class UserPerfil
    {
        public string Nombre { get; set; }
        public object Acciones { get; set; }
        public int RolId { get; set; }
        public string RolUser { get; set; }
        public int IdTienda { get; set; }
        public bool Estatus { get; set; }
    }

    public class Userfiltro
    {
        public string Cedula  { get; set; }
        public string NombreUser { get; set; }
        public string Nick { get; set; }
        public int RolId { get; set; }
        public string FechaF { get; set; }
        public string FechaI { get; set; }
        public int Estatus { get; set; }
    }

    public class DataExcelUser
    {
        public string Cedula { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Usuario { get; set; }
        public string Clave { get; set; }
        public string Cargo { get; set; }
        public string Sucursal { get; set; }
    }

    public class DataMasivaUser
    {
        public List<DataExcelUser> data { get; set; }
        public int IdUser { get; set; }
    }
}
