using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPosRio.Models.DB;
using WebApiPosRio.Models.Response;
using WebApiPosRio.Models.ViewModels;
using X.PagedList;
namespace WebApiPosRio.Models.Repository
{
    public class WalletRepository : RIOPOSContext, IWalletRepository
    {
        private ISpRepository _SpRepo;
        public WalletRepository(ISpRepository spRepository)
        {
            _SpRepo = spRepository;
        }

        public List<SpListadoWallet> ListadoWallets(WalletViewModel walletViewModel)
        {

            List<SpListadoWallet> lstDev = _SpRepo.ListadoWallets(walletViewModel.FechaIni, walletViewModel.FechaFin, walletViewModel.Rif);

            return lstDev;
        }

        public ResponseData WalletInformacionGeneral()
        {
            try
            {
                var saldo = Wallets.Sum(w => w.Saldo);
                var promedio = Wallets.Average(w => w.Saldo);
                var saldoMayor = Wallets.Max(w => w.Saldo);
                var wallets = Wallets.Count(w => w.Id > 0);

                return new ResponseData()
                {
                    Success = 1,
                    Data = new { saldo, promedio, saldoMayor, wallets },
                };
            }
            catch (Exception e)
            {
                return new ResponseData()
                {
                    Success = 0,
                    Message = "Error interno en el servidor"
                };
            }
        }

        public ResponseData GetWallets(string rif, string nombre, string fechaI, string fechaF, int page = 1, int perPage = 10)
        {
            var wallets = Wallets
                           .Join(Clientes, w => w.ClienteId, c => c.Id,
                           (w, c) => new { Wallet = w, Cliente = c })
                           .Select(a => new
                           {
                               walletId = a.Wallet.Id,
                               cliente = a.Cliente.Nombre + " " + a.Cliente.Apellido,
                               rif = a.Cliente.Rif,
                               saldo = a.Wallet.Saldo,
                               direccion = a.Cliente.Direccion,
                               telefono = a.Cliente.Telefono,
                               ultimoUso = a.Wallet.UpdatedAt
                           })
                           .Where(
                                   d =>
                                   (string.IsNullOrEmpty(rif) || d.rif.Contains(rif)) &&
                                   (string.IsNullOrEmpty(fechaI) || Convert.ToDateTime(fechaI).Date <= d.ultimoUso.Date) && (string.IsNullOrEmpty(fechaF) || d.ultimoUso.Date <= Convert.ToDateTime(fechaF).Date) &&
                                   (string.IsNullOrEmpty(nombre)|| d.cliente.Contains(nombre))

                               ).ToList().OrderByDescending(d => d.ultimoUso).ToPagedList(page, perPage);

            var data = new
            {
                wallets = wallets,
                page = wallets.PageNumber,
                TotalItem = wallets.TotalItemCount
            };

            return new ResponseData()
            {
                //Success = (data == null) ? 0 : 1,
                //Message = (data == null) ? "No se encontro informacion" : "",
                Data = data
            };
        }

        public ResponseData WalletDeCliente(int id, string fechaI, string fechaF, int tiendaId = 0, int page = 1, int perPage = 10)
        {
            var movimientos = MovimientoWallets.Where(mw =>
                     mw.WalletId == id && (tiendaId == 0 || mw.TiendaId == tiendaId) &&
                     (string.IsNullOrEmpty(fechaI) || Convert.ToDateTime(fechaI).Date <= mw.CreatedAt.Date ) && (string.IsNullOrEmpty(fechaF) || mw.CreatedAt.Date <= Convert.ToDateTime(fechaF).Date))
                     .OrderByDescending(d => d.CreatedAt).ToPagedList(page, perPage);

            var data = (from w in Wallets
                        join c in Clientes on w.ClienteId equals c.Id
                        where w.Id == id
                        select new
                        {
                            cliente = $"{c.Nombre} {c.Apellido}",
                            saldo = w.Saldo,
                            movimientos = movimientos,
                            page = movimientos.PageNumber,
                            TotalItem = movimientos.TotalItemCount,
                        }).First();

            return new ResponseData()
            {
                Success = (data == null) ? 0 : 1,
                Message = (data == null) ? "No se encontro informacion" : "",
                Data = data,
            };
        }
    }
}
