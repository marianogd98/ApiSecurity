using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPosRio.Models.DB;
using WebApiPosRio.Models.Response;
using WebApiPosRio.Models.ViewModels;

namespace WebApiPosRio.Models.Repository
{
    public class PromocionesRepository : RIOPOSContext , IPromocionesRepository
    {
        
        public PromocionesRepository()
        {
            
        }

        public bool ActualizarPromo(PromoSeptiembre promocion)
        {
            PromocionSeptiembre.Update(promocion);
            return SaveChangesContext();
        }

        public PromoSeptiembre GetPromoById(int id)
        {
            return PromocionSeptiembre.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<PromoSeptiembre> GetPromoSeptiembre()
        {
            try
            {
                var promocion = PromocionSeptiembre.ToList();

                return promocion;
            }
            catch
            {
                throw new Exception();
            }
        }

        public IEnumerable<PromoSeptiembre> GetPromoSeptiembreFechas(DateTime fechaI, DateTime fechaF)
        {
            var promocion = PromocionSeptiembre.Where(p=> fechaI <= p.Fecha && p.Fecha <= fechaF).ToList();
            return promocion;
        }

        public bool SaveChangesContext()
        {
            return (this.SaveChanges() >= 0);
        }

    }
}