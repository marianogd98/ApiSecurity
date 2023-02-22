using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiPosRio.Models.Response;
using WebApiPosRio.Models.ViewModels;

namespace WebApiPosRio.Models.Repository
{
    public interface IBancoRepository
    {
        ResponseData AddBanco(BancoViewModel bancoViewModel);
        ResponseData DeleteBanco(int Id);
        List<BancoViewModel> GetBancos();
        Task<BancoViewModel> GetIdRolAsync(int id);
        ResponseData UpdateBanco(BancoViewModel bancoViewModel, int Id);
    }
}