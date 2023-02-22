using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiPosRio.Models.Response;
using WebApiPosRio.Models.ViewModels;

namespace WebApiPosRio.Models.Repository
{
    public interface ICajaRepository
    {
        ResponseData AddCaja(CajaViewModel accionViewModel);
        ResponseData DeleteCaja(int Id);
        object GetAccionsProfile();
        object GetCajaProfile(int idUser, string Item);
        public List<CajaViewModel> GetCajas(Cajafiltro filtro);
        CajaViewModel GetIdCajaAsync(int id, bool esTurno = false);
        ResponseData UpdateCaja(CajaViewModel dataCaja, int id);
        int GetIdCajaByCodCaja(string codCaja);
        List<CajaViewModel> GetCajas(int TiendaId);
    }
}