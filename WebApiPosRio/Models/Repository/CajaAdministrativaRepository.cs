using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using X.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using WebApiPosRio.Models.DB;
using WebApiPosRio.Models.Response;
using WebApiPosRio.Models.ViewModels;

namespace WebApiPosRio.Models.Repository
{
    public class CajaAdministrativaRepository : RIOPOSContext, ICajaAdministrativaRepository
    {
        private readonly ISpRepository _SpRepo;
        private readonly IUsuarioRepository _Usuario;        

        public CajaAdministrativaRepository(ISpRepository spRepo, IUsuarioRepository usuarioRepository)
        {
            _SpRepo = spRepo;
            _Usuario = usuarioRepository;
        }

        public ResponseData DetalleMoneda()
        {
            ResponseData oR = new ResponseData();
            var data = _SpRepo.DetalleMoneda();
            if(data != null)
            {
                oR.Success = 1;
                oR.Data = data;
                oR.Message = "Datos Encontrados!!";
            }
            else
            {
                oR.Success = 0;
                oR.Data = null;
                oR.Message = "Datos No encontrados";
            }
            
            return oR;
        }

        public List<SpDetalleFormaPagoCajaAdm> DetalleAdministracionCaja(List<SpDetalleFormaPago> spDetalleFormaPagos)
        {
            List<SpDetalleFormaPagoCajaAdm> listaformaPagoCA = new List<SpDetalleFormaPagoCajaAdm>();
            foreach (SpDetalleFormaPago item in spDetalleFormaPagos)
            {
                listaformaPagoCA.Add(new SpDetalleFormaPagoCajaAdm {
                    CajaId= item.CajaId,
                    CodigoCaja= item.CodigoCaja,
                    Turno = item.Turno,
                    FormaPagoId = item.FormaPagoId,
                    Descripcion= item.Descripcion,
                    TotalFormaPago=item.TotalFormaPago,
                    TotalPagoSegunCaja=0
                });
            }

            return listaformaPagoCA;
        }

        public ResponseData DetalleFormaPago(string codMoneda, int IdTurno)
        {
            ResponseData oR = new ResponseData();
            List<SpDetalleFormaPago> data = _SpRepo.DetalleFormaPago(codMoneda, IdTurno);
            var dataCajAdm = DetalleAdministracionCaja(data);
            if (data != null)
            {
                oR.Success = 1;
                oR.Data = dataCajAdm;
                oR.Message = "Datos Encontrados!!";
            }
            else
            {
                oR.Success = 0;
                oR.Data = null;
                oR.Message = "Datos No encontrados";
            }

            return oR;
        }

