using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPosRio.Models.DB;

namespace WebApiPosRio.Models.Repository
{
    public class ClienteRepository : RIOPOSContext, IClienteRepository
    {
        public int GetIdClienteByCedula(string Cedula)
        {
            var objCliente = (from c in Clientes
                              where (c.Rif.Contains(Cedula))
                              select new
                              {
                                  Id = c.Id
                              }).FirstOrDefault();

            return objCliente != null ? objCliente.Id : -1;
        }
    }
}
