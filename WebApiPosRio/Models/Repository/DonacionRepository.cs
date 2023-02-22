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
    public class DonacionRepository : RIOPOSContext, IDonacionRepository
    {
        private readonly ICajaAdministrativaRepository _CajaAdm;
        private readonly ISpRepository _SpRepo;
        public DonacionRepository(ICajaAdministrativaRepository cajaAdministrativaRepository, ISpRepository spRepository)
        {
            _CajaAdm = cajaAdministrativaRepository;
            _SpRepo = spRepository;
        }

        public List<DonacionViewModel> GetDonaciones()
        {
            List<DonacionViewModel> lstDonaciones = (from d in Donacions
                                                     select new DonacionViewModel
                                                     {
                                                         Id = d.Id,
                                                         Fecha = d.Fecha,
                                                         ClienteId = d.ClienteId,
                                                         OrganizacionId = d.OrganizacionId,
                                                         Estatus = d.Estatus,
                                                         CajaId = d.CajaId,
                                                         TurnoId = d.TurnoId,
                                                         CajeraId = d.CajeraId,
                                                         FormaPagoId = d.FormaPagoId,
                                                         MontoDonado = d.MontoDonado,
                                                         Tasa = d.Tasa,
                                                         TiendaId = d.TiendaId,
                                                         BancoAdquirienteId = d.BancoAdquirienteId,
                                                         Lote = d.Lote,
                                                         NumeroTransacion = d.NumeroTransacion,
                                                         Nombre = d.Nombre,
                                                         TipoTarjeta = d.TipoTarjeta,
                                                         NroAutorizacion = d.NroAutorizacion,
                                                         Vposdata = d.Vposdata
                                                     }).ToList();

            return lstDonaciones.OrderByDescending(lm => lm.Id).ToList();
        }

        public List<DonacionViewModel> GetDonaciones(string  Fecha, int ClienteId=0, int OrganizacionId=0, int CajaId=0, int TurnoId=0 )
        {
            var f = Convert.ToDateTime(Fecha);

            List<DonacionViewModel> lstDonaciones = (from d in Donacions
                                                     where (d.TurnoId == TurnoId || TurnoId==0) && (d.CajaId == CajaId || CajaId==0)
                                                     select new DonacionViewModel
                                                     {
                                                         Id = d.Id,
                                                         Fecha = d.Fecha,
                                                         ClienteId = d.ClienteId,
                                                         OrganizacionId = d.OrganizacionId,
                                                         Estatus = d.Estatus,
                                                         CajaId = d.CajaId,
                                                         TurnoId = d.TurnoId,
                                                         CajeraId = d.CajeraId,
                                                         FormaPagoId = d.FormaPagoId,
                                                         MontoDonado = d.MontoDonado,
                                                         Tasa = d.Tasa,
                                                         TiendaId = d.TiendaId,
                                                         BancoAdquirienteId = d.BancoAdquirienteId,
                                                         Lote = d.Lote,
                                                         NumeroTransacion = d.NumeroTransacion,
                                                         Nombre = d.Nombre,
                                                         TipoTarjeta = d.TipoTarjeta,
                                                         NroAutorizacion = d.NroAutorizacion,
                                                         Vposdata = d.Vposdata
                                                     }).ToList();

            lstDonaciones = lstDonaciones.Where(d => d.Fecha.ToString("yyyy-MM-dd") == Fecha).ToList();

            return lstDonaciones.OrderByDescending(lm => lm.Id).ToList();
        }

        public object GetResumenDonaciones(string Fecha, int ClienteId = 0, int OrganizacionId = 0, int CajaId = 0, int TurnoId = 0)
        {
            var f = Convert.ToDateTime(Fecha);

            List<DonacionViewModel> lstDonaciones = (from d in Donacions
                                                     where (d.TurnoId == TurnoId || TurnoId == 0) && (d.CajaId == CajaId || CajaId == 0)
                                                     select new DonacionViewModel
                                                     {
                                                         Id = d.Id,
                                                         Fecha = d.Fecha,
                                                         ClienteId = d.ClienteId,
                                                         OrganizacionId = d.OrganizacionId,
                                                         Estatus = d.Estatus,
                                                         CajaId = d.CajaId,
                                                         TurnoId = d.TurnoId,
                                                         CajeraId = d.CajeraId,
                                                         FormaPagoId = d.FormaPagoId,
                                                         MontoDonado = d.MontoDonado,
                                                         Tasa = d.Tasa,
                                                         TiendaId = d.TiendaId,
                                                         BancoAdquirienteId = d.BancoAdquirienteId,
                                                         Lote = d.Lote,
                                                         NumeroTransacion = d.NumeroTransacion,
                                                         Nombre = d.Nombre,
                                                         TipoTarjeta = d.TipoTarjeta,
                                                         NroAutorizacion = d.NroAutorizacion,
                                                         Vposdata = d.Vposdata
                                                     }).ToList();

            lstDonaciones = lstDonaciones.Where(d => d.Fecha.ToString("yyyy-MM-dd") == Fecha).ToList();

            var fp = _CajaAdm.GetFormasPago();

            var donacionesList = new
            {
                donacionAcum = lstDonaciones.GroupBy(xpp => xpp.FormaPagoId).Select(
                        g => new
                        {
                            Key = g.Key,
                            Acum = g.Sum(s => s.MontoDonado),
                            FormaPago = fp.FirstOrDefault(x => x.Id == g.Key).Nombre
                                    //FormaPagoId = g.Select(x =>x.FormaPagoId).FirstOrDefault()
                        }
                    ),                  
             };

            return donacionesList;
        }

        public List<SpListadoDonaciones> ListadoDonaciones(ParamsLfViewModel filtroLF)
        {
            return _SpRepo.ListadoDonacion(filtroLF);
        }
    }
}
