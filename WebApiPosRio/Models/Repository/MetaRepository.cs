using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPosRio.Models.DB;
using WebApiPosRio.Models.ViewModels;

namespace WebApiPosRio.Models.Repository
{
    public class MetaRepository : RIOPOSContext, IMetaRepository
    {
        private readonly ITiendaRepository _tiendaRepository;
        private readonly IInsideRepository _insideDashboard;
        public MetaRepository(ITiendaRepository tiendaRepository, IInsideRepository insideRepository)
        {
            _tiendaRepository = tiendaRepository;
            _insideDashboard = insideRepository;
        }

        public object GetDataMeta(InsideVentaViewModel insideVentaViewModel)
        {
            DateTime FechaIni = Convert.ToDateTime(insideVentaViewModel.FechaIni);
            DateTime FechaFin = Convert.ToDateTime(insideVentaViewModel.FechaFin).AddDays(4);

            List<Metum> oDataEstimado = null;
            if(insideVentaViewModel.tiendaId>0)
                oDataEstimado = Meta.Where(mt => (mt.Inicio >= FechaIni && mt.Fin <= FechaFin) && mt.TiendaId == insideVentaViewModel.tiendaId).ToList();
            else
                oDataEstimado = Meta.Where(mt => (mt.Inicio >= FechaIni && mt.Fin <= FechaFin)).ToList();
            //var groupdata = oDataEstimado.GroupBy(d => d.Inicio).ToList();
            //.Sum(mp => mp.Monto)
            var oDataVenta = _insideDashboard.MostrarVentaDepartamento(insideVentaViewModel).ToList();

            return new {
                Estimado = oDataEstimado,
                Venta = oDataVenta
            };
        }

        public object GetDataMetaAcum(InsideVentaViewModel insideVentaViewModel)
        {
            DateTime f = Convert.ToDateTime(insideVentaViewModel.FechaIni);
            f = f.AddDays((-1 * f.Day) + 1);
            if (f.Day == 1) insideVentaViewModel.FechaIni = f.ToString("yyyy-MM-dd");
            DateTime FechaIni = Convert.ToDateTime(insideVentaViewModel.FechaIni);
            DateTime FechaFin = Convert.ToDateTime(insideVentaViewModel.FechaFin);

            double oDataEstimado = 0;
            if (insideVentaViewModel.tiendaId > 0)
                oDataEstimado = Meta.Where(mt => (mt.Inicio >= FechaIni && mt.Fin <= FechaFin) && mt.TiendaId == insideVentaViewModel.tiendaId).Sum(mp => mp.Monto);
            else
                oDataEstimado = Meta.Where(mt => (mt.Inicio >= FechaIni && mt.Fin <= FechaFin)).Sum(mp => mp.Monto);

            var oDataVenta = _insideDashboard.MostrarVentaDepartamento(insideVentaViewModel).Sum(iv => iv.TotalDolares);

            return new
            {
                Estimado = oDataEstimado,
                Venta = oDataVenta
            };
        }       

        public List<VentasMetasPorDia> GetListMeta(InsideVentaViewModel insideVentaViewModel)
        {
            DateTime f = Convert.ToDateTime(insideVentaViewModel.FechaIni);           
            f = f.AddDays((-1*f.Day)+1);
            if (f.Day == 1) insideVentaViewModel.FechaIni = f.ToString("yyyy-MM-dd");
            DateTime FechaIni = Convert.ToDateTime(insideVentaViewModel.FechaIni);
            DateTime FechaFin = Convert.ToDateTime(insideVentaViewModel.FechaFin);

            List<Metum> oDataEstimado = null;
            if (insideVentaViewModel.tiendaId > 0)
                oDataEstimado = Meta.Where(mt => (mt.Inicio >= FechaIni && mt.Fin <= FechaFin) && mt.TiendaId == insideVentaViewModel.tiendaId).OrderBy(m => m.Inicio).ToList();
            else {
                oDataEstimado = Meta.Where(mt => (mt.Inicio >= FechaIni && mt.Fin <= FechaFin)).OrderBy(m => m.Inicio).ToList();
                var agrupado = oDataEstimado.GroupBy(ode => ode.Inicio);
                List<Metum> oDataEstimadoAllTiendas = new List<Metum>();
                foreach (var item in agrupado)
                {
                    //var myItem = item.Sum(i => i.Monto);
                    oDataEstimadoAllTiendas.Add(new Metum() {
                        Inicio = item.Key,
                        Fin = item.Key,
                        Monto = item.Sum(i => i.Monto),
                        Estatus = 1,
                        TiendaId = 0,
                        UsuarioId =0,                        
                    });
                }
                oDataEstimado = oDataEstimadoAllTiendas;
            }


            TimeSpan difFechas = FechaFin - FechaIni;
            int dias = difFechas.Days+1;

            List<VentasMetasPorDia> ventasPorDia = new List<VentasMetasPorDia>();

            if (oDataEstimado.Count() > 0)
            {
                SpListadoResumenVenta oDataVenta = null;
                for (int d = 1; d <= dias; d++)
                {
                    insideVentaViewModel.FechaFin = insideVentaViewModel.FechaIni;

                    if (insideVentaViewModel.tiendaId != 0)
                    {
                        oDataVenta = _insideDashboard.MostrarVentaDepartamento(insideVentaViewModel).FirstOrDefault();
                    }
                    else
                    {
                        oDataVenta = new SpListadoResumenVenta(); 
                        var mydata = _insideDashboard.MostrarVentaDepartamento(insideVentaViewModel);

                        oDataVenta.TiendaId = 0;
                        oDataVenta.TicketPromedio = mydata.Sum(md => md.TicketPromedio);
                        oDataVenta.ProdPromedio = mydata.Sum(md => md.ProdPromedio);
                        oDataVenta.Nombre = "Todas las Tiendas";
                        oDataVenta.TotalBolivares = mydata.Sum(md => md.TotalBolivares);
                        oDataVenta.TotalDolares = mydata.Sum(md => md.TotalDolares);
                    }
                    
                    if (oDataVenta != null)
                    {
                        try
                        {
                            ventasPorDia.Add(new VentasMetasPorDia()
                            {
                                Fecha = FechaIni.AddDays(d - 1),
                                VentaTienda = oDataVenta,
                                Metas = oDataEstimado[d - 1],
                                Variacion = (oDataVenta.TotalDolares/*V2*/- oDataEstimado[d - 1].Monto/*V1*/ ) / oDataEstimado[d - 1].Monto/*V1*/ * 100,
                                PresupuestoAcum = (ventasPorDia.Count() > 0 ? oDataEstimado[d - 1].Monto + ventasPorDia[ventasPorDia.Count() - 1].PresupuestoAcum : oDataEstimado[d - 1].Monto),
                                EjecutadoAcum = (ventasPorDia.Count() > 0 ? oDataVenta.TotalDolares + ventasPorDia[ventasPorDia.Count() - 1].EjecutadoAcum : oDataVenta.TotalDolares),
                                VariacionAcum = ((ventasPorDia.Count() > 0 ? oDataVenta.TotalDolares + ventasPorDia[ventasPorDia.Count() - 1].EjecutadoAcum : oDataVenta.TotalDolares)/*V2*/- (ventasPorDia.Count() > 0 ? oDataEstimado[d - 1].Monto + ventasPorDia[ventasPorDia.Count() - 1].PresupuestoAcum : oDataEstimado[d - 1].Monto)/*V1*/ ) / (ventasPorDia.Count() > 0 ? oDataEstimado[d - 1].Monto + ventasPorDia[ventasPorDia.Count() - 1].PresupuestoAcum : oDataEstimado[d - 1].Monto)/*V1*/ * 100,
                            });
                        }
                        catch{}
                    }
                    insideVentaViewModel.FechaIni = FechaIni.AddDays(d).ToString("yyyy-MM-dd");
                }
            }
            
            return ventasPorDia;
        }

