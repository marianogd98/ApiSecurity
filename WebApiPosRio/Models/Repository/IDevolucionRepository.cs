using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiPosRio.Models.DB;
using WebApiPosRio.Models.Response;
using WebApiPosRio.Models.ViewModels;

namespace WebApiPosRio.Models.Repository
{
    public interface IDevolucionRepository
    {
        ResponseData GetIdDevolucionAsync(string id, string codCaja, int IdTurno);
        ResponseData BuscarDevoluciones(int TurnoId);
        ResponseData GetDevoluciones(DevolucionViewModel devolucionViewModel);
        List<SpListadoDevoluciones> ListadoDevoluciones(DevolucionViewModel devolucionViewModel);
    }
}