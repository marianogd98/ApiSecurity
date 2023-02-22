using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPosRio.Models.DB;
using WebApiPosRio.Models.ViewModels;

namespace WebApiPosRio.Models.Repository
{
    public interface ICandadoRepository 
    {
        int CrearHorario(string Descripcion, string HoraInicio, string HoraFin, int Ciclo, int Activo);
        int AsignarDias(int IdTurno, string Descripcion );
        int AsignarHorario(int Cedula, int Turno, DateTime FechaInicio, DateTime FechaFin, int Activo);
        List<SpBuscarTurnos> BuscarTurnos(int IdTurno, string Descripcion, string Fecha_Creacion, string Fecha_Modificacion, int Activo);
        List<SpBuscarEmpleados> BuscarEmpleados(int Cedula, string Nombre, string Cargo, int Activo);
    }
}
