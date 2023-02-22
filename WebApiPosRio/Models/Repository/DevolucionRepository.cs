using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPosRio.Models.ViewModels;
using WebApiPosRio.Models.DB;
using WebApiPosRio.Models.Response;

namespace WebApiPosRio.Models.Repository
{
    public class DevolucionRepository : RIOPOSContext, IDevolucionRepository
    {
        private ICajaRepository _cajaRepo;
        private ISpRepository _SpRepo;
        public DevolucionRepository(ICajaRepository cajaRepository, ISpRepository spRepository)
        {
            _cajaRepo = cajaRepository;
            _SpRepo = spRepository;
        }

        public ResponseData GetDevoluciones(DevolucionViewModel devolucionViewModel)
        {
            ResponseData Rd = new ResponseData
            {
                Success = 0,
                Data = null,
                Message = "No hay Informacion Disponible"
            };

            //var ddp = DateTime.Compare(DateTime.Parse("2021-03-14"), devolucionViewModel.Fecha);

            List<DevolucionViewModel> lstDev = null;
            try
            {
            lstDev = (from d in Devolucions
                        where devolucionViewModel.Fecha >=d.Fecha && d.Fecha <= devolucionViewModel.Fecha
                        select new DevolucionViewModel
                            {
                                Id = d.Id,
                                Monto = d.Monto,
                                FacturaId = d.FacturaId,
                                SerialFiscal = d.SerialFiscal,
                                TiendaId = d.TiendaId,
                                UsuarioId = d.UsuarioId,
                                Fecha = d.Fecha,
                            }).ToList();
            }
            catch(Exception ex)
            {
                Rd.Message = "Devoluciones: "+ex.Message.ToString();
            }
            if (lstDev != null)
            {
                Rd.Success = 1;
                Rd.Data = lstDev;
                Rd.Message = "Datos Encontrados!!";
            }

            return Rd;
        }

        public ResponseData GetIdDevolucionAsync(string id, string codCaja, int IdTurno)
        {
            int IdCaja = _cajaRepo.GetIdCajaByCodCaja(codCaja);
            ResponseData Rd = new ResponseData {
                Success = 0,
                Data = null,
                Message = "No hay Informacion Disponible"
            };

            DevolucionViewModel lstDev = (from d in Devolucions
                                         where d.NumeroDevolucion.Contains(id) && d.CajaId == IdCaja && d.TurnoId == IdTurno
                                         select new DevolucionViewModel
                                         {
                                             Id = d.Id,
                                             Monto = d.Monto,
                                             FacturaId = d.FacturaId,
                                             SerialFiscal = d.SerialFiscal,
                                             TiendaId = d.TiendaId,
                                             UsuarioId = d.UsuarioId,
                                             Fecha = d.Fecha,
                                         }).FirstOrDefault();

            if (lstDev == null)
            {
                return Rd;
            }

            Rd.Success = 1;
            Rd.Data = lstDev;
            Rd.Message = "Datos Encontrados!!";

            return Rd;
        }

        public ResponseData BuscarDevoluciones(int TurnoId)
        {
            ResponseData Rd = new ResponseData
            {
                Success = 0,
                Data = null,
                Message = "No hay Informacion Disponible"
            };

            var lstDev = _SpRepo.BuscarDevoluciones(TurnoId);

            lstDev[0].Total = lstDev[0].Total * -1;

            if (lstDev == null)
            {
                return Rd;
            }

            Rd.Success = 1;
            Rd.Data = lstDev;
            Rd.Message = "Datos Encontrados!!";

            return Rd;
        }

        public List<SpListadoDevoluciones> ListadoDevoluciones(DevolucionViewModel devolucionViewModel)
        {

            List<SpListadoDevoluciones> lstDev = _SpRepo.ListadoDevoluciones(devolucionViewModel.Fecha,devolucionViewModel.NumeroDevolucion, "0" ,devolucionViewModel.FacturaId);

            return lstDev;
        }
    }
}
