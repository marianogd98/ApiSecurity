using System.Collections.Generic;
using WebApiPosRio.Models.DB;
using WebApiPosRio.Models.ViewModels;
using WebApiPosRio.Models.Response;
namespace WebApiPosRio.Models.Repository
{
    public interface IWalletRepository
    {
        List<SpListadoWallet> ListadoWallets(WalletViewModel walletViewModel);
        ResponseData WalletInformacionGeneral();
        ResponseData GetWallets(string rif, string nombre, string fechaI, string fechaF, int page = 1 , int perPage = 10);
        ResponseData WalletDeCliente(int id, string fechaI, string fechaF, int tiendaId = 0, int page = 1 , int perPage = 10);
    }
}