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
    public class FormaPagoRepository : RIOPOSContext, IFormaPagoRepository
    {
        public List<FormaPagoViewModel> GetFormaPagos()
        {
            List<FormaPagoViewModel> lstFormaPago = (from d in FormaPagos                                                     
                                                     select new FormaPagoViewModel
                                                     {
                                                         Id = d.Id,
                                                         Descripcion = d.Descripcion,
                                                         CodigoMoneda = d.CodigoMoneda,
                                                         Orden = d.Orden,
                                                         /*CreatedAt = d.CreatedAt.ToString(),
                                                         UpdatedAt = d.UpdatedAt.ToString()*/
                                                     }).ToList();

            return lstFormaPago.OrderBy(lm => lm.Orden).ToList();
        }

        public async Task<FormaPagoViewModel> GetIdRolAsync(int id)
        {
            var formaPagoItem = await FormaPagos.FindAsync(id);

            if (formaPagoItem == null)
            {
                return null;
            }
            return new FormaPagoViewModel
            {
                Id = formaPagoItem.Id,
                Descripcion = formaPagoItem.Descripcion
            };
        }


        public ResponseData AddBanco(FormaPagoViewModel formaPagoViewModel)
        {
            ResponseData oR = new ResponseData();
            try
            {
                var formaPagoData = new FormaPago
                {
                    Descripcion = Util.UFirst(formaPagoViewModel.Descripcion)
                };

                FormaPagos.Add(formaPagoData);
                this.SaveChanges();
                oR.Success = 1;
                oR.Data = GetFormaPagos();
                oR.Message = "Datos Almacenados Satisfactoriamente";
            }
            catch (Exception ex)
            {
                oR.Success = 0;
                oR.Message = ex.Message;
            }
            return oR;
        }

        public ResponseData UpdateBanco(FormaPagoViewModel formaPagoViewModel, int Id)
        {
            ResponseData oR = new ResponseData();
            try
            {
                var formaPagoData = new FormaPago
                {
                    Id = Id,
                    Orden = formaPagoViewModel.Orden,
                    Descripcion = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(formaPagoViewModel.Descripcion)
                };

                FormaPagos.Update(formaPagoData);
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

        public ResponseData DeleteFormaPago(int Id)
        {
            ResponseData oR = new ResponseData();
            try
            {
                var formaPagoData = new FormaPago
                {
                    Id = Id
                };
                this.Remove(formaPagoData);
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
