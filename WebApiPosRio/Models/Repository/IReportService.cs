using System.Collections.Generic;
using WebApiPosRio.Models.DB;
using WebApiPosRio.Models.ViewModels;

namespace WebApiPosRio.Models.Repository
{
    public interface IReportService
    {
        byte[] GeneratePdfReport();
        List<SpResumenxCajaxFecha> ResumenCajaFecha(string fecha, string CodCaja);
        List<SpListadoDonaciones> ListadoDonaciones(ParamsLfViewModel filtroLF);
        List<SpListadoResumenVentaDepartamento> VentasPorDepartamento(InsideVentaViewModel filtroLF);
    }
}