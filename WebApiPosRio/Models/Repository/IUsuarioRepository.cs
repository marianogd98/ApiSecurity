using System.Collections.Generic;
using WebApiPosRio.Models.Response;
using WebApiPosRio.Models.ViewModels;

namespace WebApiPosRio.Models.Repository
{
    public interface IUsuarioRepository
    {
        public List<UsuarioConfViewModel> GetUsuarios();
        IEnumerable<UsuarioConfViewModel> GetUsuarios(int page, int perPage, Userfiltro filtro, int IdUser = 0);
        List<RolConfViewModel> GetRols();
        List<ModuloConfViewModel> GetModulos();
        public List<ModuloConfViewModel> GetModulosAccionByRol(int rolId = 2);
        public ResponseData AddUsuario(UsuarioConfViewModel usuarioViewModel);
        public ResponseData UpdateUser(UsuarioConfViewModel usuarioViewModel, int Id);
        public ResponseData DeleteUser(int Id);
        public ResponseData Login(LoginSecurityViewModel user);
        public ResponseData Login(AuthRequest user);
        public ResponseData GetProfileUser(int idUser);
        public ResponseData GetPermisionUser(int idUser, string item);
        public ResponseData GetPermissionLogin(UserPermissionViewModel user);
        public ResponseData GetUserTurno(int turnoId);
        int getIdUserbyName(string nombreCajera);
        bool CargaDataExcelUser(DataMasivaUser dataMasivaUser);
    }
}