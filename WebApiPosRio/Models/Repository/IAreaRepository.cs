using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiPosRio.Models.Response;
using WebApiPosRio.Models.ViewModels;

namespace WebApiPosRio.Models.Repository
{
    public interface IAreaRepository
    {
        ResponseData AddArea(AreaViewModel accionViewModel);
        ResponseData DeleteArea(int Id);
        List<AreaViewModel> GetArea();
        List<AreaSelectViewModel> GetSelectArea(int tiendaId);
        Task<AreaViewModel> GetIdAreaAsync(int id);
        ResponseData UpdateArea(AreaViewModel areaViewModel, int Id);
    }
}