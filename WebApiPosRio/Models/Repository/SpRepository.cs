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
    public class SpRepository : RIOPOSContext, ISpRepository
    {
        public void SetAccionesUsuario(XmlDocument xml)
        {
            using (var context = new SpContext())
            {
                var texto = new SqlParameter("@Acciones", xml.InnerXml);
                context.Database.ExecuteSqlRaw("EXEC [dbo].[ActualizarAcciones] @Acciones", texto);
            }
        }

        public SpPruebaData VerificarTurno(int IdUsuario, int IdCaja)
        {
            using (var context = new SpContext())
            {
                //params
                var paramIdUser = new SqlParameter("@IdUsuario", IdUsuario);
                var paramIdCaja = new SqlParameter("@Idcaja", IdCaja);
                var data = context.SpDatabase
                    .FromSqlRaw("EXEC [dbo].[AbrirTurno] @IdUsuario=@IdUsuario, @Idcaja=@Idcaja", paramIdUser, paramIdCaja).ToList();
                SpPruebaData respuesta = null;
                foreach (SpPruebaData d in data)
                {
                    respuesta = new SpPruebaData() { Estatus = d.Estatus, Id = d.Id };
                }

                return respuesta;
            }
        }

        public List<SpDetalleMoneda> DetalleMoneda()
        {
            using (var context = new SpContext())
            {
                List<SpDetalleMoneda> data = context.SpDetalleMoneda
                    .FromSqlRaw("EXEC [dbo].[DetalleMoneda]").ToList();
                return data;
            }
        }

        public List<SpDetalleFormaPago> DetalleFormaPago(string tipoMoneda, int idTurno)
        {
            using (var context = new SpContext())
            {
                var paramCodMoneda = new SqlParameter("@cod_moneda", tipoMoneda);
                var paramCodTurno = new SqlParameter("@idTurno", idTurno);
                List<SpDetalleFormaPago> data = context.SpDetalleFormaPago
                        .FromSqlRaw("EXEC [dbo].[DetalleFormaPago] @cod_moneda=@cod_moneda, @Turno=@idTurno", paramCodMoneda, paramCodTurno).ToList();

                return data;
            }
        }

        public List<SpFacturadoXCajaXFecha> FacturadoXCajaXFecha(string fechaActual)
        {
            using (var context = new SpContext())
            {
                var paramFecha = new SqlParameter("@fecha", fechaActual);
                List<SpFacturadoXCajaXFecha> data = context.SpFacturadoXCajaXFecha
                    .FromSqlRaw("EXEC [dbo].[FacturadoXCajaXFecha] @fecha=@fecha", paramFecha).ToList();

                return data;
            }
        }

        public List<SpFacturadoXTurnoXFecha> FacturadoXTurnoXFecha(string fechaActual, string caja)
        {
            using (var context = new SpContext())
            {
                caja = caja != null ? caja : "0";
                var paramFecha = new SqlParameter("@fecha", fechaActual);
                var paramCaja = new SqlParameter("@CodCaja", caja);
                List<SpFacturadoXTurnoXFecha> data = context.SpFacturadoXTurnoXFecha
                    .FromSqlRaw("EXEC [dbo].[FacturadoXTurnoXFecha] @fecha=@fecha, @caja=@CodCaja", paramFecha, paramCaja).ToList();

                return data;
            }
        }

        public List<SpListadoFactura> ListadoFactura(ParamsLfViewModel filtroLF)
        {
            using (var context = new SpContext())
            {
                filtroLF.Caja = filtroLF.Caja != null ? filtroLF.Caja : "0";
                //filtroLF.Tasa = filtroLF.Tasa != null ? filtroLF.Tasa : 0;
                filtroLF.Tienda = filtroLF.Tienda != null ? filtroLF.Tienda : "";

                var paramTurno = new SqlParameter("@Turno", filtroLF.Turno);
                var paramCaja = new SqlParameter("@Caja", filtroLF.Caja);
                var paramTasa = new SqlParameter("@Tasa", filtroLF.Tasa);
                var paramFecha = new SqlParameter("@Fecha", filtroLF.Fecha);
                var paramTienda = new SqlParameter("@Tienda", filtroLF.Tienda);

                List<SpListadoFactura> data = context.SpListadoFactura
                    .FromSqlRaw("EXEC [dbo].[ListadoFactura] @Turno=@Turno, @Caja=@Caja, @Tasa=@Tasa, @Fecha=@Fecha, @Tienda=@Tienda", paramTurno, paramCaja, paramTasa, paramFecha, paramTienda).ToList();

                return data;
            }
        }

        public List<SpListadoDetalleFactura> DetalleFactura(DetalleFacturaviewModel filtroLF)
        {
            try
            {
                using (var context = new SpContext())
                {
                    filtroLF.Factura = filtroLF.Factura != null ? filtroLF.Factura : "";//falta el nro de caja o id caja

                    var paramFactura = new SqlParameter("@Factura", filtroLF.Factura);
                    var paramTasa = new SqlParameter("@Tasa", filtroLF.Tasa);
                    var paramFecha = new SqlParameter("@Fecha", filtroLF.Fecha);
                    var paramCajaId = new SqlParameter("@IdCaja", filtroLF.CajaId);
                    // @Factura=@Factura,, @Fecha=@Fecha, @Tasa=@Tasa,@Status=@Estatus , paramFecha, paramTasa, paramEstatus paramFactura,

                    List<SpListadoDetalleFactura> data = context.SpListadoDetalleFactura
                        .FromSqlRaw("EXEC [dbo].[ListadoDetalleFactura] @Factura=@Factura, @Fecha=@Fecha, @IdCaja=@IdCaja", paramFactura, paramFecha, paramCajaId).ToList();

                    return data;
                }
            }
            catch (Exception e)
            {
                var m = e.Message.ToString();
                return null;
            }
        }

        public int CrudRol(RolViewModel crudAccion, int AccionCrud = 1)
        {
            try
            {
                using (var context = new SpContext())
                {
                    var paramAccion = new SqlParameter("@Accion", AccionCrud);
                    var paramDescrip = new SqlParameter("@Descripcion", crudAccion.Descripcion);
                    var paramActivo = new SqlParameter("@Activo", crudAccion.Activo == true ? 1 : 0);
                    var paramID = new SqlParameter("@ID", crudAccion.Id);

                    context.SpCrudAccion
                        .FromSqlRaw("EXEC [dbo].[ActualizarRol]  @Accion=@Accion, @Descripcion=@Descripcion, @Activo=@Activo, @ID=@ID", paramAccion, paramDescrip, paramActivo, paramID);

                    return 1;
                }
            }
            catch
            {
                return 0;
            }
        }

        public int SpAddArqueo(ArqueoViewModel arqueo, string XmlDetalleArqueo)
        {
            try
            {
                using (var context = new SpContext())
                {
                    var texto = new SqlParameter("@arqueo", XmlDetalleArqueo);
                    var paramIdUser = new SqlParameter("@UsuarioId", arqueo.UsuarioId);
                    var datastatus = context.Database.ExecuteSqlRaw("EXEC [dbo].[GuardarArqueo] @arqueo, @UsuarioId", texto, paramIdUser);

                    return 1;
                }
            }
            catch (Exception e)
            {
                var m = e.Message.ToString();
                return 0;
            }
        }

        public List<SpMostrarCierresToC> MostrarCierreTurno(int idTurno)
        {
            try
            {
                using (var context = new SpContext())
                {

                    var paramIdTurno = new SqlParameter("@Idturno", idTurno);

                    List<SpMostrarCierresToC> data = context.SpCierreTurno
                        .FromSqlRaw("EXEC [dbo].[MostrarCierreTurno] @Idturno=@Idturno", paramIdTurno).ToList();

                    return data;
                }
            }
            catch (Exception ex)
            {
                var m = ex.Message;
                return null;
            }
        }

        public List<SpMostrarCierresToC> MostrarCierreCaja(string codCaja, string fecha)
        {
            using (var context = new SpContext())
            {
                codCaja = codCaja != "0" ? codCaja : "";
                var paramCodCaja = new SqlParameter("@caja", codCaja);
                var paramFecha = new SqlParameter("@fecha", fecha);

                List<SpMostrarCierresToC> data = context.SpCierreCaja
                    .FromSqlRaw("EXEC [dbo].[MostrarCierreCaja] @caja=@caja, @fecha=@fecha", paramCodCaja, paramFecha).ToList();

                return data;
            }
        }

        public List<SpDeclaradoProcesado> DeclaradoyProcesado(string codCaja, string fecha)
        {
            using (var context = new SpContext())
            {
                codCaja = codCaja != null ? codCaja == "0" ? "" : codCaja : "";
                var paramCodCaja = new SqlParameter("@caja", codCaja);
                var paramFecha = new SqlParameter("@fecha", fecha);

                List<SpDeclaradoProcesado> data = context.SpDeclaradoProcesado
                    .FromSqlRaw("EXEC [dbo].[DeclaradoyProcesado] @caja=@caja, @fecha=@fecha", paramCodCaja, paramFecha).ToList();

                return data;
            }
        }

        public int VerificaTurno(int idTurno, int idUser)
        {
            try
            {
                using (var context = new SpContext())
                {
                    var paramIdTurno = new SqlParameter("@TurnoId", idTurno);
                    var paramIdUser = new SqlParameter("@UsuarioId", idUser);
                    context.Database.ExecuteSqlRaw("EXEC [dbo].[VerificarTurno] @TurnoId, @UsuarioId", paramIdTurno, paramIdUser);

                    return 1;
                }
            }
            catch (Exception e)
            {
                var m = e.Message.ToString();
                return 0;
            }
        }

        public int ActualizarFactura(int idFactura, int idUser, int Estatus)
        {
            try
            {
                if (Estatus != 1 && Estatus != 2) { return 0; }
                using (var context = new SpContext())
                {
                    var paramIdFactura = new SqlParameter("@IdFactura", idFactura);
                    var paramIdUser = new SqlParameter("@UsuarioId", idUser);
                    var paramEstatus = new SqlParameter("@Estatus", Estatus);
                    context.Database.ExecuteSqlRaw("EXEC [dbo].[ActualizarEstatusFactura] @IdFactura, @UsuarioId, @Estatus", paramIdFactura, paramIdUser, paramEstatus);

                    return 1;
                }
            }
            catch (Exception e)
            {
                var m = e.Message.ToString();
                return 0;
            }
        }

        public int ResetArqueo(int idTurno, int idUser)
        {
            try
            {
                using (var context = new SpContext())
                {
                    var paramIdTurno = new SqlParameter("@IdTurno", idTurno);
                    var paramIdUser = new SqlParameter("@UsuarioId", idUser);
                    context.Database.ExecuteSqlRaw("EXEC [dbo].[ResetArqueo] @IdTurno, @UsuarioId", paramIdTurno, paramIdUser);

                    return 1;
                }
            }
            catch (Exception e)
            {
                var m = e.Message.ToString();
                return 0;
            }
        }

        public List<SpMostrarCierresTurnoS4> ReporteCierreTurnosS4(string fecha)
        {
            using (var context = new SpContext())
            {
                var paramFecha = new SqlParameter("@fecha", fecha);

                List<SpMostrarCierresTurnoS4> data = context.SpCierreTurnoS4
                    .FromSqlRaw("EXEC [dbo].[MostrarCierreTurnoS4] @fecha=@fecha", paramFecha).ToList();

                return data;
            }
        }

        public List<SpReporteCierresToC> ReporteCierreCaja(int IdCaja, string fecha, int IdTurno = 0)//IdCaja==0: todo el dia | paramIdTurno == 0: reporte por caja dif de 0 solo el turno
        {
            try
            {
                using (var context = new SpContext())
                {
                    var paramIdCaja = new SqlParameter("@caja", IdCaja);
                    var paramIdTurno = new SqlParameter("@turno", IdTurno);
                    var paramFecha = new SqlParameter("@fecha", fecha);

                    List<SpReporteCierresToC> data = context.SpCierreCajaPos
                        .FromSqlRaw("EXEC [dbo].[DetalleFormaPagoCaja] @caja=@caja, @fecha=@fecha, @turno=@turno", paramIdCaja, paramFecha, paramIdTurno).ToList();

                    return data;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<SpDescloseDolar> DesgloseDolarArqueo(int ArqueoId)
        {
            using (var context = new SpContext())
            {
                var paramIdArqueo = new SqlParameter("@IdArqueo", ArqueoId);

                List<SpDescloseDolar> data = context.SpDesgloseDolarArqueo
                    .FromSqlRaw("EXEC [dbo].[DesgloseDolarArqueo] @IdArqueo=@IdArqueo", paramIdArqueo).ToList();

                return data;
            }
        }

        public List<SpFaltanteSobrante> ListadoSobrantesFaltantes(int IdTurno, int IdCaja, int IdUsuario, string fechaI, string fechaF)
        {
            try
            {
                using (var context = new SpContext())
                {
                    var paramIdTurno = new SqlParameter("@Idturno", IdTurno);
                    var paramIdCaja = new SqlParameter("@Idcaja", IdCaja);
                    var paramIdUsuario = new SqlParameter("@Idusuario", IdUsuario);
                    var paramFecha = new SqlParameter("@fecha", fechaI);
                    //var paramFechaF = new SqlParameter("@fechafin", fechaF);

                    List<SpFaltanteSobrante> data = context.SpFaltanteSobrante
                        .FromSqlRaw("EXEC [dbo].[FaltanteSobrante] @Idturno=@Idturno, @Idcaja=@Idcaja, @Idusuario=@Idusuario, @fecha=@fecha", paramIdTurno, paramIdCaja, paramIdUsuario, paramFecha).ToList();

                    return data;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<SpListadoFacturaRangFech> ListadoFacturaRangFech(string NroControl, string NroFactura, string ZetaSerial, string fechaI, string fechaF)
        {
            try
            {
                using (var context = new SpContext())
                {
                    var paramNroControl = new SqlParameter("@ncontrol", NroControl != null ? NroControl.PadLeft(9, '0') : "");
                    var paramNroFactura = new SqlParameter("@nfactura", NroFactura != null ? NroFactura.PadLeft(8, '0') : "");
                    var paramSetaSerial = new SqlParameter("@zserial", ZetaSerial != null ? ZetaSerial : "");
                    var paramFechaI = new SqlParameter("@fechaini", fechaI);
                    var paramFechaF = new SqlParameter("@fechafin", fechaF);

                    List<SpListadoFacturaRangFech> data = context.ListadoFacturaRangFech
                        .FromSqlRaw("EXEC [dbo].[ListadoFacturas] @ncontrol=@ncontrol, @nfactura=@nfactura, @zserial=@zserial, @fechaini=@fechaini, @fechafin=@fechafin", paramNroControl, paramNroFactura, paramSetaSerial, paramFechaI, paramFechaF).ToList();

                    return data;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<SpReporteFormasPago> ReporteFormasPagoRangFech(int IdTienda, int IdCaja, int Idturno, string fechaI, string fechaF)
        {
            try
            {
                using (var context = new SpContext())
                {
                    var paramIdTienda = new SqlParameter("@IdTienda", IdTienda);
                    var paramIdCaja = new SqlParameter("@IdCaja", IdCaja);
                    var paramIdTurno = new SqlParameter("@IdTurno", Idturno);
                    var paramFechaI = new SqlParameter("@fechaini", fechaI);
                    var paramFechaF = new SqlParameter("@fechafin", fechaF);

                    List<SpReporteFormasPago> data = context.ReporteFormaPagoRangFech
                        .FromSqlRaw("EXEC [dbo].[ReporteFormasPago] @IdTienda=@IdTienda, @IdCaja=@IdCaja, @IdTurno=@IdTurno, @fechaini=@fechaini, @fechafin=@fechafin", paramIdTienda, paramIdCaja, paramIdTurno, paramFechaI, paramFechaF).ToList();

                    return data;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<SpFormasPagoFacturaId> FormasPagoFacturaId(int IdFactura)
        {
            try
            {
                using (var context = new SpContext())
                {
                    var paramIdFactura = new SqlParameter("@idfactura", IdFactura);

                    List<SpFormasPagoFacturaId> data = context.FormaPagoFacturaId
                        .FromSqlRaw("EXEC [dbo].[FormaPagoXFacturaId] @idfactura=@idfactura", paramIdFactura).ToList();

                    return data;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<SpDevolucionByTurnoId> BuscarDevoluciones(int TurnoId)
        {
            try
            {
                using (var context = new SpContext())
                {
                    var paramIdTurno = new SqlParameter("@turno", TurnoId);

                    List<SpDevolucionByTurnoId> data = context.BuscarDevoluciones
                        .FromSqlRaw("EXEC [dbo].[BuscarDevolucion] @turno=@turno", paramIdTurno).ToList();

                    return data;
                }
            }
            catch (Exception ex)
            {
                string m = ex.Message.ToString();
                return null;
            }
        }

        public List<SpResumenxCajaxFecha> ResumenxCajaxFechas(string fecha, string CodCaja = "0")
        {
            try
            {
                using (var context = new SpContext())
                {
                    var paramFecha = new SqlParameter("@fecha", fecha);
                    var paramCodCaja = new SqlParameter("@caja", CodCaja);

                    List<SpResumenxCajaxFecha> data = context.ResumenxCajaxFechas
                        .FromSqlRaw("EXEC [dbo].[ResumenXCajaXFecha] @fecha=@fecha, @caja=@caja", paramFecha, paramCodCaja).ToList();

                    return data;
                }
            }
            catch (Exception ex)
            {
                string m = ex.Message.ToString();
                return null;
            }
        }

        public List<SpListadoDevoluciones> ListadoDevoluciones(DateTime Fecha, string NumDevolucion="0", string NumFactura="0", int IdFactura=0)
        {
            try
            {
                using (var context = new SpContext())
                {
                    var paramNumDevolucion = new SqlParameter("@NumeroDevolucion", (NumDevolucion!=null)?NumDevolucion:"0");
                    var paramNumFactura = new SqlParameter("@NumeroFactura", NumFactura);
                    var paramIdFactura = new SqlParameter("@IdFactura", IdFactura);
                    var paramFecha = new SqlParameter("@Fecha", Fecha);

                    List<SpListadoDevoluciones> data = context.ListadoDevoluciones
                        .FromSqlRaw("EXEC [dbo].[ListadoDevolucion] @Fecha=@Fecha, @IdFactura=@IdFactura, @NumeroFactura=@NumeroFactura, @NumeroDevolucion=@NumeroDevolucion", paramFecha, paramIdFactura, paramNumFactura, paramNumDevolucion).ToList();

                    return data;
                }
            }
            catch(Exception ex)
            {
                var error = ex.Message.ToString();
                return null;
            }
        }

        public List<SpListadoWallet> ListadoWallets(DateTime FechaIni, DateTime FechaFin, string Rif = "0")
        {
            try
            {

                using (var context = new SpContext())
                {
                    var paramRIF = new SqlParameter("@RIF", (Rif != null) ? Rif : "0");
                    var paramFechaIni = new SqlParameter("@FechaIni", FechaIni);
                    var paramFechaFin = new SqlParameter("@FechaFin", FechaFin);

                    List<SpListadoWallet> data = context.ListadoWallets
                        .FromSqlRaw("EXEC [dbo].[ListadoWallet] @FechaIni=@FechaIni, @FechaFin=@FechaFin, @RIF=@RIF", paramFechaIni, paramFechaFin, paramRIF).ToList();

                    return data;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message.ToString();
                return null;
            }
        }

        public List<SpMostrarConsolidado> MostrarConsolidado(string fechaIni, string fechaFin)
        {
            try
            {
                using (var context = new SpContext())
                {
                    var paramFechaIni = new SqlParameter("@fechaini", fechaIni);

                    var paramFechaFin = new SqlParameter("@fechafin", fechaFin);

                    List<SpMostrarConsolidado> data = context.MostrarConsolidado
                        .FromSqlRaw("EXEC [dbo].[MostrarConsolidado] @fechaini=@fechaini, @fechafin=@fechafin", paramFechaIni, paramFechaFin).ToList();

                    return data;
                }
            }catch(Exception e)
            {
                var m = e.Message.ToString();
                return null;
            }
        }

        public List<SpMostrarQQM> GetQQM(int IdQQM)
        {
            try
            {
                using (var context = new SpContext())
                {
                    var paramIdQQM = new SqlParameter("@Id", IdQQM);

                    List<SpMostrarQQM> data = context.MostrarQQMs
                        .FromSqlRaw("EXEC [QQM].[ListadoPreguntas] @Id=@Id", paramIdQQM).ToList();

                    return data;
                }
            }
            catch (Exception e)
            {
                var m = e.Message.ToString();
                return null;
            }
        }

        public int SaveQuestionsQQM(int Id, SpMostrarQQM mostrarQQM)
        {
            try
            {
                using (var context = new SpContext())
                {
                    var paramV0 = new SqlParameter("@Id", Id);
                    var paramV2 = new SqlParameter("@Json", mostrarQQM.PreguntaRespuesta);
                    context.Database.ExecuteSqlRaw("EXEC [QQM].[ActualizarPreguntas] @Id, @Json", paramV0, paramV2);

                    return 1;
                }
            }
            catch (Exception e)
            {
                var m = e.Message.ToString();
                return 0;
            }
        }

        public int SaveGiftsQQM(int Id, SpMostrarQQM mostrarQQM)
        {
            try
            {
                using (var context = new SpContext())
                {
                    var paramV0 = new SqlParameter("@Id", Id);
                    var paramV2 = new SqlParameter("@Descripcion", mostrarQQM.Premio);
                    context.Database.ExecuteSqlRaw("EXEC [QQM].[ActualizarPremios] @Id, @Descripcion", paramV0, paramV2);

                    return 1;
                }
            }
            catch (Exception e)
            {
                var m = e.Message.ToString();
                return 0;
            }
        }

        public List<SpListadoConstante> GetConstantes(int Id=0)
        {
            try
            {
                using (var context = new SpContext())
                {
                    var paramId = new SqlParameter("@Id", Id);

                    List<SpListadoConstante> data = context.MostrarConstantes
                        .FromSqlRaw("EXEC [QQM].[ListadoConstante] @Id=@Id", paramId).ToList();

                    return data;
                }
            }
            catch (Exception e)
            {
                var m = e.Message.ToString();
                return null;
            }
        }

        public int SetConstantes(int Id, int Valor)
        {
            try
            {
                using (var context = new SpContext())
                {
                    var paramV0 = new SqlParameter("@Id", Id);
                    var paramV2 = new SqlParameter("@Valor", Valor);
                    context.Database.ExecuteSqlRaw("EXEC [QQM].[ActualizarConstante] @Id, @Valor", paramV0, paramV2);

                    return 1;
                }
            }
            catch (Exception e)
            {
                var m = e.Message.ToString();
                return 0;
            }
        }

        public SpGame SetGame(int Id = 0, int NivelId=0, string Pregunta="")
        {
            try
            {
                using (var context = new SpContext())
                {
                    var paramId = new SqlParameter("@Id", Id);
                    var paramNivelID = new SqlParameter("@NivelID", NivelId);
                    var paramPregunta = new SqlParameter("@Pregunta", Pregunta);

                     var data = context.SetGame
                        .FromSqlRaw("EXEC [QQM].[ActualizarSorteo] @Id=@Id, @NivelID=@NivelID, @Pregunta=@Pregunta", paramId, paramNivelID, paramPregunta).ToList();

                    return data[0];
                }
            }
            catch (Exception e)
            {
                var m = e.Message.ToString();
                return null;
            }
        }

        public List<SpListadoProveedor> GetProveedores(string Id = "", string Nombre="")
        {
            try
            {
                using (var context = new SpContext())
                {
                    var paramId = new SqlParameter("@rif", (Id==null)?"":Id);
                    var paramNombre = new SqlParameter("@nombre", (Nombre==null)?"":Nombre);

                    List<SpListadoProveedor> data = context.ListadoProveedor
                        .FromSqlRaw("EXEC [dbo].[ListadoProveedor] @rif=@rif, @nombre=@nombre", paramId, paramNombre).ToList();
                    return data;
                }
            }
            catch (Exception e)
            {
                var m = e.Message.ToString();
                return null;
            }
        }

        public List<SpPagoProforma> GetPagos(FiltroPagoViewModel pagoProformaViewModel)
        {
            try
            {
                using (var context = new SpContext())
                {
                    var paramProformaId = new SqlParameter("@ProformaId", pagoProformaViewModel.ProformaId);
                    List<SpPagoProforma> data = context.ListadoPagoProforma.FromSqlRaw("EXEC [dbo].[ListadoPagoforma] @ProformaId=@ProformaId", /*paramFechaInicio, paramFechaFin,*/ paramProformaId).ToList();
                    return data;
                }
            }
            catch (Exception e)
            {
                var m = e.Message.ToString();
                return null;
            }
        }

        public int SaveUpdatePagoProforma(PagoViewModel pagoProforma)
        {
            try
            {
                using (var context = new SpContext())
                {
                    var prmId = new SqlParameter("@Id", pagoProforma.Id);
                    var prmProformaId = new SqlParameter("@ProformaId", pagoProforma.ProformaId);
                    var prmMontoPago = new SqlParameter("@MontoPago", pagoProforma.MontoPago);
                    var prmFecha = new SqlParameter("@FechaPago", pagoProforma.FechaPago);
                    var prmObservacion = new SqlParameter("@Observacion", pagoProforma.Observacion);
                    var prmEstatus = new SqlParameter("@Estatus", pagoProforma.Estatus);
                    var prmFormaPagoId = new SqlParameter("@FormaPagoId", pagoProforma.FormaPagoId);
                    var prmAcreedorId = new SqlParameter("@AcreedorId", pagoProforma.AcreedorId);                     
                    var prmUserId = new SqlParameter("@UserId", pagoProforma.UserId);

                    context.Database.ExecuteSqlRaw("EXEC [dbo].[InsertarActualizarPagosProforma] @Id=@Id, @ProformaId=@ProformaId, @MontoPago=@MontoPago, @FechaPago=@FechaPago, @Observacion=@Observacion, @Estatus=@Estatus ,@FormaPagoId=@FormaPagoId, @AcreedorId=@AcreedorId, @UserId=@UserId", prmId, prmProformaId, prmMontoPago, prmFecha, prmObservacion, prmEstatus,prmFormaPagoId, prmAcreedorId, prmUserId);

                    return 1;
                }
            }
            catch (Exception e)
            {
                var m = e.Message.ToString();
                return 0;
            }
        }

        public List<SpProformaViewModel> GetProformas(FiltroProformaViewModel proformaViewModel)
        {
            try
            {
                using (var context = new SpContext())
                {
                    var paramFechaInicio = new SqlParameter("@FechaInicio", proformaViewModel.FechaIni);
                    var paramFechaFin = new SqlParameter("@FechaFin", proformaViewModel.FechaFin);
                    var paramNumeroProforma = new SqlParameter("@NumeroProforma", proformaViewModel.NumeroProforma==null?"": proformaViewModel.NumeroProforma);
                    var paramCodigoProveedor = new SqlParameter("@CodigoProveedor", proformaViewModel.CodigoProveedor=="-1"?"": proformaViewModel.CodigoProveedor);

                    List<SpProformaViewModel> data = context.ListadoProforma.FromSqlRaw("EXEC [dbo].[ListadoProforma] @FechaInicio=@FechaInicio, @FechaFin=@FechaFin, @NumeroProforma=@NumeroProforma, @CodigoProveedor=@CodigoProveedor", paramFechaInicio, paramFechaFin, paramNumeroProforma, paramCodigoProveedor).ToList();
                    return data;
                }
            }
            catch (Exception e)
            {
                var m = e.Message.ToString();
                return null;
            }
        }

        public List<SpListadoFormaPago> GetFormaPago(string Descripcion)
        {
            try
            {
                using (var context = new SpContext())
                {
                    var paramDescripcion = new SqlParameter("@Descripcion", Descripcion == null ? "" : Descripcion);

                    List<SpListadoFormaPago> data = context.SpListadoFormaPago.FromSqlRaw("EXEC [dbo].[ListadoFormaPago] @Descripcion=@Descripcion", paramDescripcion).ToList();
                    return data;
                }
            }
            catch (Exception e)
            {
                var m = e.Message.ToString();
                return null;
            }
        }

        public List<SpListadoAcreedor> GetAcreedor(string Descripcion)
        {
            try
            {
                using (var context = new SpContext())
                {
                    var paramDescripcion = new SqlParameter("@Descripcion", Descripcion == null ? "" : Descripcion);

                    List<SpListadoAcreedor> data = context.SpListadoAcreedor.FromSqlRaw("EXEC [dbo].[ListadoAcreedor] @Descripcion=@Descripcion", paramDescripcion).ToList();
                    return data;
                }
            }
            catch (Exception e)
            {
                var m = e.Message.ToString();
                return null;
            }
        }

        public int SaveUpdateAcreedor(AcreedorViewModel acreedor)
        {
            try
            { /*falta agragar aqui datos*/
                using (var context = new SpContext())
                {
                    var prmId = new SqlParameter("@Id", acreedor.Id);
                    var prmDescripcion = new SqlParameter("@Descripcion", acreedor.Descripcion.ToUpper());
                    var prmEstatus = new SqlParameter("@Estatus", acreedor.Estatus);

                    List<SpSaveAcreedor> data = new List<SpSaveAcreedor>();
                    data = context.SpSaveAcreedors.FromSqlRaw("EXEC [dbo].[InsertarActualizarAcreedor] @Id=@Id, @Descripcion=@Descripcion, @Estatus=@Estatus", prmId, prmDescripcion, prmEstatus).ToList();
                    return (int)data[0].IdAcreedor;
                }
            }
            catch (Exception e)
            {
                var m = e.Message.ToString();
                return 0;
            }
        }

        public int SaveUpdateProforma(ProformaViewModel proforma)
        {
            try
            { /*falta agragar aqui datos*/
                using (var context = new SpContext())
                {
                    var prmId = new SqlParameter("@Id", proforma.Id);
                    var prmCodProveedor = new SqlParameter("@CodigoProveedor", proforma.CodigoProveedor);
                    var prmCodProforma = new SqlParameter("@NumeroProforma", proforma.NumeroProforma.ToUpper());
                    var prmDescripcion = new SqlParameter("@Descripcion", proforma.Descripcion.ToUpper());
                    var prmCantContainer = new SqlParameter("@CantidadContainer", proforma.CantidadContainer);                    
                    var prmMonto = new SqlParameter("@Monto", proforma.Monto);
                    var prmFechaProforma = new SqlParameter("@FechaProforma", proforma.FechaProforma);
                    var prmEstatus = new SqlParameter("@Estatus", proforma.Estatus);
                    var prmMonedaId = new SqlParameter("@MonedaId", proforma.MonedaId);
                    var prmUserId = new SqlParameter("@UserId", proforma.UserId);
                    var prmFechaEmbarque = new SqlParameter("@FechaEmbarque", proforma.FechaEmbarque);
                    var prmFechaLlegada = new SqlParameter("@FechaLlegada", proforma.FechaLlegada);

                    var prmDescripcionProduccion = new SqlParameter("@DescripcionProduccion", (proforma.DescripcionProduccion==null)?"": proforma.DescripcionProduccion);
                    var prmFechaInicio = new SqlParameter("@FechaInicio", proforma.FechaInicio);
                    var prmFechaFin = new SqlParameter("@FechaFin", proforma.FechaFin);

                    List<SpProfomaSave> data = new List<SpProfomaSave>();
                    /*if (proforma.Id == 0)
                    {*/
                        data = context.SpSaveProforma.FromSqlRaw("EXEC [dbo].[InsertarActualizarProforma] @Id=@Id, @CodigoProveedor=@CodigoProveedor, @NumeroProforma=@NumeroProforma, @Descripcion=@Descripcion, @CantidadContainer=@CantidadContainer, @Monto=@Monto, @FechaProforma=@FechaProforma, @Estatus=@Estatus, @MonedaId=@MonedaId, @UserId=@UserId, @FechaEmbarque=@FechaEmbarque, @FechaLlegada=@FechaLlegada, @DescripcionProduccion=@DescripcionProduccion, @FechaInicio=@FechaInicio, @FechaFin=@FechaFin", prmId, prmCodProveedor, prmCodProforma, prmDescripcion, prmCantContainer, prmMonto, prmFechaProforma, prmEstatus, prmMonedaId, prmUserId, prmFechaEmbarque, prmFechaLlegada, prmDescripcionProduccion, prmFechaInicio, prmFechaFin).ToList();
                        return (int)data[0].IdProforma;
                   /* }
                    else
                    {
                        //context.Database.ExecuteSqlRaw("EXEC [dbo].[InsertarActualizarProforma] @Id=@Id, @CodigoProveedor=@CodigoProveedor, @NumeroProforma=@NumeroProforma, @Descripcion=@Descripcion, @CantidadContainer=@CantidadContainer, @Monto=@Monto, @FechaProforma=@FechaProforma, @Estatus=@Estatus, @FechaEmbarque=@FechaEmbarque, @FechaLlegada=@FechaLlegada", prmId, prmCodProveedor, prmCodProforma, prmDescripcion, prmCantContainer, prmMonto, prmFechaProforma, prmEstatus, prmFechaEmbarque, prmFechaLlegada);
                        //return 1;
                    }*/
                }
            }
            catch (Exception e)
            {
                var m = e.Message.ToString();
                return 0;
            }
        }

        public int DeleteProforma(int Id, int Estatus, int All=0)
        {
            try
            {
                using (var context = new SpContext())
                {
                    var prmId = new SqlParameter("@Id", Id);
                    var prmEstatus = new SqlParameter("@Estatus", Estatus);
                    var prmAll = new SqlParameter("@All", All);
                    context.Database.ExecuteSqlRaw("EXEC [dbo].[AnularProforma] @Id, @Estatus, @All", prmId, prmEstatus, prmAll);

                    return 1;
                }
            }
            catch (Exception e)
            {
                var m = e.Message.ToString();
                return 0;
            }
        }

        public List<SpListadoFactProfroma> GetFactura(int ProformaId)
        {
            try
            {
                using (var context = new SpContext())
                {
                    var paramProformaId = new SqlParameter("@ProformaId", ProformaId);

                    List<SpListadoFactProfroma> data = context.SpListadoFactProfromas.FromSqlRaw("EXEC [dbo].[ListadoFactura] @ProformaId=@ProformaId", paramProformaId).ToList();
                    return data;
                }
            }
            catch (Exception e)
            {
                var m = e.Message.ToString();
                return null;
            }
        }

        public int SaveUpdateFactura(FacturaProformaViewModel factProforma)
        {
            try
            {
                using (var context = new SpContext())
                {
                    var prmId = new SqlParameter("@Id", factProforma.Id);
                    var prmProformaId = new SqlParameter("@ProformaId", factProforma.ProformaId);
                    var prmNumeroFactura = new SqlParameter("@NumeroFactura", factProforma.NumeroFactura);
                    var prmMonto = new SqlParameter("@Monto", factProforma.Monto);
                    var prmFechaFactura = new SqlParameter("@FechaFactura", factProforma.FechaFactura);
                    var prmEstatus = new SqlParameter("@Estatus", factProforma.Estatus);

                    List<SpSaveFacturaProforma> data = new List<SpSaveFacturaProforma>();
                    data = context.SpSaveFacturaProformas.FromSqlRaw("EXEC [dbo].[InsertarActualizarFactura] @Id=@Id, @ProformaId=@ProformaId, @NumeroFactura=@NumeroFactura, @Monto=@Monto, @FechaFactura=@FechaFactura, @Estatus=@Estatus", prmId, prmProformaId, prmNumeroFactura, prmMonto, prmFechaFactura, prmEstatus).ToList();

                    return (int)data[0].IdFactura;
                }
            }
            catch (Exception e)
            {
                var m = e.Message.ToString();
                return 0;
            }
        }

        public List<SpDesglosePuntos> GetDesglosePuntos(string Fecha, int TurnoId, int CajaId)
        {
            try
            {
                using (var context = new SpContext())
                {
                    var paramFecha = new SqlParameter("@Fecha", Fecha);
                    var paramTurnoId = new SqlParameter("@TurnoId", TurnoId);
                    var paramCajaId = new SqlParameter("@CajaId", CajaId);

                    List<SpDesglosePuntos> data = context.SpDesglosePunto.FromSqlRaw("EXEC [dbo].[DesglosePuntos] @Fecha=@Fecha, @TurnoId=@TurnoId, @CajaId=@CajaId", paramFecha, paramTurnoId, paramCajaId).ToList();
                    return data;
                }
            }
            catch (Exception e)
            {
                var m = e.Message.ToString();
                return null;
            }
        }

        public List<SpDescloseDolar> GetDesgloseEfectivoDE(string Fecha, int TurnoId)
        {
            try
            {
                using (var context = new SpContext())
                {
                    var paramFecha = new SqlParameter("@Fecha", Fecha);
                    var paramTurnoId = new SqlParameter("@TurnoId", TurnoId);

                    List<SpDescloseDolar> data = context.SpDesgloseDolarArqueo.FromSqlRaw("EXEC [dbo].[DesgloseDolarCantidad] @Fecha=@Fecha, @TurnoId=@TurnoId", paramFecha, paramTurnoId).ToList();
                    return data;
                }
            }
            catch (Exception e)
            {
                var m = e.Message.ToString();
                return null;
            }
        }

        public List<SpMostrarFacturaPromedio> MostrarFacturaPromedios(string fechaIni)
        {
            try
            {
                using (var context = new SpContext())
                {
                    var prmFecha = new SqlParameter("@fecha", fechaIni); 
                    List<SpMostrarFacturaPromedio> data = context.MostrarFacturaPromedio.FromSqlRaw("EXEC [dbo].[MostrarFacturaPromedio] @fecha=@fecha", prmFecha).ToList();
                    return data;
                }
            }
            catch (Exception e)
            {
                var m = e.Message.ToString();
                return null;
            }
        }

        public List<SpListaResumenVentaXFormaPago> MostrarFormasPagoInside(string fechaIni, string fechaFin, int tiendaId)
        {
            try
            {
                using (var context = new SpContext())
                {
                    var prmFechaIni = new SqlParameter("@FechaIni", fechaIni);
                    var prmFechaFin = new SqlParameter("@FechaFin", fechaFin);
                    var prmTiendaId = new SqlParameter("@TiendaId", tiendaId);
                    List<SpListaResumenVentaXFormaPago> data = context.FormaPagoInside.FromSqlRaw("EXEC [dbo].[ListadoResumenVentaxFormaPago] @FechaIni=@FechaIni, @FechaFin=@FechaFin, @TiendaId=@TiendaId", prmFechaIni, prmFechaFin, prmTiendaId).ToList();
                    return data;
                }
            }
            catch (Exception e)
            {
                var m = e.Message.ToString();
                return null;
            }
        }

        public List<SpMostrarVentasXDep> MostrarVentasXDep(string fechaIni, string fechaFin)
        {/*directo en tienda*/
            try
            {
                using (var context = new SpContext())
                {
                    var prmFechaInicio = new SqlParameter("@fecha1", fechaIni);
                    var prmFechaFin = new SqlParameter("@fecha2", fechaFin);
                    List<SpMostrarVentasXDep> data = context.MostrarVentasXDep.FromSqlRaw("EXEC [dbo].[MostrarVentaXDepartamento] @fecha1=@fecha1, @fecha2=@fecha2", prmFechaInicio, prmFechaFin).ToList();
                    return data;
                }
            }
            catch (Exception e)
            {
                var m = e.Message.ToString();
                return null;
            }
        }

        public List<SpListadoResumenVenta> ListadoResumenVentaInside(int tiendaId, string fechaIni, string fechaFin = "")
        {
            try
            {
                using (var context = new SpContext())
                {
                    var prmFechaIni = new SqlParameter("@FechaIni", fechaIni);
                    var prmFechaFin = new SqlParameter("@FechaFin", fechaFin);
                    var prmTiendaId = new SqlParameter("@TiendaId", tiendaId);
                    List<SpListadoResumenVenta> data = context.listadoResumenVentasGeneral.FromSqlRaw("EXEC [dbo].[ListadoResumenVenta] @FechaIni=@FechaIni, @FechaFin=@FechaFin, @TiendaId=@TiendaId", prmFechaIni, prmFechaFin, prmTiendaId).ToList();
                    return data;
                }
            }
            catch (Exception e)
            {
                var m = e.Message.ToString();
                return null;
            }
        }

        public List<SpListadoResumenVentaDepartamento> ListadoResumenVentaDepartamentoInside(int tiendaId, string fechaIni, string fechaFin)
        {
            try
            {
                using (var context = new SpContext())
                {
                    var prmFechaInicio = new SqlParameter("@Fechadesde", fechaIni);
                    var prmFechaFin = new SqlParameter("@Fechahasta", fechaFin);
                    var prmTiendaId = new SqlParameter("@TiendaId", tiendaId);
                    List<SpListadoResumenVentaDepartamento> data = context.ListadoResumenVentaDepartamentos.FromSqlRaw("EXEC [dbo].[ListadoResumenVentaxDepartamento] @Fechadesde=@Fechadesde, @Fechahasta=@Fechahasta, @TiendaId=@TiendaId", prmFechaInicio, prmFechaFin, prmTiendaId).ToList();
                    return data;
                }
            }
            catch (Exception e)
            {
                var m = e.Message.ToString();
                return null;
            }
        }

        public List<SpListadoResumenTopVentaProducto> ListadoResumenTopVentaProducto(int tiendaId, string fechaIni, string fechaFin)
        {
            try
            {
                using (var context = new SpContext())
                {
                    var prmFechaIni = new SqlParameter("@FechaIni", fechaIni);
                    var prmFechaFin = new SqlParameter("@FechaFin", fechaFin);
                    var prmTiendaId = new SqlParameter("@TiendaId", tiendaId);
                    List<SpListadoResumenTopVentaProducto> data = context.ListadoResumenTopVentaProducto.FromSqlRaw("EXEC [dbo].[ListadoResumenTopVentaProducto] @FechaIni=@FechaIni, @FechaFin=@FechaFin, @TiendaId=@TiendaId", prmFechaIni, prmFechaFin, prmTiendaId).ToList();
                    return data;
                }
            }
            catch (Exception e)
            {
                var m = e.Message.ToString();
                return null;
            }
        }

        public List<SpListadoResumenTopVentaProducto> ListadoResumenNoTopVentaProducto(int tiendaId, string fechaIni, string fechaFin)
        {
            try
            {
                using (var context = new SpContext())
                {
                    var prmFechaIni = new SqlParameter("@FechaIni", fechaIni);
                    var prmFechaFin = new SqlParameter("@FechaFin", fechaFin);
                    var prmTiendaId = new SqlParameter("@TiendaId", tiendaId);
                    List<SpListadoResumenTopVentaProducto> data = context.ListadoResumenTopVentaProducto.FromSqlRaw("EXEC [dbo].[ListadoResumenDownVentaProducto] @FechaIni=@FechaIni, @FechaFin=@FechaFin, @TiendaId=@TiendaId", prmFechaIni, prmFechaFin, prmTiendaId).ToList();
                    return data;
                }
            }
            catch (Exception e)
            {
                var m = e.Message.ToString();
                return null;
            }
        }

        public List<SpListadoDonaciones> ListadoDonacion(ParamsLfViewModel filtroLF)
        {
            try
            {
                using (var context = new SpContext())
                {
                    filtroLF.Caja = filtroLF.Caja != null ? filtroLF.Caja : "0";
                    //filtroLF.Tasa = filtroLF.Tasa != null ? filtroLF.Tasa : 0;
                    filtroLF.Tienda = filtroLF.Tienda != null ? filtroLF.Tienda : "0";

                    /*var paramTurno = new SqlParameter("@Turno", filtroLF.Turno);
                    var paramCaja = new SqlParameter("@Caja", filtroLF.Caja);
                    var paramTasa = new SqlParameter("@Tasa", filtroLF.Tasa);
                    var paramFecha = new SqlParameter("@Fecha", filtroLF.Fecha);*/
                    var paramTienda = new SqlParameter("@TiendaId", filtroLF.Tienda);

                    List<SpListadoDonaciones> data = context.ListadoDonaciones
                        .FromSqlRaw("EXEC [dbo].[ListadoDonacion] @TiendaId=@TiendaId", paramTienda).ToList();

                    return data;
                }
            }
            catch(Exception e){
                return null;
            }
        }

        public List<SpBuscarTasas> BuscarTasas(string pFecha)
        {
            try
            {
                using (var context = new SpContext())
                {
                    var parametro = new SqlParameter("@pFecha", pFecha);

                    List<SpBuscarTasas> data = context.BuscarTasas
                        .FromSqlRaw("EXEC [dbo].[BuscarTasas] @pFecha=@pFecha", parametro).ToList();

                    return data;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public int GuardarTasa(string pFecha, double pTasaUsd, double pTasaEur, int pUsuario)
        {
            try
            {
                using (var context = new SpContext())
                {
                    var parametro = new List<SqlParameter>()
                    {
                        new SqlParameter("@pFecha", pFecha),
                        new SqlParameter("@pTasaUsd", pTasaUsd),
                        new SqlParameter("@pTasaEur", pTasaEur),
                        new SqlParameter("@pUsuario", pUsuario),
                    };

                    List<SpGuardarTasa> data = context.GuardarTasa
                        .FromSqlRaw("EXEC [dbo].[GuardarTasa] @pFecha=@pFecha, @pTasaUsd=@pTasaUsd, @pTasaEur=@pTasaEur, @pUsuario=@pUsuario", parametro.ToArray()).ToList();

                    return (int)data[0].r;
                }
            }
            catch (Exception e)
            {
                return 0;
            }
        }


    }
}
