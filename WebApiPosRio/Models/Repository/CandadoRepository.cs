using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using WebApiPosRio.Models.DB;
using WebApiPosRio.Models.ViewModels;

namespace WebApiPosRio.Models.Repository
{
    public class CandadoRepository : BIOContext, ICandadoRepository
    {
        public int CrearHorario(string Descripcion, string HoraInicio, string HoraFin, int Ciclo, int Activo)
        {
            try
            {
                using (var context = new SpContextB())
                {
                    var parametro = new List<SqlParameter>()
                    {
                        new SqlParameter("@Descripcion", Descripcion),
                        new SqlParameter("@HoraInicio", HoraInicio),
                        new SqlParameter("@HoraFin", HoraFin),
                        new SqlParameter("@Ciclo", Ciclo),
                        new SqlParameter("@Activo", Activo)
                    };

                    List<SpCrearHorarios> data = context.CrearHorario
                        .FromSqlRaw("EXEC [dbo].[CrearHorarios] @Descripcion=@Descripcion, @HoraInicio=@HoraInicio, @HoraFin=@HoraFin, @Ciclo=@Ciclo, @Activo=@Activo", parametro.ToArray()).ToList();

                    return (int)data[0].IdTurno;
                }
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public int AsignarDias(int IdTurno, string Descripcion)
        {
            try
            {
                using (var context = new SpContextB())
                {
                    var parametro = new List<SqlParameter>()
                    {
                        new SqlParameter("@Turno", IdTurno),
                        new SqlParameter("@Descripcion", Descripcion)
                    };

                    List<SpAsignarDiasxTurnos> data = context.AsignarDiasxTurnos
                        .FromSqlRaw("EXEC [dbo].[AsignarDiasxTurnos] @Descripcion=@Descripcion, @IdTurno=@IdTurno", parametro.ToArray()).ToList();

                    return (int)data[0].r;
                }
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public int AsignarHorario(int Cedula, int Turno, DateTime FechaInicio, DateTime FechaFin, int Activo)
        {
            try
            {
                using (var context = new SpContextB())
                {
                    var parametro = new List<SqlParameter>()
                    {
                        new SqlParameter("@Cedula", Cedula),
                        new SqlParameter("@Turno", Turno),
                        new SqlParameter("@FechaInicio", FechaInicio),
                        new SqlParameter("@FechaFin", FechaFin),
                        new SqlParameter("@Activo", Activo)
                    };

                    List<SpAsignarHorariosxEmpleados> data = context.AsignarHorariosxEmpleado
                        .FromSqlRaw("EXEC [dbo].[AsignarHorariosxEmpleados] @Cedula=@Cedula, @Turno=@Turno, @FechaInicio=@FechaInicio, @FechaFin=@FechaFin, @Activo=@Activo", parametro.ToArray()).ToList();

                    return (int)data[0].r;
                }
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public List<SpBuscarEmpleados> BuscarEmpleados(int Cedula, string Nombre, string Cargo, int Activo)
        {
            try
            {
                using (var context = new SpContextB())
                {
                    var prmCi = new SqlParameter("@Cedula", Cedula);
                    var prmName = new SqlParameter("@Nombre", Nombre);
                    var prmCargo = new SqlParameter("@Cargo", Cargo);
                    var prmAct = new SqlParameter("@Activo", Activo);

                    List<SpBuscarEmpleados> data = context.BuscarEmpleados
                        .FromSqlRaw("EXEC [dbo].[BuscarEmpleados] @Cedula=@Cedula, @Nombre=@Nombre, @Cargo=@Cargo, @Activo=@Activo", prmCi, prmName, prmCargo, prmAct).ToList();

                    return data;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<SpBuscarTurnos> BuscarTurnos(int IdTurno, string Descripcion, string Fecha_Creacion, string Fecha_Modificacion, int Activo)
        {
            try
            {
                using (var context = new SpContextB())
                {
                    var prmTurno = new SqlParameter("@IdTurno", IdTurno);
                    var prmDesc = new SqlParameter("@Descripcion", Descripcion);
                    var prmFechaC = new SqlParameter("@Fecha_Creacion", Fecha_Creacion);
                    var prmFechaM = new SqlParameter("@Fecha_Modificacion", Fecha_Modificacion);
                    var prmAct = new SqlParameter("@Activo", Activo);

                    List<SpBuscarTurnos> data = context.BuscarTurnos
                        .FromSqlRaw("EXEC [dbo].[BuscarTurnos] @IdTurno=@IdTurno, @Descripcion=@Descripcion, @Fecha_Creacion=@Fecha_Creacion, @Fecha_Modificacion=@Fecha_Modificacion, @Activo=@Activo", prmTurno, prmDesc, prmFechaC, prmFechaM, prmAct).ToList();

                    return data;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
