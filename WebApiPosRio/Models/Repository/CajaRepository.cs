using Microsoft.EntityFrameworkCore;
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
    public class CajaRepository : RIOPOSContext, ICajaRepository
    {
        public CajaRepository()
        {
        }

        public List<CajaViewModel> GetCajas(Cajafiltro filtro)
        {
            ResponseData oR = new ResponseData();
            List<CajaViewModel> lstCajas = (from c in Cajas
                                            join t in Tienda on c.TiendaId equals t.Id
                                            join a in Areas on c.AreaId equals a.Id
                                            where ((filtro.TiendaId>-1 && filtro.TiendaId == c.TiendaId) || filtro.TiendaId==-1) &&
                                                    (c.Estatus == filtro.Estatus)
                                               orderby c.Id descending
                                               select new CajaViewModel
                                               {
                                                    Id = c.Id,
                                                    TiendaId = c.TiendaId,
                                                    TiendaNombre = t.Nombre,
                                                    CodigoCaja = c.CodigoCaja,
                                                    Consecutivo = c.Consecutivo,
                                                    PuertoBalanza = c.PuertoBalanza,
                                                    PuertoCodigoBarra = c.PuertoCodigoBarra,
                                                    SerialImpresora = c.SerialImpresora,
                                                    Vtid = c.Vtid,
                                                    AreaId = c.AreaId,
                                                    AreaNombre = a.Descripcion,
                                                    AbrirGaveta= c.AbrirGaveta,
                                                    FacturaAlMayor= c.FacturaAlMayor,
                                                    Estatus = c.Estatus
                                               }).ToList();

            if (lstCajas != null)
            {
                if (filtro.CodCaja != null)
                {
                    lstCajas = lstCajas.Where(nu => nu.CodigoCaja.Contains(filtro.CodCaja)).ToList();
                }

                if (filtro.CajaId > 0)
                {
                    lstCajas = lstCajas.Where(nu => nu.Id == filtro.CajaId).ToList();
                }

                if (filtro.AreaId > 0)
                {
                    lstCajas = lstCajas.Where(nu => nu.AreaId == filtro.AreaId).ToList();
                }
            }
            return lstCajas;
        }

        public List<CajaViewModel> GetCajas(int TiendaId)
        {
            ResponseData oR = new ResponseData();
            List<CajaViewModel> lstCajas = (from c in Cajas
                                            join t in Tienda on c.TiendaId equals t.Id
                                            join a in Areas on c.AreaId equals a.Id
                                            where c.TiendaId == TiendaId
                                            //orderby c.Id descending
                                            select new CajaViewModel
                                            {
                                                Id = c.Id,
                                                TiendaId = c.TiendaId,
                                                TiendaNombre = t.Nombre,
                                                CodigoCaja = c.CodigoCaja,
                                                Consecutivo = c.Consecutivo,
                                                PuertoBalanza = c.PuertoBalanza,
                                                PuertoCodigoBarra = c.PuertoCodigoBarra,
                                                SerialImpresora = c.SerialImpresora,
                                                Vtid = c.Vtid,
                                                AreaId = c.AreaId,
                                                AreaNombre = a.Descripcion,
                                                AbrirGaveta = c.AbrirGaveta,
                                                FacturaAlMayor = c.FacturaAlMayor,
                                                Estatus = c.Estatus
                                            }).ToList();
            return lstCajas;
        }

        public int GetIdCajaByCodCaja(string codCaja)
        {
           
            var Caja = (from d in Cajas
                             where d.CodigoCaja.Contains(codCaja)                             
                             select new
                             {
                                 Id = d.Id
                             }).FirstOrDefault();
            return Caja.Id;
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

        public object GetCajaProfile(int idUser, string Item)
        {
            ResponseData oR = new ResponseData();
            var user = Usuarios.Find(idUser);
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

        public CajaViewModel GetIdCajaAsync(int id, bool esTurno = false)
        {

            CajaViewModel cajaItem = null;

            if (!esTurno)
            {
                cajaItem = (from c in Cajas
                            where c.Id == id
                            select (
                                  new CajaViewModel
                                  {
                                      Id = c.Id,
                                      TiendaId = c.TiendaId,
                                      //TiendaNombre = t.Nombre,
                                      CodigoCaja = c.CodigoCaja,
                                      Consecutivo = c.Consecutivo,
                                      PuertoBalanza = c.PuertoBalanza,
                                      PuertoCodigoBarra = c.PuertoCodigoBarra,
                                      SerialImpresora = c.SerialImpresora,
                                      Vtid = c.Vtid,
                                      AreaId = c.AreaId,
                                      //AreaNombre = a.Descripcion,
                                      AbrirGaveta = c.AbrirGaveta,
                                      FacturaAlMayor = c.FacturaAlMayor,
                                      Estatus = c.Estatus
                                  })).FirstOrDefault();
            }
            else
            {
                cajaItem = (from c in Cajas
                            join t in Turnos on c.Id equals t.CajaId
                            where t.Id == id
                            select (
                                new CajaViewModel
                                {
                                    Id = c.Id,
                                    TiendaId = c.TiendaId,
                                    //TiendaNombre = t.Nombre,
                                    CodigoCaja = c.CodigoCaja,
                                    Consecutivo = c.Consecutivo,
                                    PuertoBalanza = c.PuertoBalanza,
                                    PuertoCodigoBarra = c.PuertoCodigoBarra,
                                    SerialImpresora = c.SerialImpresora,
                                    Vtid = c.Vtid,
                                    AreaId = c.AreaId,
                                    //AreaNombre = a.Descripcion,
                                    AbrirGaveta = c.AbrirGaveta,
                                    FacturaAlMayor = c.FacturaAlMayor,
                                    Estatus = c.Estatus
                                })).FirstOrDefault();
            }

            return cajaItem;
        }

        public ResponseData AddCaja(CajaViewModel cajaViewModel)
        {
            ResponseData oR = new ResponseData();
            try
            {
                var cajaData = new Caja
                {
                    CodigoCaja = cajaViewModel.CodigoCaja,
                    BancoId = 1,
                    Consecutivo = cajaViewModel.Consecutivo,
                    Vtid = cajaViewModel.Vtid,
                    SerialImpresora = cajaViewModel.SerialImpresora.ToUpper(),
                    TiendaId = cajaViewModel.TiendaId,
                    AreaId = cajaViewModel.AreaId,
                    Estatus = cajaViewModel.Estatus,
                    PuertoBalanza = cajaViewModel.PuertoBalanza.ToUpper(),
                    PuertoCodigoBarra = cajaViewModel.PuertoCodigoBarra.ToUpper(),
                    PuertoImpresora = cajaViewModel.PuertoImpresora.ToUpper()
                    //Descripcion = Util.UFirst(accionViewModel.Descripcion),
                };

                Cajas.Add(cajaData);
                this.SaveChanges();

                oR.Success = 1;
                oR.Message = "Datos Almacenados Satisfactoriamente";
            }
            catch (Exception ex)
            {
                oR.Success = 0;
                oR.Message = ex.Message;
            }
            return oR;
        }

        public ResponseData UpdateCaja(CajaViewModel cajaViewModel, int Id)
        {
            ResponseData oR = new ResponseData();
            try
            {
                Caja cajaData = new Caja
                {
                    Id = Id,
                    CodigoCaja = cajaViewModel.CodigoCaja,
                    Consecutivo = cajaViewModel.Consecutivo,
                    Vtid = cajaViewModel.Vtid,
                    SerialImpresora = cajaViewModel.SerialImpresora,
                    FacturaAlMayor = cajaViewModel.FacturaAlMayor,
                    AbrirGaveta = cajaViewModel.AbrirGaveta,
                    TiendaId = cajaViewModel.TiendaId,
                    AreaId = cajaViewModel.AreaId,
                    Estatus = cajaViewModel.Estatus,
                    PuertoBalanza = (cajaViewModel.PuertoCodigoBarra != null) ? cajaViewModel.PuertoBalanza.ToUpper():"",
                    PuertoCodigoBarra = (cajaViewModel.PuertoCodigoBarra != null)?cajaViewModel.PuertoCodigoBarra.ToUpper():"",
                    PuertoImpresora = (cajaViewModel.PuertoImpresora!=null)?cajaViewModel.PuertoImpresora.ToUpper():""
                };

                //Cajas.Update(cajaData);
                Cajas.Attach(cajaData);

                Entry(cajaData).Property(u => u.CodigoCaja).IsModified = true;
                Entry(cajaData).Property(u => u.Consecutivo).IsModified = true;
                Entry(cajaData).Property(u => u.Vtid).IsModified = true;
                Entry(cajaData).Property(u => u.SerialImpresora).IsModified = true;
                Entry(cajaData).Property(u => u.FacturaAlMayor).IsModified = true;
                Entry(cajaData).Property(u => u.AbrirGaveta).IsModified = true;
                Entry(cajaData).Property(u => u.TiendaId).IsModified = true;
                Entry(cajaData).Property(u => u.AreaId).IsModified = true;
                Entry(cajaData).Property(u => u.Estatus).IsModified = true;
                Entry(cajaData).Property(u => u.PuertoBalanza).IsModified = true;
                Entry(cajaData).Property(u => u.PuertoCodigoBarra).IsModified = true;
                Entry(cajaData).Property(u => u.PuertoImpresora).IsModified = true;


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

        public ResponseData DeleteCaja(int Id)
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
