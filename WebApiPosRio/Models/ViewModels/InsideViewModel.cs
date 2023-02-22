using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPosRio.Models.DB;

namespace WebApiPosRio.Models.ViewModels
{
    public class InsideViewModel
    {
        public string FechaIni { get; set; }
        public string FechaFin { get; set; }
        public string Url { get; set; }
        public int TiendaId { get; set; }
    }

    public class InsideVentaViewModel
    {
        public int tiendaId { get; set; }
        public string FechaIni { get; set; }
        public string FechaFin { get; set; }
        public string Opcion { get; set; }
        public int Elementos { get; set; }
        public string Departamento { get; set; }
        public List<string> Procedencia { get; set; }
        public string Orden { get; set; }
    }

    public class Dataset
    {
        public string Label { get; set; }
        public List<int> Data { get; set; }
    }

    public class ChartData
    {
        public List<Dataset> Datasets { get; set; }
        public List<string> Labels { get; set; }
    }

    public class BigLineChart
    {
        public List<List<double>> AllData { get; set; }
        public int ActiveIndex { get; set; }
        public ChartData ChartData { get; set; }
        public object DataConsolidado { get; set; }
    }

    public  class Meses
    {
        public  string[] _Meses = { "Ene", "Feb", "Mar", "Abrl", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic" };
    }

    public class VentasTiendas
    {
        public Task<List<SpMostrarConsolidado>> ConsolidadoTienda { get; set; }
    }

    public class VentasMetasPorDia
    {
        public DateTime Fecha { get; set; }
        public SpListadoResumenVenta VentaTienda { get; set; }
        public Metum Metas { get; set; }
        public double Variacion { get; set; }
        public double PresupuestoAcum { get; set; }
        public double EjecutadoAcum{ get; set; }
        public double VariacionAcum { get; set; }
}

    public class VentasPorDia
    {
        public DateTime Fecha { get; set; }
        public SpListadoResumenVenta VentaTienda { get; set; }
        public Metum Metas { get; set; }
    }

    public class Goal
    {
        public string Name { get; set; } = "meta";
        public double Value { get; set; } //monto meta
        public int StrokeWidth { get; set; } = 5;//tamaño
        public string StrokeColor { get; set; } = "#775DD0";//color

    }

    public class GraficaVentas
    {
        public string X { get; set; } //Nombre tienda
        public double Y { get; set; } //monto tienda
        public List<Goal> Goals { get; set; } //metas
    }

}
