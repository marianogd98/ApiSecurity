using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPosRio.Models.DB;
using WebApiPosRio.Models.Helper;
using WebApiPosRio.Models.Response;
using WebApiPosRio.Models.ViewModels;

namespace WebApiPosRio.Models.Repository
{
    public class AreaRepository : RIOPOSContext, IAreaRepository
    {
        public AreaRepository()
        {
        }

        public List<AreaViewModel> GetArea()
        {
            ResponseData oR = new ResponseData();
            List<AreaViewModel> lstArea = (from a in Areas
                                           orderby a.Id descending
                                           select new AreaViewModel
                                           {
                                               Id = a.Id,
                                               Descripcion = a.Descripcion,
                                               TiendaId = a.TiendaId,
                                           }).ToList();
            return lstArea;
        }

        public List<AreaSelectViewModel> GetSelectArea(int tiendaId)
        {
            ResponseData oR = new ResponseData();
            List<AreaSelectViewModel> lstArea = (from a in Areas
                                                 where a.TiendaId == tiendaId
                                           orderby a.Id descending
                                           select new AreaSelectViewModel
                                           {
                                               Value = a.Id,
                                               Text = a.Descripcion,                                               
                                           }).ToList();
            return lstArea;
        }

        public async Task<AreaViewModel> GetIdAreaAsync(int id)
        {
            var AccionItem = await Areas.FindAsync(id);

            if (AccionItem == null)
            {
                return null;
            }
            return new AreaViewModel
            {
                Id = AccionItem.Id,
                Descripcion = AccionItem.Descripcion,
                //ModuloId = AccionItem.ModuloId,
                //CreatedAt = AccionItem.CreatedAt.ToString(),
                //UpdatedAt = AccionItem.UpdatedAt.ToString()
            };
        }

        public ResponseData AddArea(AreaViewModel accionViewModel)
        {
            ResponseData oR = new ResponseData();
            try
            {
                var accionData = new Area
                {
                    TiendaId = accionViewModel.TiendaId,
                    Descripcion = Util.UFirst(accionViewModel.Descripcion),
                };

                Areas.Add(accionData);
                this.SaveChanges();

                oR.Success = 1;
                // oR.Data = GetAccions();
                oR.Message = "Datos Almacenados Satisfactoriamente";
            }
            catch (Exception ex)
            {
                oR.Success = 0;
                oR.Message = ex.Message;
            }
            return oR;
        }

        public ResponseData UpdateArea(AreaViewModel areaViewModel, int Id)
        {
            ResponseData oR = new ResponseData();
            try
            {
                Area areaData = new Area
                {
                    Id = Id,
                    Descripcion = Util.UFirst(areaViewModel.Descripcion),
                    TiendaId = areaViewModel.TiendaId
                };

                Areas.Attach(areaData);
                Entry(areaData).Property(u => u.Descripcion).IsModified = true;
                Entry(areaData).Property(u => u.TiendaId).IsModified = true;
                Areas.Update(areaData);
                this.SaveChanges();


                oR.Success = 1;
                //oR.Data = GetAccions();
                oR.Message = "Datos Actualizados Satisfactoriamente";
            }
            catch (Exception ex)
            {
                oR.Success = 0;
                oR.Message = ex.Message;
            }
            return oR;
        }

        public ResponseData DeleteArea(int Id)
        {
            ResponseData oR = new ResponseData();
            try
            {
                var areaData = new Area
                {
                    Id = Id
                };

                Areas.Remove(areaData);
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
