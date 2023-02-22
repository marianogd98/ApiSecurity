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
    public class RolRepository : RIOPOSContext, IRolRepository
    {
        ISpRepository _spRepository;
        public RolRepository(ISpRepository spRepository) {
            _spRepository = spRepository;
        }

        public IEnumerable<RolViewModel> GetRols(int page, int perPage)
        {
            List<RolViewModel> lstRol = (from d in Rols
                                         where d.Id>0
                                         select new RolViewModel
                                         {
                                             Id = d.Id,
                                             Descripcion = d.Descripcion,
                                             Activo = d.Activo,
                                             CreatedAt = d.CreatedAt.ToString(),
                                             UpdatedAt = d.UpdatedAt.ToString()
                                         }).ToList();

            return lstRol.ToPagedList(page, perPage); ;
        }

        public async Task<RolViewModel> GetIdRolAsync(int id)
        {
            var rolItem = await Rols.FindAsync(id);

            if (rolItem == null)
            {
                return null;
            }
            return new RolViewModel
            {
                Id = rolItem.Id,
                Descripcion = rolItem.Descripcion
            };
        }

        public ResponseData AddRol(RolViewModel rolViewModel)
        {
            ResponseData oR = new ResponseData();
            try
            {
                var rolData = new Rol
                {
                    Descripcion = Util.UFirst(rolViewModel.Descripcion)
                };
                                     
                Rols.Add(rolData);
                this.SaveChanges();
                oR.Success = 1;
                oR.Data = null;// GetRols();
                oR.Message = "Datos Almacenados Satisfactoriamente";
            }
            catch (Exception ex)
            {
                oR.Success = 0;
                oR.Message = ex.Message;
            }
            return oR;
        }

        public ResponseData UpdateRol(RolViewModel rolViewModel, int Id)
        {
            ResponseData oR = new ResponseData();
            try
            {
                int r = _spRepository.CrudRol(rolViewModel, 2);
                oR.Success = r;
                oR.Message = r==1?"Datos Actualizados Satisfactoriamente":"Error Actualizando Rol, Verifique los datos";
            }
            catch (Exception ex)
            {
                oR.Success = 0;
                oR.Message = ex.Message;
            }
            return oR;
        }

        public ResponseData DeleteRol(int Id)
        {
            ResponseData oR = new ResponseData();
            try
            {
                int r = _spRepository.CrudRol(new RolViewModel { Id = Id}, 3);
                oR.Success = r;                
                oR.Message = r==1?"Registro eliminado Satisfactoriamente":"Ocurrio un error Eliminanto el rol";
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
