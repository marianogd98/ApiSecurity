using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiPosRio.Models.Response;
using WebApiPosRio.Models.ViewModels;

namespace WebApiPosRio.Models.Repository
{
    public interface IRolRepository
    {
        //DbSet<Rol> Rols { get; set; }

        //List<RolViewModel> GetRols();
        IEnumerable<RolViewModel> GetRols(int page, int perPage);
        Task<RolViewModel> GetIdRolAsync(int id);
        ResponseData AddRol(RolViewModel rolViewModel);
        ResponseData UpdateRol(RolViewModel rolViewModel, int Id);
        ResponseData DeleteRol(int Id);
    }
}