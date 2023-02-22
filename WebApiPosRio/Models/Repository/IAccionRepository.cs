using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiPosRio.Models.Response;
using WebApiPosRio.Models.ViewModels;

namespace WebApiPosRio.Models.Repository
{
    public interface IAccionRepository
    {
        ResponseData AddAccion(AccionViewModel AccionViewModel);
        ResponseData DeleteAccion(int Id);
        //List<AccionViewModel> GetAccions();
        IEnumerable<AccionViewModel> GetModuloIdAccionAsync(int ModuloId);
        IEnumerable<AccionViewModel> GetAccions(int page, int perPage);
        IEnumerable<AccionViewModel> GetAccions();
        public object GetAccionsProfile();
        public object GetAccionsProfile(int idUser, string Item);
        Task<AccionViewModel> GetIdAccionAsync(int id);
        ResponseData UpdateAccion(AccionViewModel accionViewModel, int Id);
    }
}