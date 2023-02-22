using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WebApiPosRio.Models.DB;
using WebApiPosRio.Models.Helper;
using WebApiPosRio.Models.Response;
using WebApiPosRio.Models.ViewModels;

namespace WebApiPosRio.Models.Repository
{
    public class BancoRepository : RIOPOSContext, IBancoRepository
    {
        public List<BancoViewModel> GetBancos()
        {
            List<BancoViewModel> lstBanco = (from d in Bancos
                                             select new BancoViewModel
                                             {
                                                 Id = d.Id,
                                                 Nombre = d.Nombre,
                                                 Estatus = d.Estatus,
                                                 /*CreatedAt = d.CreatedAt.ToString(),
                                                 UpdatedAt = d.UpdatedAt.ToString()*/
                                             }).ToList();

            return lstBanco.OrderByDescending(lm => lm.Id).ToList();
        }

        public async Task<BancoViewModel> GetIdRolAsync(int id)
        {
            var bancoItem = await Bancos.FindAsync(id);

            if (bancoItem == null)
            {
                return null;
            }
            return new BancoViewModel
            {
                Id = bancoItem.Id,
                Nombre = bancoItem.Nombre
            };
        }

        public ResponseData AddBanco(BancoViewModel bancoViewModel)
        {
            ResponseData oR = new ResponseData();
            try
            {
                var bancoData = new Banco
                {
                    Nombre = Util.UFirst(bancoViewModel.Nombre)
                };

                Bancos.Add(bancoData);
                this.SaveChanges();
                oR.Success = 1;
                oR.Data = GetBancos();
                oR.Message = "Datos Almacenados Satisfactoriamente";
            }
            catch (Exception ex)
            {
                oR.Success = 0;
                oR.Message = ex.Message;
            }
            return oR;
        }

        public ResponseData UpdateBanco(BancoViewModel bancoViewModel, int Id)
        {
            ResponseData oR = new ResponseData();
            try
            {
                var bancoData = new Banco
                {
                    Id = Id,
                    Estatus = bancoViewModel.Estatus,
                    Nombre = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(bancoViewModel.Nombre)
                };

                Bancos.Update(bancoData);
                this.SaveChanges();
                oR.Success = 1;
                oR.Message = "Datos Actualizados Satisfactoriamente";
            }
            catch (Exception ex)
            {
                oR.Success = 0;
                oR.Message = ex.Message;
            }
            return oR;
        }

        public ResponseData DeleteBanco(int Id)
        {
            ResponseData oR = new ResponseData();
            try
            {
                var bancoData = new Banco
                {
                    Id = Id
                };
                this.Remove(bancoData);
                this.SaveChanges();
                oR.Success = 1;
                oR.Message = "Registro eliminado Satisfactoriamente";
            }
            catch (Exception ex)
            {
                oR.Success = 0;
                oR.Message = ex.Message;
            }
            return oR;
        }
    }
}
