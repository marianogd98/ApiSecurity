using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiPosRio.Models.DB;
using WebApiPosRio.Models.Response;
using WebApiPosRio.Models.ViewModels;

namespace WebApiPosRio.Models.Repository
{
    public interface IPromocionesRepository
    {
        IEnumerable<PromoSeptiembre> GetPromoSeptiembre();
        IEnumerable<PromoSeptiembre> GetPromoSeptiembreFechas(DateTime fechaI , DateTime fechaF);
        bool ActualizarPromo (PromoSeptiembre promocion);
        bool SaveChangesContext();
        PromoSeptiembre GetPromoById(int id);
    }
}