        public object GetGrafica(InsideVentaViewModel insideVentaViewModel)
        {
            DateTime f = Convert.ToDateTime(insideVentaViewModel.FechaIni);
            f = f.AddDays((-1 * f.Day) + 1);
            if (f.Day == 1) insideVentaViewModel.FechaIni = f.ToString("yyyy-MM-dd");
            DateTime FechaIni = Convert.ToDateTime(insideVentaViewModel.FechaIni);
            DateTime FechaFin = Convert.ToDateTime(insideVentaViewModel.FechaFin);

            List<GraficaVentas> listDataGrafica = new List<GraficaVentas>();
            var tiendas = _tiendaRepository.GetTiendas().Where(t => t.Id!=0).ToList();
            //dataGrafica.Goals = new List<Goal>();
            //ventasPorDia.ListadoVenta = new List<SpListadoResumenVenta>();

            double oDataEstimado = 0;
            for (int t =1; t <= tiendas.Count(); t++)
            {
                insideVentaViewModel.tiendaId = t;

                oDataEstimado = Meta.Where(mt => (mt.Inicio >= FechaIni && mt.Fin <= FechaFin) && mt.TiendaId == insideVentaViewModel.tiendaId).Sum(mp => mp.Monto);

                var oDataVenta = _insideDashboard.MostrarVentaDepartamento(insideVentaViewModel).Sum(iv => iv.TotalDolares);
                List<Goal> goals = new List<Goal>();
                goals.Add(new Goal() { Value = Helper.Util.Truncar(oDataEstimado) });

                listDataGrafica.Add(new GraficaVentas() {
                 X = tiendas.Where(ti => ti.Id == t).FirstOrDefault().CodigoArea,
                 Y = Helper.Util.Truncar(oDataVenta),
                 Goals = goals
                });
            }



            /*for (int d = 1; d <= dias; d++)
            {
                insideVentaViewModel.FechaFin = FechaIni.AddDays(d).ToString("yyyy-MM-dd");
                var oDataVenta = _insideDashboard.MostrarVentaDepartamento(insideVentaViewModel).FirstOrDefault();
                if (oDataVenta != null)
                {
                    try
                    {
                        lis
                        dataGrafica.X = oDataVenta.Nombre;
                        dataGrafica.Y = oDataVenta.TotalDolares;
                        dataGrafica.Goals.Add(new Goal()
                        {
                            Value = oDataEstimado[d - 1].Monto
                        });
                    }catch
                    { }
                }
                insideVentaViewModel.FechaIni = FechaIni.AddDays(d).ToString("yyyy-MM-dd");
            }*/
            return listDataGrafica.OrderBy(g => g.X).ToList();
        }

        public bool CargaDataExcel(DataMeta dataMeta)
        {
            try
            {
                
                List<Metum> DataMetaSave = new List<Metum>();

                foreach (var pvm in dataMeta.data)
                {
                    DataMetaSave.Add(new Metum {
                        TiendaId = _tiendaRepository.GetTiendaByAcronimo(pvm.Tienda).Id,
                        Monto = pvm.Presupuesto,
                        Inicio = Convert.ToDateTime(pvm.Fecha),
                        Fin = Convert.ToDateTime(pvm.Fecha),
                        Estatus = 1,
                        UsuarioId = dataMeta.IdUser
                    });
                } 

                Meta.AddRange(DataMetaSave);
                this.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
