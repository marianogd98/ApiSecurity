using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiPosRio.Models.Response;
using WebApiPosRio.Models.ViewModels;

namespace WebApiPosRio.Models.Repository
{
    public interface IModuloRepository
    {
        ResponseData AddModulo(ModuloViewModel ModuloViewModel);
        ResponseData DeleteModulo(int Id);
        Task<ModuloViewModel> GetIdModuloAsync(int id);
        List<ModuloViewModel> GetModulos();
        IEnumerable<ModuloViewModel> GetModulos(int page, int perPage);
        ResponseData UpdateModulo(ModuloViewModel ModuloViewModel, int Id);
    }
}