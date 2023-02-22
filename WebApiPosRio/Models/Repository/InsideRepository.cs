using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebApiPosRio.Models.DB;
using WebApiPosRio.Models.ViewModels;

namespace WebApiPosRio.Models.Repository
{
    public class InsideRepository : IInsideRepository
    {
        private readonly ISpRepository _SpRepo;
        private readonly ITiendaRepository _TiendaRepo;
        private readonly IUsuarioRepository _Usuario;

        public InsideRepository(ISpRepository spRepo, ITiendaRepository tiendaRepo, IUsuarioRepository usuario)
        {
            _SpRepo = spRepo;
            _TiendaRepo = tiendaRepo;
            _Usuario = usuario;
        }

        public decimal MonthDifference(DateTime FechaFin, DateTime FechaInicio)
        {
            return Math.Abs((FechaFin.Month - FechaInicio.Month) + 12 * (FechaFin.Year - FechaInicio.Year));

        }

        public BigLineChart Consolidado(string fechaIni, string fechaFin)
        {
            BigLineChart bigLineChart = new BigLineChart();

            List<SpMostrarConsolidado> dataCon = _SpRepo.MostrarConsolidado(fechaIni, fechaFin);
            if (dataCon==null)
            {
                return null;
            }

            bigLineChart.DataConsolidado = dataCon;
            bigLineChart.ActiveIndex = 0;
            bigLineChart.AllData = new List<List<double>>();

            List<double> absisaYMen = new List<double>() { 0,0,0,0,0,0,0,0,0,0,0,0}; // new List<double>();//mensual
            List<double> absisaYSem = new List<double>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };// { 12000000, 125635214, 12365200, 123654, 12365214, 10002012, 45622520};
            bigLineChart.ChartData = new ChartData();


            foreach (var item in dataCon)
            {
               absisaYMen[item.Mes - 1] = item.Totalfacturadodolar;
               absisaYSem[item.Mes - 1] = item.Totalfacturadobs;                    
            }
            bigLineChart.AllData.Add(absisaYMen);
            bigLineChart.AllData.Add(absisaYSem);
            Meses m = new Meses();


            bigLineChart.ChartData.Labels = m._Meses.ToList();

            return bigLineChart;
        }

        public List<SpMostrarFacturaPromedio> MostrarFacturaPromedios(string fechaIni)
        {
            List<SpMostrarFacturaPromedio> dataCon = _SpRepo.MostrarFacturaPromedios(fechaIni);

            return dataCon;
        }

        public List<SpListaResumenVentaXFormaPago> MostrarFormasPagoInside(string fechaIni, string fechaFin, int tiendaId)
        {
            List<SpListaResumenVentaXFormaPago> dataCon = _SpRepo.MostrarFormasPagoInside(fechaIni, fechaFin, tiendaId);

            return dataCon;
        }

        public List<SpMostrarConsolidado> MostrarConsolidadoTienda(string fechaIni, string fechaFin)
        {
            List<SpMostrarConsolidado> dataCon = _SpRepo.MostrarConsolidado(fechaIni, fechaFin);

            return dataCon;
        }

        public List<SpMostrarVentasXDep> MostrarVentaDepartamento(string fechaIni, string fechaFin)
        {
            List<SpMostrarVentasXDep> dataCon = _SpRepo.MostrarVentasXDep(fechaIni, fechaFin);
        

            return dataCon;
        }

        public List<SpListadoResumenVenta> MostrarVentaDepartamento(InsideVentaViewModel insideFiltro)
        {
            List<SpListadoResumenVenta> dataCon = _SpRepo.ListadoResumenVentaInside(insideFiltro.tiendaId, insideFiltro.FechaIni, insideFiltro.FechaFin);

            return dataCon;
        }

        public List<SpListadoResumenVentaDepartamento> ListadoVentaDepartamentoInside(InsideVentaViewModel insideFiltro)
        {
            List<SpListadoResumenVentaDepartamento> dataCon = _SpRepo.ListadoResumenVentaDepartamentoInside(insideFiltro.tiendaId, insideFiltro.FechaIni, insideFiltro.FechaFin);
            

            if ((insideFiltro.Procedencia!=null)?insideFiltro.Procedencia.Count()>0:false)
            {
                if (insideFiltro.Procedencia[0] != null)
                {
                    var cad = insideFiltro.Procedencia[0].Split(',');
                    if (cad.Length == 1)
                    {
                        if (insideFiltro.Procedencia[0].Contains("nacional"))
                        {
                            dataCon = dataCon.OrderByDescending(x => x.TotalNac).ToList();
                        }
                        else
                        {
                            dataCon = dataCon.OrderByDescending(x => x.TotalInt).ToList();
                        }
                    }
                }
            }

            return dataCon;
        }

        public object ListadoResumenTopVentaProducto(InsideVentaViewModel insideFiltro)
        {
            List<SpListadoResumenTopVentaProducto> dataConTop = _SpRepo.ListadoResumenTopVentaProducto(insideFiltro.tiendaId, insideFiltro.FechaIni, insideFiltro.FechaFin);
            List<SpListadoResumenTopVentaProducto> dataConNoTop = _SpRepo.ListadoResumenNoTopVentaProducto(insideFiltro.tiendaId, insideFiltro.FechaIni, insideFiltro.FechaFin);

