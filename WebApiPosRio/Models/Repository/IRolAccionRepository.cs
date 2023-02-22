using WebApiPosRio.Models.ViewModels;

namespace WebApiPosRio.Models.Repository
{
    public interface IRolAccionRepository
    {
        bool UpdateRolAccion(int accionId = 0, AccionViewModel accionViewModel = null);
    }
}