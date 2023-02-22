namespace WebApiPosRio.Models.Repository
{
    public interface IApiConectorRepository
    {
        string GetUrlTienda(int TiendaId);
        string AjaxRequest(string URL, string Data = "", string Token = "", string Method = "GET");
        string CleanUrl(string urlData);
    }
}