
namespace WebApiPosRio.Models.ViewModels
{
    public class TiendaViewModel
    {
        public int Id { get; set; }
        public string CodigoTienda { get; set; }
        public string CodigoArea { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        public bool AplicaIva { get; set; }
        public string UrlApi { get; set; }
        public double Tasa { get; set; }
        public double DescuentoMax { get; set; }
    }

    public class TiendaSelectViewModel
    {
        public int Value { get; set; }
        public string CodigoTienda { get; set; }
        public string CodigoArea { get; set; }
        public string Text { get; set; }
    }
}
