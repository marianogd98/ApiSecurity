
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiPosRio.Models.DB;
using WebApiPosRio.Models.ViewModels;

namespace WebApiPosRio.Models.Repository
{
    public class ReportService : IReportService
    {
        private readonly ISpRepository _SpRepo;
        public ReportService(ISpRepository spRepo)
        {
            _SpRepo = spRepo;
        }

        public byte[] GeneratePdfReport()
        {
            return Encoding.ASCII.GetBytes("hola");
        }

        public List<SpResumenxCajaxFecha> ResumenCajaFecha(string fecha, string CodCaja)
        {
            return _SpRepo.ResumenxCajaxFechas(fecha, CodCaja);
        }

        public List<SpListadoDonaciones> ListadoDonaciones(ParamsLfViewModel filtroLF)
        {
            return _SpRepo.ListadoDonacion(filtroLF);
        }

        public List<SpListadoResumenVentaDepartamento> VentasPorDepartamento(InsideVentaViewModel filtroLF)
        {
            return _SpRepo.ListadoResumenVentaDepartamentoInside(filtroLF.tiendaId, filtroLF.FechaIni, filtroLF.FechaFin);
        }

    }
}
