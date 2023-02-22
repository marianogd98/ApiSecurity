using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using System;
using System.Linq;
using System.Collections.Generic;
using WebApiPosRio.Models.DB;
using WebApiPosRio.Models.Repository;
using WebApiPosRio.Models.ViewModels;
using WebApiPosRio.Models.Response;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiPosRio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WalletController : ControllerBase
    {
        private readonly IWalletRepository _devolWall;
        public WalletController(IWalletRepository walletRepository)
        {
            _devolWall = walletRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get([FromQuery] WalletViewModel walletViewModel, int page = 1, int perPage = 10)
        {
            List<SpListadoWallet> value = _devolWall.ListadoWallets(walletViewModel);

            if (value != null)
            {
                if (walletViewModel.NombreCliente != null)
                {
                    value = value.Where(w => w.NombreCliente.Contains(walletViewModel.NombreCliente, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                if (walletViewModel.Estatus == 0)
                {
                    value = value.Where(w => w.Pago > 0).ToList();
                }

                if (walletViewModel.Estatus == 1)
                {
                    value = value.Where(w => (w.Dep * -1) > 0).ToList();
                }

                if (walletViewModel.Estatus == 2)
                {
                    value = value.Where(w => w.Saldo > 0).ToList();
                }

                var paginacion = value.ToPagedList(page, perPage);
                var paginateData = new
                {
                    Wallets = paginacion,
                    totalrow = paginacion.TotalItemCount
                };
                return (paginateData != null) ? Ok(paginateData) : BadRequest(paginateData);
            }
            else
            {
                return BadRequest("No hay datos");
            }
        }

        [HttpGet("info")]
        [AllowAnonymous]
        public IActionResult WalletInformacionGeneral()
        {
            var data = _devolWall.WalletInformacionGeneral();

            return data != null ? Ok(data) : BadRequest(data);
        }
        [HttpGet("cliente")]
        [AllowAnonymous]
        public IActionResult WalletCliente([FromQuery] RequestWalletCliente values)
        {
            try
            {
                if ( (values.FechaI != "" && values.FechaF != "") && Convert.ToDateTime(values.FechaI) > Convert.ToDateTime(values.FechaF))
                    return BadRequest(new ResponseData() { Success = 0, Message = "Ingrese un formato de fechas validos" });
                
                if (values.Id == 0)
                    return BadRequest(new ResponseData() { Success = 0, Message = "Envie identificador de wallet" });

                var data = _devolWall.WalletDeCliente(values.Id, values.FechaI, values.FechaF, values.TiendaId ,values.Page, values.PerPage);
                return data != null ? Ok(data) : BadRequest(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseData() { Success = 0, Message = "Error inesperado en el servidor" });
            }
        }

        [HttpGet("listado")]
        [AllowAnonymous]
        public IActionResult MovimientosWallet([FromQuery] RequestWallet values)
        {
            try
            {
                //if (Convert.ToDateTime(values.FechaI) > Convert.ToDateTime(values.FechaF))
                //    return BadRequest(new ResponseData() { Success = 0, Message = "Ingrese un formato de fechas validos" });

                var data = _devolWall.GetWallets(values.Rif, values.Nombre, values.FechaI, values.FechaF, values.Page, values.PerPage);
                return data != null ? Ok(data) : BadRequest(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseData() { Success = 0, Message = "Error inesperado en el servidor" });
            }
        }



        // POST api/<WalletController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<WalletController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<WalletController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
