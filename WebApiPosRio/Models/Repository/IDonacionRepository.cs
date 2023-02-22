using System;
using System.Collections.Generic;
using WebApiPosRio.Models.DB;
using WebApiPosRio.Models.ViewModels;

namespace WebApiPosRio.Models.Repository
{
    public interface IDonacionRepository
    {
        List<DonacionViewModel> GetDonaciones();
        List<DonacionViewModel> GetDonaciones(string Fecha, int ClienteId = 0, int OrganizacionId = 0, int CajaId = 0, int TurnoId = 0);
        object GetResumenDonaciones(string Fecha, int ClienteId = 0, int OrganizacionId = 0, int CajaId = 0, int TurnoId = 0);
        List<SpListadoDonaciones> ListadoDonaciones(ParamsLfViewModel filtroLF);
    }
}