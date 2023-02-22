namespace WebApiPosRio.Models.Repository
{
    public interface IClienteRepository
    {
        int GetIdClienteByCedula(string Cedula);
    }
}