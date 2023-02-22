using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPosRio.Models.DB;
using WebApiPosRio.Models.Response;
using WebApiPosRio.Models.ViewModels;

namespace WebApiPosRio.Models.Repository
{
    public class TiendaRepository : RIOPOSContext, ITiendaRepository
    {
        public TiendaRepository()
        {
        }

        public List<TiendaViewModel> GetTiendas()
        {
            ResponseData oR = new ResponseData();
            List<TiendaViewModel> lstCajas = (from t in Tienda
                                              orderby t.Id descending
                                              select new TiendaViewModel
                                              {
                                                  Id = t.Id,
                                                  CodigoTienda = t.CodigoTienda,
                                                  CodigoArea = t.CodigoArea,
                                                  Nombre = t.Nombre,
                                                  Activo = t.Activo,
                                                  AplicaIva = t.AplicaIva,
                                                  UrlApi = t.Urlapi,
                                                  Tasa = t.Tasa,
                                                  DescuentoMax = t.DescuentoMax
                                              }).ToList();
            return lstCajas;
        }

        public string GetUrlTienda(int tiendaId)
        {
            var AccionItem =  Tienda.Find(tiendaId);

            if (AccionItem == null)
            {
                return null;
            }
            return AccionItem.Urlapi;
        }

        public List<TiendaSelectViewModel> GetTiendasSelect()
        {
            ResponseData oR = new ResponseData();
            List<TiendaSelectViewModel> lstTiendas = (from t in Tienda
                                              orderby t.Id descending
                                              select new TiendaSelectViewModel
                                              {
                                                  Value = t.Id,
                                                  Text = t.Nombre,
                                                  CodigoTienda = t.CodigoTienda,
                                                  CodigoArea = t.CodigoArea
                                              }).ToList();
            lstTiendas.Add(new TiendaSelectViewModel() { Value=-1, Text="Seleccione Una Tienda"});
            return lstTiendas.OrderBy(v => v.Value).ToList();
        }


        public async Task<TiendaViewModel> GetIdTiendaAsync(int id)
        {
            var AccionItem = await Tienda.FindAsync(id);

            if (AccionItem == null)
            {
                return null;
            }
            return new TiendaViewModel
            {
                Id = AccionItem.Id,
            };
        }

        public Tiendum GetTiendaByAcronimo(string Acronimo)
        {
            var  TiendabyAcron =  Tienda.Where(t => t.CodigoArea == Acronimo).FirstOrDefault();
          
            return TiendabyAcron;
        }

        public ResponseData AddTienda(TiendaViewModel accionViewModel)
        {
            ResponseData oR = new ResponseData();
            try
            {
                var accionData = new Tiendum
                {
                    //ModuloId = accionViewModel.ModuloId,
                    //Descripcion = Util.UFirst(accionViewModel.Descripcion),
                };

                Tienda.Add(accionData);
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

        public ResponseData UpdateTienda(TiendaViewModel accionViewModel, int Id)
        {
            ResponseData oR = new ResponseData();
            try
            {
                Tiendum accionData = new Tiendum
                {
                    Id = Id,
                };

                var RolAction = Tienda.Where(t => t.Id == Id).FirstOrDefault();
                RolAccion rolAccionData;
                if (RolAction != null) //si consigue un elemento actualiza en rol id 
                {
                    rolAccionData = new RolAccion
                    {
                        Id = RolAction.Id,
                    };
                    RolAccions.Attach(rolAccionData);
                    Entry(rolAccionData).Property(u => u.RolId).IsModified = true;
                    RolAccions.Update(rolAccionData);
                }
                else//sino inserto un elemento en Rol Accion
                {

                }
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

        public ResponseData DeleteTienda(int Id)
        {
            ResponseData oR = new ResponseData();
            try
            {
                var accionData = new Tiendum
                {
                    Id = Id
                };
                Tienda.Remove(accionData);
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