        public ArqueoData GetArqueo(int TurnoId)
        {
            try
            {
                return (from a in Arqueos
                        where (a.TurnoId == TurnoId)
                        select new ArqueoData
                        {
                            Id = a.Id,
                            Observacion = a.Observacion,
                            Fecha = a.Fecha,
                            Estatus = a.Estatus,
                            DetalleArqueo = new List<object>()

                        }).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        object GetDataArqueo(int TurnoId)
        {
            try
            {
                var arqueoData = GetArqueo(TurnoId);

                var detalleData = (from da in DetalleArqueos
                                   where (da.ArqueoId == arqueoData.Id)
                                   select new
                                   {
                                        Monto = da.Monto,
                                        FormaPagoId = da.FormaPagoId,
                                        Tipo = da.Tipo
                                   }).OrderBy(da => da.FormaPagoId).ToList();

                arqueoData.DetalleArqueo.AddRange(detalleData);
                return arqueoData;
            }
            catch
            {
                return null;
            }
        }

        public ResponseData DesgloseDolarArqueo(int ArqueoId)
        {
            ResponseData oR = new ResponseData();
            List<SpDescloseDolar> data = _SpRepo.DesgloseDolarArqueo(ArqueoId);

            var desgloseDolarEuro = new
            {
                desgloseDolar = data.Where(dde => dde.Moneda == 1).ToList(),
                desgloseEuro = data.Where(dde => dde.Moneda == 2).ToList()
            };

            if (data != null)
            {
                oR.Success = 1;
                oR.Data = desgloseDolarEuro;
                oR.Message = "Datos Encontrados!!";
            }
            else
            {
                oR.Success = 0;
                oR.Data = null;
                oR.Message = "Datos No encontrados";
            }

            return oR;
        }

        public ResponseData DetalleFormaPagoTurnoVerificado(string codMoneda, int IdTurno)
        {
            ResponseData oR = new ResponseData();
            List<SpDetalleFormaPago> data = _SpRepo.DetalleFormaPago(codMoneda, IdTurno);

            var dataArqueo = GetDataArqueo(IdTurno);

            var dataCajAdm = DetalleAdministracionCaja(data);
            if (data != null)
            {
                oR.Success = 1;
                oR.Data = new { 
                    Arqueo= dataArqueo,
                    detalleFormaPago = dataCajAdm };
                oR.Message = "Datos Encontrados!!";
            }
            else
            {
                oR.Success = 0;
                oR.Data = null;
                oR.Message = "Datos No encontrados";
            }

            return oR;
        }

        public string GetcodigoCajaByIdUSer(int IdUser)
        {
            var codigoCaja = (from t in Turnos
                              join c in Cajas
                              on t.CajaId equals c.Id                              
                              where (t.UsuarioId == IdUser)                              
                              select new
                              {
                                  Id = t.Id,
                                  CodigoCaja = c.CodigoCaja
                              }).OrderByDescending(x => x.Id).FirstOrDefault();
            return (codigoCaja!=null)?codigoCaja.CodigoCaja:"";
        }

        public string GetcodigoCajaByIdTurno(int IdTurno)
        {
            var codigoCaja = (from t in Turnos
                              join c in Cajas
                              on t.CajaId equals c.Id
                              where (t.Id == IdTurno)
                              select new
                              {
                                  Id = t.Id,
                                  CodigoCaja = c.CodigoCaja
                              }).FirstOrDefault();
            return (codigoCaja != null) ? codigoCaja.CodigoCaja : "";
        }

        public ResponseData FacturadoXCajaXFecha(ParamsLfViewModel paramsLfViewModel)
        {
            ResponseData oR = new ResponseData();
            int IdBuscar = -1;
            var dataCajas = _SpRepo.FacturadoXCajaXFecha(paramsLfViewModel.Fecha);

            if (paramsLfViewModel.Caja !=null)
            {
                dataCajas = dataCajas.Where(dc => dc.CodigoCaja.Contains(paramsLfViewModel.Caja)).ToList();
            }

            if (paramsLfViewModel.NombreCajera != null && paramsLfViewModel.NombreCajera != "")
            {
                IdBuscar = _Usuario.getIdUserbyName(paramsLfViewModel.NombreCajera);
                if (IdBuscar > 0)
                {
                    string codCaja = GetcodigoCajaByIdUSer(IdBuscar);
                    dataCajas = dataCajas.Where(dc => dc.CodigoCaja.Contains(codCaja)).ToList();
                }
            }

            if (paramsLfViewModel.Turno>0)
            {
                string codCaja = GetcodigoCajaByIdTurno(paramsLfViewModel.Turno);
                dataCajas = dataCajas.Where(dc => dc.CodigoCaja.Contains(codCaja)).ToList();
            }

            var paginacion = dataCajas.ToPagedList(paramsLfViewModel.Page, paramsLfViewModel.PerPage);
            var paginateData = new
            {
                Cajas = paginacion,
                totalrow = paginacion.TotalItemCount
            };

            if (dataCajas.Count > 0)
            {
                oR.Success = 1;
                oR.Data = paginateData;
                oR.Message = "Datos Encontrados!!";
            }
            else
            {
                oR.Success = 0;
                oR.Data = null;
                oR.Message = "Datos No encontrados";
            }

            return oR;
        }

        public ResponseData FacturadoXTurnoXFecha(string fechaActual, int page, int perPage, string CodigoCaja = "0")
        {
            ResponseData oR = new ResponseData();
            var data = _SpRepo.FacturadoXTurnoXFecha(fechaActual, CodigoCaja );
            if (data != null)
            {
                oR.Success = 1;
                oR.Data = data.ToPagedList(page, perPage);
                oR.Message = "Datos Encontrados!!";
            }
            else
            {
                oR.Success = 0;
                oR.Data = null;
                oR.Message = "Datos No encontrados";
            }

            return oR;
        }

        public ResponseData ListadoFactura(ParamsLfViewModel paramsLFViewModel)
        {
            ResponseData oR = new ResponseData();
            var data = _SpRepo.ListadoFactura(paramsLFViewModel);
            if (data != null)
            {
                oR.Success = 1;
                oR.Data = data;
                oR.Message = "Datos Encontrados!!";
            }
            else
            {
                oR.Success = 0;
                oR.Data = null;
                oR.Message = "Datos No encontrados";
            }

            return oR;
        }

        public ResponseData DetalleFactura(DetalleFacturaviewModel detalleFacturaviewModel)
        {
            ResponseData oR = new ResponseData();
            var facturaData = _SpRepo.DetalleFactura(detalleFacturaviewModel);
            var clienteData = GetCliente(detalleFacturaviewModel.ClienteId);
            if (facturaData != null)
            {
                oR.Success = 1;
                oR.Data = new { 
                    cliente = clienteData,
                    detalleFactura = facturaData
                };
                oR.Message = "Datos Encontrados!!";
            }
            else
            {
                oR.Success = 0;
                oR.Data = null;
                oR.Message = "Datos No encontrados";
            }

            return oR;
        }

        public ResponseData DetalleFormaPagoFacturaId(int IdFactura)
        {
            ResponseData oR = new ResponseData();
            var data = _SpRepo.FormasPagoFacturaId(IdFactura);
            if (data != null)
            {
                oR.Success = 1;
                oR.Data = data;
                oR.Message = "Datos Encontrados!!";
            }
            else
            {
                oR.Success = 0;
                oR.Data = null;
                oR.Message = "Datos No encontrados";
            }

            return oR;
        }

        public object GetCliente(int ClienteId)
        {
            try
            {
                var clienteData = (from c in Clientes
                                   //join c in Clientes
                                   //on f.ClienteId equals c.Id
                                   where (c.Id == ClienteId)//ojoaqui
                                   select new
                                   {
                                       Nombre = c.Nombre,
                                       Apellido = c.Apellido,
                                       CedRif = c.Rif,
                                       Telefono = c.Telefono,
                                       Direccion = c.Direccion
                                   }).FirstOrDefault();
                return clienteData;
            }
            catch
            {
                return null;
            }            
        }

        public XmlDocument CreateXmlSaveActions(int idUser, ArqueoViewModel arqueoViewModel)
        {

            //xml
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("Root");
            XmlElement eAcciones = doc.CreateElement("Arqueo");
            eAcciones.SetAttribute("TiendaId", arqueoViewModel.TiendaId.ToString());
            eAcciones.SetAttribute("TurnoId", arqueoViewModel.TurnoId.ToString());
            eAcciones.SetAttribute("Observacion", arqueoViewModel.Observacion);

            if (arqueoViewModel.DetalleArqueo != null)
            {
                arqueoViewModel.DetalleArqueo.ForEach(a =>
                {
                    XmlElement detalle = doc.CreateElement("DetalleArqueo");
                    detalle.SetAttribute("Monto", a.TotalPagoSegunCaja.ToString().Replace(",", "."));
                    detalle.SetAttribute("FormaPagoId", a.FormaPagoId.ToString());
                    eAcciones.AppendChild(detalle);
                });

                arqueoViewModel.DesgloseEfectivoD.ForEach(a =>
                {
                    if (a.Monto > 0)
                    {
                        XmlElement desglose = doc.CreateElement("DesgloseDolar");
                        desglose.SetAttribute("Monto", a.Monto.ToString().Replace(",","."));
                        desglose.SetAttribute("Denominacion", a.Denominacion.ToString());
                        desglose.SetAttribute("Cantidad", a.Cantidad.ToString());
                        desglose.SetAttribute("Moneda", a.Moneda.ToString());
                        eAcciones.AppendChild(desglose);
                    }
                });

                arqueoViewModel.DesgloseEfectivoE.ForEach(a =>
                {
                    if (a.Monto > 0)
                    {
                        XmlElement desglose = doc.CreateElement("DesgloseDolar");
                        desglose.SetAttribute("Monto", a.Monto.ToString().Replace(",", "."));
                        desglose.SetAttribute("Denominacion", a.Denominacion.ToString());
                        desglose.SetAttribute("Cantidad", a.Cantidad.ToString());
                        desglose.SetAttribute("Moneda", a.Moneda.ToString());
                        eAcciones.AppendChild(desglose);
                    }
                });

                if (arqueoViewModel.DesglosePuntoExterno !=null)
                {
                    arqueoViewModel.DesglosePuntoExterno.ForEach(a =>
                    {
                        if (a.Monto > 0)
                        {
                            XmlElement desglosePe = doc.CreateElement("DesglosePuntoExterno");
                            desglosePe.SetAttribute("Monto", a.Monto.ToString().Replace(",", "."));
                            desglosePe.SetAttribute("Tipo", ((a.Tipo.ToString() == "TDD") ? "1" : "2"));
                            desglosePe.SetAttribute("Banco", a.BancoId.ToString());
                            eAcciones.AppendChild(desglosePe);
                        }
                    });
                }
            }
            //eAcciones.AppendChild(age);
            root.AppendChild(eAcciones);
            doc.AppendChild(root);


            return doc;
        }

        public ResponseData AddArqueo(ArqueoViewModel arqueoViewModel)
        {
            ResponseData responseData = new ResponseData();
            string xml = CreateXmlSaveActions(1, arqueoViewModel).InnerXml;
            responseData.Success = _SpRepo.SpAddArqueo(arqueoViewModel, xml);
            if (responseData.Success == 1)
            {
                responseData.Message = "Datos Actualizados Satisfactoriamente";
            }
            else
            {
                responseData.Message = "Error Agregando Arqueo  (SpAddArqueo)";
            }
            return responseData;
        }

        public ResponseData ResetArqueo(int idTurno, int idUsuario)
        {
            ResponseData oR = new ResponseData();
            if(idTurno>0)
                oR.Success = _SpRepo.ResetArqueo(idTurno, idUsuario);

            oR.Data = null;
            if (oR.Success == 1)
            {
                oR.Message = "Datos Actualizados Satisfactoriamente";
            }
            else
            {
                oR.Message = "Error actualizando Arqueo  (sp_ResetArqueo)";
            }
            return oR;
        }

        public ResponseData UpdateArqueoEstatus(int idTurno, int idUsuario)
        {
            ResponseData oR = new ResponseData();

            oR.Success = _SpRepo.VerificaTurno(idTurno, idUsuario);
            oR.Data = null;
            if (oR.Success == 1)
            {                
                oR.Message = "Datos Actualizados Satisfactoriamente";
            }
            else {
                oR.Message = "Error actualizando Turno  (sp_verificaturno)";
            }
            return oR;
        }

        public ResponseData UpdateFacturaEstatus(CajaAdmViewModel Factura)
        {
            ResponseData oR = new ResponseData();

            //actualiza factura a impresa estatus:1
            oR.Success = _SpRepo.ActualizarFactura(Factura.FacturaId, Factura.UserId, 1);
            oR.Data = null;
            if (oR.Success == 1)
            {
                oR.Message = "Datos Actualizados Satisfactoriamente";
            }
            else
            {
                oR.Message = "Error actualizando Turno  (sp_ActualizarFactura)";
            }
            return oR;
        }

        public ResponseData AnularFactura(CajaAdmViewModel Factura)
        {
            ResponseData oR = new ResponseData();

            //Actualiza a estatus: 2, anulado
            oR.Success = _SpRepo.ActualizarFactura(Factura.FacturaId, Factura.UserId, 2);
            oR.Data = null;
            if (oR.Success == 1)
            {
                oR.Message = "Datos Actualizados Satisfactoriamente";
            }
            else
            {
                oR.Message = "Error actualizando Turno  (sp_ActualizarFactura)";
            }
            return oR;
        }

        public ResponseData ReporteCierreTurno(int cierreTurno)
        {
            ResponseData oR = new ResponseData();
            var data = new
            {
                Reporte = _SpRepo.MostrarCierreTurno(cierreTurno),
                FechaHora = DateTime.Now.ToLocalTime()
            };
            if (data != null)
            {
                oR.Success = 1;
                oR.Data = data;
                oR.Message = "Datos Encontrados!!";
            }
            else
            {
                oR.Success = 0;
                oR.Data = null;
                oR.Message = "Datos No encontrados";
            }

            return oR;
        }

        public ResponseData ReporteCierreTurnosS4(string fecha)
        {
            ResponseData oR = new ResponseData();
            var dataCierreTurnos = _SpRepo.ReporteCierreTurnosS4(fecha);
            var data = new
            {               
                Procesado = dataCierreTurnos.Where(p => p.Origen == "procesado").ToList(),
                Declarado = dataCierreTurnos.Where(p => p.Origen == "declarado").ToList(),
                FechaHora = DateTime.Now.ToLocalTime()
            };
            if (data != null)
            {
                oR.Success = 1;
                oR.Data = data;
                oR.Message = "Datos Encontrados!!";
            }
            else
            {
                oR.Success = 0;
                oR.Data = null;
                oR.Message = "Datos No encontrados";
            }

            return oR;
        }

        public ResponseData ReporteCierreCaja(string codCaja, string fecha)
        {
            ResponseData oR = new ResponseData();
            var dataCierre = _SpRepo.MostrarCierreCaja(codCaja, fecha);

            var data = new
            {
                Procesado = dataCierre.Where(p => p.Origen == "procesado").ToList(),
                Declarado = dataCierre.Where(p => p.Origen == "declarado").ToList(),
                FechaHora = DateTime.Now.ToLocalTime()
            };
            if (data != null)
            {
                oR.Success = 1;
                oR.Data = data;
                oR.Message = "Datos Encontrados!!";
            }
            else
            {
                oR.Success = 0;
                oR.Data = null;
                oR.Message = "Datos No encontrados";
            }

            return oR;
        }

        public List<FormaPagoConfViewModel> GetFormasPago()
        {
            try
            {
                List<FormaPagoConfViewModel> formaPagoData = (from c in FormaPagos
                                  // where (c.Id == ClienteId)//ojoaqui
                                   select new FormaPagoConfViewModel
                                   {
                                       Id = c.Id,
                                       Nombre = c.Descripcion,

                                   }).ToList();
                return formaPagoData;
            }
            catch
            {
                return null;
            }
        }

        public List<ReportViewModel> ListadoDecProcExcel(ResumenCajaFiltro resumenCajaFiltro)
        {
            var dataCierre = _SpRepo.DeclaradoyProcesado(resumenCajaFiltro.Caja, resumenCajaFiltro.Fecha);


            if (resumenCajaFiltro.FormaPagoId > 0)
            {
                dataCierre = dataCierre.Where(dfp => dfp.FormaPagoId == resumenCajaFiltro.FormaPagoId).ToList();
            }

            if (resumenCajaFiltro.Turno != null)
            {
                if (resumenCajaFiltro.Turno != "0")
                    dataCierre = dataCierre.Where(dfp => dfp.Turno == int.Parse(resumenCajaFiltro.Turno)).ToList();
            }

            if (resumenCajaFiltro.EstatusTurno > 0)
            {
                dataCierre = dataCierre.Where(dfp => dfp.EstatusTurno == resumenCajaFiltro.EstatusTurno).ToList();
            }

            if (resumenCajaFiltro.EstatusDepo > 0)
            {
                //dataCierre = dataCierre.Where(dfp => dfp.EstatusArqueo == resumenCajaFiltro.EstatusDepo).ToList();
            }

            if (resumenCajaFiltro.Ordenar > 0)
            {
                switch (resumenCajaFiltro.Ordenar)
                {
                    case 1: dataCierre = dataCierre.OrderBy(o => o.Caja).ToList(); break;
                    case 2: dataCierre = dataCierre.OrderBy(o => o.Turno).ToList(); break;
                    case 3: dataCierre = dataCierre.OrderBy(o => o.FormaPagoId).ToList(); break;
                }
            }

            var datag = dataCierre.GroupBy(x => x.Turno).ToList();

            List<ReportViewModel> decproc = new List<ReportViewModel>();

            foreach (var item in datag)
            {
                int key = item.Key;
                List<SpDeclaradoProcesado> obj = item.Select(s => s).ToList();
                var declarado = obj.Where(p => p.Origen == "declarado").ToList();
                var procesado = obj.Where(p => p.Origen == "procesado").ToList();

                foreach (var proc in procesado)
                {
                    try
                    {
                        var dataDeclarada = declarado.Where(dcl => dcl.FormaPagoId == proc.FormaPagoId).FirstOrDefault();
                        decproc.Add(new ReportViewModel
                        {
                            Procesado = proc.Monto,
                            ProcesadoBs = proc.MontoBs,
                            Declarado = dataDeclarada != null ? dataDeclarada.Monto : 0,
                            Caja = proc.Caja,
                            Turno = proc.Turno,
                            FormaPagoId = proc.FormaPagoId,
                            Descripcion = proc.Descripcion,
                            EstatusArqueo = dataDeclarada != null ? dataDeclarada.EstatusArqueo : 0,
                            EstatusTurno = proc.EstatusTurno,
                            TiendaId = proc.TiendaId
                        });
                    }
                    catch
                    {
                    }
                }
            }
            return decproc;
        }

        public ResponseData ListadoDeclaradoProcesado(ResumenCajaFiltro resumenCajaFiltro, int page, int perPage)
        {
            ResponseData oR = new ResponseData();
            var dataCierre = _SpRepo.DeclaradoyProcesado(resumenCajaFiltro.Caja, resumenCajaFiltro.Fecha);


            if (resumenCajaFiltro.FormaPagoId > 0)
            {
                dataCierre = dataCierre.Where(dfp => dfp.FormaPagoId == resumenCajaFiltro.FormaPagoId).ToList();
            }

            if (resumenCajaFiltro.Turno != null)
            {
                if(resumenCajaFiltro.Turno!="0")
                dataCierre = dataCierre.Where(dfp => dfp.Turno == int.Parse(resumenCajaFiltro.Turno)).ToList();
            }

            if (resumenCajaFiltro.EstatusTurno > 0)
            {
            dataCierre = dataCierre.Where(dfp => dfp.EstatusTurno == resumenCajaFiltro.EstatusTurno).ToList();
            }

            if (resumenCajaFiltro.EstatusDepo > 0)
            {
                //dataCierre = dataCierre.Where(dfp => dfp.EstatusArqueo == resumenCajaFiltro.EstatusDepo).ToList();
            }

            if (resumenCajaFiltro.Ordenar > 0)
            {
                switch (resumenCajaFiltro.Ordenar)
                {
                    case 1: dataCierre = dataCierre.OrderBy(o => o.Caja).ToList(); break;
                    case 2: dataCierre = dataCierre.OrderBy(o => o.Turno).ToList(); break;
                    case 3: dataCierre = dataCierre.OrderBy(o => o.FormaPagoId).ToList(); break;
                }
            }

            var datag = dataCierre.GroupBy(x => x.Turno).ToList();

            List<object> decproc = new List<object>();

            foreach (var item in datag)
            {
                int key = item.Key;
                List<SpDeclaradoProcesado> obj = item.Select(s => s).ToList();
                var declarado = obj.Where(p => p.Origen == "declarado").ToList();
                var procesado = obj.Where(p => p.Origen == "procesado").ToList();

                foreach (var proc in procesado)
                {
                    try
                    {
                        var dataDeclarada = declarado.Where(dcl => dcl.FormaPagoId == proc.FormaPagoId).FirstOrDefault();
                        decproc.Add(new
                        {
                            Procesado = proc.Monto,
                            ProcesadoBs = proc.MontoBs,
                            Declarado = dataDeclarada != null? dataDeclarada.Monto:0,
                            Caja = proc.Caja,
                            Turno = proc.Turno,
                            FormaPagoId = proc.FormaPagoId,
                            Descripcion = proc.Descripcion,
                            EstatusArqueo = dataDeclarada != null ? dataDeclarada.EstatusArqueo : 0,
                            EstatusTurno = proc.EstatusTurno,
                            TiendaId = proc.TiendaId
                        });
                    }
                    catch
                    {
                    }
                }

                
            }

            //paginacion de data
            var paginacion = decproc.ToPagedList(page, perPage);
            var paginateData = new
            {
                ResumenTurnoFp = paginacion,
                totalrow = paginacion.TotalItemCount
            };


            if (decproc != null)
            {
                oR.Success = 1;
                oR.Data = paginateData;
                oR.Message = "Datos Encontrados!!";
            }
            else
            {
                oR.Success = 0;
                oR.Data = null;
                oR.Message = "Datos No encontrados";
            }

            return oR;
        }

        public ResponseData ListadoTurnosCajaXDia()
        {
            ResponseData oR = new ResponseData();
            

            var data = new
            {
               // Procesado = dataCierre.Where(p => p.Origen == "procesado").ToList(),
               // Declarado = dataCierre.Where(p => p.Origen == "declarado").ToList(),
                FechaHora = DateTime.Now.ToLocalTime()
            };
            if (data != null)
            {
                oR.Success = 1;
                oR.Data = data;
                oR.Message = "Datos Encontrados!!";
            }
            else
            {
                oR.Success = 0;
                oR.Data = null;
                oR.Message = "Datos No encontrados";
            }

            return oR;
        }

        public ResponseData ListadoFaltantesSobrantes(int IdTurno, int IdCaja, int IdUsuario, string nombreCajera, string FormaPagoId, string fechaI, string fechaF,int page, int perPage)
       {
            ResponseData oR = new ResponseData();

            if (nombreCajera !=null)
            {
                IdUsuario = _Usuario.getIdUserbyName(nombreCajera);
            }

            if (IdTurno < 0)
            {
                IdTurno = 0;
            }

            var dataSF= _SpRepo.ListadoSobrantesFaltantes(IdTurno, IdCaja, IdUsuario, fechaI, fechaF);

            if (FormaPagoId != null)
            {
                if (FormaPagoId != "0")
                    dataSF = dataSF.Where(dsf => dsf.Descripcion.Contains(FormaPagoId)).ToList();
            }

            var paginacion = dataSF.ToPagedList(page, perPage);
            var paginateData = new
            {
                Facturas = paginacion,
                totalrow = paginacion.TotalItemCount
            };
            //return (paginateData != null) ? Ok(paginateData) : BadRequest(paginateData);
            if (dataSF != null)
            {
                oR.Success = 1;
                oR.Data = paginateData;
                oR.Message = "Datos Encontrados!!";
            }
            else
            {
                oR.Success = 0;
                oR.Data = null;
                oR.Message = "Datos No encontrados";
            }

            return oR;
        }

        public List<SpListadoFacturaRangFech> ListadoFacturaRangFech(FiltroListadoFactura filtroListadoFactura)
        {
            var dataFacturas = _SpRepo.ListadoFacturaRangFech(filtroListadoFactura.NroDocumento, filtroListadoFactura.NroFactura, filtroListadoFactura.ZetaSerial, filtroListadoFactura.FechaI, filtroListadoFactura.FechaF);

            if (filtroListadoFactura.Estatus >= 0)
            {
                dataFacturas = dataFacturas.Where(df => df.Estatus == filtroListadoFactura.Estatus).ToList();
            }

            if (filtroListadoFactura.IdTurno > 0)
            {
                dataFacturas = dataFacturas.Where(df => df.TurnoId == filtroListadoFactura.IdTurno).ToList();
            }

            return dataFacturas;
        }

        public List<SpReporteFormasPago> ReporteFormasPagoRangFech(TesoreriaViewModel tesoreriaViewModel)
        {
            int IdBuscar = -1;
            var dataFacturas = _SpRepo.ReporteFormasPagoRangFech(tesoreriaViewModel.IdTienda, tesoreriaViewModel.IdCaja, ((tesoreriaViewModel.IdTurno!=null)?int.Parse(tesoreriaViewModel.IdTurno):0), tesoreriaViewModel.FechaI, tesoreriaViewModel.FechaF);

            if (tesoreriaViewModel.NombreCajera !=null && tesoreriaViewModel.NombreCajera != "")
            {
                IdBuscar = _Usuario.getIdUserbyName(tesoreriaViewModel.NombreCajera);               
            }

            if (tesoreriaViewModel.FormaPagoId != null)
            {
                if(Int32.Parse(tesoreriaViewModel.FormaPagoId)>0)
                    dataFacturas = dataFacturas.Where(df => df.FormaPagoId == Int32.Parse(tesoreriaViewModel.FormaPagoId)).ToList();
            }

            return dataFacturas;
        }

        public List<SpResumenxCajaxFecha> ResumenCajaXTurnosEstatus(string fecha, string CodCaja = "0")
        {
            var data = _SpRepo.ResumenxCajaxFechas(fecha, CodCaja);
            return data;
        }
    
        public List<SpDesglosePuntos> ResumenDesglosePuntos(string Fecha, int TurnoId, int CajaId)
        {
            var data = _SpRepo.GetDesglosePuntos(Fecha, TurnoId, CajaId);
            return data;
        }

        public List<SpDescloseDolar> ResumenDesgloseDolar(string Fecha, int TurnoId)
        {
            var data = _SpRepo.GetDesgloseEfectivoDE(Fecha, TurnoId);
            return data;
        }
    }
}