            if (dataConTop != null && dataConNoTop != null)
            {
                if (insideFiltro.Opcion != null)
                    if (insideFiltro.Opcion != "No") {
                        dataConTop = dataConTop.Where(p => p.Procedencia.ToLower().CompareTo(insideFiltro.Opcion.ToLower()) == 0).ToList();
                        dataConNoTop = dataConNoTop.Where(p => p.Procedencia.ToLower().CompareTo(insideFiltro.Opcion.ToLower()) == 0).ToList();
                    }
                if (insideFiltro.Departamento != null)
                {
                    if (insideFiltro.Departamento != "-1")
                    {
                        dataConTop = dataConTop.Where(p => p.Departamento.Contains(insideFiltro.Departamento)).ToList();
                        dataConNoTop = dataConNoTop.Where(p => p.Departamento.Contains(insideFiltro.Departamento)).ToList();
                    }

                }

                if (insideFiltro.Orden != null)
                {
                    if (insideFiltro.Orden.Contains("cantidad"))
                    {
                        dataConTop = dataConTop.OrderByDescending(x => x.Cantidad).Take(insideFiltro.Elementos).ToList();
                        dataConNoTop = dataConNoTop.OrderBy(x => x.Cantidad).Take(insideFiltro.Elementos).ToList();
                    }
                    else
                    {
                        dataConTop = dataConTop.OrderByDescending(x => x.TotalDolares).Take(insideFiltro.Elementos).ToList();
                        dataConNoTop = dataConNoTop.OrderBy(x => x.TotalDolares).Take(insideFiltro.Elementos).ToList();
                    }
                }
                else{
                    dataConTop = dataConTop.OrderByDescending(x => x.Cantidad).Take(insideFiltro.Elementos).ToList();
                    if(insideFiltro.tiendaId==0)
                        dataConNoTop = dataConNoTop.OrderBy(x => x.Cantidad).Take(insideFiltro.Elementos).ToList();
                }
                    

                return new {
                    Top10 = dataConTop,
                    noTop10 = dataConNoTop
                };
            }
            else return null;
        }

        public List<SpReporteFormasPago> ListadoTesoreriaFormasPagoPorTienda(TesoreriaViewModel tesoreriaViewModel)
        {
            int IdBuscar = -1;
            var dataFacturas = _SpRepo.ReporteFormasPagoRangFech(tesoreriaViewModel.IdTienda, tesoreriaViewModel.IdCaja, ((tesoreriaViewModel.IdTurno != null) ? int.Parse(tesoreriaViewModel.IdTurno) : 0), tesoreriaViewModel.FechaI, tesoreriaViewModel.FechaF);

            if (tesoreriaViewModel.NombreCajera != null && tesoreriaViewModel.NombreCajera != "")
            {
                IdBuscar = _Usuario.getIdUserbyName(tesoreriaViewModel.NombreCajera);
            }

            if (tesoreriaViewModel.FormaPagoId != null)
            {
                if (Int32.Parse(tesoreriaViewModel.FormaPagoId) > 0)
                    dataFacturas = dataFacturas.Where(df => df.FormaPagoId == Int32.Parse(tesoreriaViewModel.FormaPagoId)).ToList();
            }

            return dataFacturas;
        }

        /*public async Task<List<SpMostrarConsolidado>> GetDataTiendasAsync(string path, string fechaIni, string fechaFin)
        {
            List<SpMostrarConsolidado> Infotienda = new List<SpMostrarConsolidado>();
             try
             {
                 using (var client = new HttpClient())
                 {
                     client.BaseAddress = new Uri(path);
                     client.DefaultRequestHeaders.Clear();
                     client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                     string Res = await client.GetStringAsync("Inside/ConsolidadoTienda");
                     if (Res!=null)
                     {
                        // var EmpResponse = Res;//.Content.ReadAsStringAsync().Result;
                         Infotienda = JsonConvert.DeserializeObject<List<SpMostrarConsolidado>>(Res);
                     }

                     return Infotienda;
                 }
             }
             catch(Exception e)
             {
                 var m = e.Message;
                 return null;
             }
            return Infotienda;
        }*/

        /*public List<VentasTiendas> ProcesarVentasTiendas(string fechaIni, string fechaFin)
        {
            var tiendas = _TiendaRepo.GetTiendas();
            List<VentasTiendas> DataTiendas = new List<VentasTiendas>();

            foreach (var item in tiendas)
            {
                //var data = _SpRepo.MostrarConsolidado(fechaIni, fechaFin);
                Task<List<SpMostrarConsolidado>> data = GetDataTiendasAsync(tiendas[0].UrlApi, fechaIni, fechaFin);

                DataTiendas.Add(new VentasTiendas { ConsolidadoTienda = data });
            }

            return DataTiendas;
        }*/
    }
}
