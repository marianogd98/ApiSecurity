using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiPosRio.Models.DB;
using WebApiPosRio.Models.Response;
using WebApiPosRio.Models.ViewModels;

namespace WebApiPosRio.Models.Repository
{
    public interface ITiendaRepository
    {
        ResponseData AddTienda(TiendaViewModel accionViewModel);
        ResponseData DeleteTienda(int Id);
        Task<TiendaViewModel> GetIdTiendaAsync(int id);
        List<TiendaViewModel> GetTiendas();
        List<TiendaSelectViewModel>  GetTiendasSelect();
        ResponseData UpdateTienda(TiendaViewModel accionViewModel, int Id);
        Tiendum GetTiendaByAcronimo(string Acronimo);
        string GetUrlTienda(int tiendaId);
    }
}