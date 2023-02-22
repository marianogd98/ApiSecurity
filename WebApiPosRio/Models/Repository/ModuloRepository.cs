using Microsoft.EntityFrameworkCore;
using X.PagedList;
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
    public class ModuloRepository : RIOPOSContext, IModuloRepository
    {
        public ModuloRepository()
        {
        }

        public IEnumerable<ModuloViewModel> GetModulos(int page, int perPage)
        {
            List<ModuloViewModel> lstModulo = (from d in Modulos                                               
                                               select new ModuloViewModel
                                               {
                                                   Id = d.Id,
                                                   Descripcion = d.Descripcion,
                                                   CreatedAt = d.CreatedAt.ToString(),
                                                   UpdatedAt = d.UpdatedAt.ToString()
                                               }).ToList();

            return lstModulo.ToPagedList(page, perPage);
        }

        public List<ModuloViewModel> GetModulos()
        {
            List<ModuloViewModel> lstModulo = (from d in Modulos
                                               select new ModuloViewModel
                                               {
                                                   Id = d.Id,
                                                   Descripcion = d.Descripcion,
                                                   CreatedAt = d.CreatedAt.ToString(),
                                                   UpdatedAt = d.UpdatedAt.ToString()
                                               }).ToList();

            return lstModulo.OrderByDescending(lm => lm.Id).ToList();
        }

        public async Task<ModuloViewModel> GetIdModuloAsync(int id)
        {
            var ModuloItem = await Modulos.FindAsync(id);

            if (ModuloItem == null)
            {
                return null;
            }
            return new ModuloViewModel
            {
                Id = ModuloItem.Id,
                Descripcion = ModuloItem.Descripcion
            };
        }

        public ResponseData AddModulo(ModuloViewModel moduloViewModel)
        {
            ResponseData oR = new ResponseData();
            try
            {
                var moduloData = new Modulo
                {
                    Descripcion = Util.UFirst(moduloViewModel.Descripcion)
                };

                Modulos.Add(moduloData);
                this.SaveChanges();
                oR.Success = 1;
                oR.Data = GetModulos();
                oR.Message = "Datos Almacenados Satisfactoriamente";
            }
            catch (Exception ex)
            {
                oR.Success = 0;
                oR.Message = ex.Message;
            }
            return oR;
        }

        public ResponseData UpdateModulo(ModuloViewModel moduloViewModel, int Id)
        {
            ResponseData oR = new ResponseData();
            try
            {
                var moduloData = new Modulo
                {
                    Id = Id,
                    Descripcion = Util.UFirst(moduloViewModel.Descripcion)
                };

                Modulos.Update(moduloData);
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

        public ResponseData DeleteModulo(int Id)
        {
            ResponseData oR = new ResponseData();
            try
            {
                var moduloData = new Modulo
                {
                    Id = Id
                };
                this.Remove(moduloData);
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
