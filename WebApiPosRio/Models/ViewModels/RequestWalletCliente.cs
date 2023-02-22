namespace WebApiPosRio.Models.ViewModels
{
    public class RequestWalletCliente : Filters
    {
        public int Id { get; set; } = 0;
        public int TiendaId { get; set; } = 0;
    }
    public class RequestWallet : Filters
    {
        public string Rif { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;

    }

}
