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
    public class AccionRepository : RIOPOSContext, IAccionRepository
    {
        
        public AccionRepository( )
        {
            //_rolAccion = rolAccion;
        }

        public /*List<AccionViewModel>*/ IEnumerable<AccionViewModel> GetAccions()
        {
            ResponseData oR = new ResponseData();
            List<AccionViewModel> lstAccion = (from d in Accions
                                               orderby d.Id descending
                                               select new AccionViewModel
                                               {
                                                   Id = d.Id,
                                                   ModuloId = d.ModuloId,
                                                   Descripcion = d.Descripcion,
                                                   CreatedAt = d.CreatedAt.ToString(),
                                                   UpdatedAt = d.UpdatedAt.ToString()
                                               }).ToList();

            return lstAccion;
        }

        public IEnumerable<AccionViewModel>  GetModuloIdAccionAsync(int ModuloId)
        {
            List<AccionViewModel> lstAccion = (from d in Accions
                                               where d.ModuloId == ModuloId
                                               orderby d.Id descending
                                               select new AccionViewModel
                                               {
                                                   Id = d.Id,
                                                   ModuloId = d.ModuloId,
                                                   Descripcion = d.Descripcion,
                                                   CreatedAt = d.CreatedAt.ToString(),
                                                   UpdatedAt = d.UpdatedAt.ToString()
                                               }).ToList();

            return lstAccion;
        }

        public IEnumerable<AccionViewModel> GetAccions(int page, int perPage)
        {
            ResponseData oR = new ResponseData();
            List<AccionViewModel> lstAccion = (from d in Accions
                                               orderby d.Id descending
                                               select new AccionViewModel
                                               {
                                                   Id = d.Id,
                                                   ModuloId = d.ModuloId,
                                                   Descripcion = d.Descripcion,
                                                   CreatedAt = d.CreatedAt.ToString(),
                                                   UpdatedAt = d.UpdatedAt.ToString()
                                               }).ToList();

            var paginate = lstAccion.ToPagedList(page, perPage);

            return paginate;
        }

        public object GetAccionsProfile()
        {
            ResponseData oR = new ResponseData();
            var lstAccion = (from d in Accions
                             orderby d.Id descending
                             select new 
                             {
                              Id = d.Id,
                              ModuloId = d.ModuloId,
                              Descripcion = d.Descripcion,
                                  }).ToList();
            return lstAccion;
        }

        public object GetAccionsProfile(int idUser, string Item)
        {
            ResponseData oR = new ResponseData();

            var user = Usuarios.Find(idUser);

            if (Util.EsNumero(Item))
            {
                var accionesProfile =
                    (from ua in UsuarioAccions
                     join a in Accions
                     on ua.AccionId equals a.Id
                     where ua.UsuarioId == idUser && a.Id == Int32.Parse(Item)
                     select new
                     {
                         IdAccion = ua.AccionId,
                         Nombre = a.Descripcion,
                         Permitido = true
                     })
                    .Union
                        (from ra in RolAccions
                         join a in Accions
                         on ra.AccionId equals a.Id
                         where ra.RolId == user.RolId && a.Id==Int32.Parse(Item)
                         select new
                         {
                             IdAccion = ra.AccionId,
                             Nombre = a.Descripcion,
                             Permitido = true
                         });
                    try
                    {
                        return (accionesProfile.Count(c => c.IdAccion > 0) > 0) ? accionesProfile : null;
                    }
                    catch
                    {
                        return null;
                    }
            }
            else
            {
                var accionesProfile =
                    (from ua in UsuarioAccions
                     join a in Accions
                     on ua.AccionId equals a.Id
                     where ua.UsuarioId == idUser && a.Descripcion.Contains(Item)
                     select new
                     {
                         IdAccion = ua.AccionId,
                         Nombre = a.Descripcion,
                         Permitido = true
                     })
                    .Union
                        (from ra in RolAccions
                         join a in Accions
                         on ra.AccionId equals a.Id
                         where ra.RolId == user.RolId && a.Descripcion.Contains(Item)
                         select new
                         {
                             IdAccion = ra.AccionId,
                             Nombre = a.Descripcion,
                             Permitido = true
                         });
                    try
                    {
                        return (accionesProfile.Count(c => c.IdAccion > 0) > 0) ? accionesProfile : null;
                    }
                    catch
                    {
                        return null;
                    }
            }
        }

        public async Task<AccionViewModel> GetIdAccionAsync(int id)
        {
            var AccionItem = await Accions.FindAsync(id);

            if (AccionItem == null)
            {
                return null;
            }
            return new AccionViewModel
            {
                Id = AccionItem.Id,
                Descripcion = AccionItem.Descripcion,
                ModuloId = AccionItem.ModuloId,
                CreatedAt = AccionItem.CreatedAt.ToString(),
                UpdatedAt = AccionItem.UpdatedAt.ToString()
            };
        }

        public ResponseData AddAccion(AccionViewModel accionViewModel)
        {
            ResponseData oR = new ResponseData();
            try
            {
                var accionData = new Accion
                {
                    ModuloId = accionViewModel.ModuloId,
                    Descripcion = Util.UFirst(accionViewModel.Descripcion),                    
                };

                Accions.Add(accionData);
                this.SaveChanges();

                var rolAccion = new RolAccion
                {
                    RolId = accionViewModel.RolId,
                    AccionId = accionData.Id
                };
                RolAccions.Add(rolAccion);
                this.SaveChanges();

                oR.Success = 1;
                oR.Data = GetAccions();
                oR.Message = "Datos Almacenados Satisfactoriamente";
            }
            catch (Exception ex)
            {
                oR.Success = 0;
                oR.Message = ex.Message;
            }
            return oR;
        }

        public ResponseData UpdateAccion(AccionViewModel accionViewModel, int Id)
        {
            ResponseData oR = new ResponseData();
            try
            {
                Accion accionData = new Accion
                {
                    Id = Id,
                    ModuloId = accionViewModel.ModuloId,
                    Descripcion = Util.UFirst(accionViewModel.Descripcion),
                    //UpdatedAt = new DateTime().ToLocalTime()
                };

                Accions.Attach(accionData);
                Entry(accionData).Property(u => u.Descripcion).IsModified = true;
                Entry(accionData).Property(u => u.ModuloId).IsModified = true;
                //Entry(accionData).Property(u => u.UpdatedAt).IsModified = true;
                Accions.Update(accionData);
                this.SaveChanges();

                oR.Success = 1;
                oR.Data = GetAccions();
                oR.Message = "Datos Actualizados Satisfactoriamente";
            }
            catch (Exception ex)
            {
                oR.Success = 0;
                oR.Message = ex.Message;
            }
            return oR;
        }

        public ResponseData DeleteAccion(int Id)
        {
            ResponseData oR = new ResponseData();
            try
            {
                var accionData = new Accion
                {
                    Id = Id
                };
                Accions.Remove(accionData);
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
