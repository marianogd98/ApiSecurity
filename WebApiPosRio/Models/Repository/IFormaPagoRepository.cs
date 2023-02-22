using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiPosRio.Models.Response;
using WebApiPosRio.Models.ViewModels;

namespace WebApiPosRio.Models.Repository
{
    public interface IFormaPagoRepository
    {
        ResponseData AddBanco(FormaPagoViewModel formaPagoViewModel);
        ResponseData DeleteFormaPago(int Id);
        List<FormaPagoViewModel> GetFormaPagos();
        Task<FormaPagoViewModel> GetIdRolAsync(int id);
        ResponseData UpdateBanco(FormaPagoViewModel formaPagoViewModel, int Id);
    }
}