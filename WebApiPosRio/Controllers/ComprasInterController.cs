using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApiPosRio.Models.Repository;
using WebApiPosRio.Models.Response;
using WebApiPosRio.Models.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiPosRio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ComprasInterController : ControllerBase
    {
        private readonly IComprasInternacionalRepository _ciRepository;
        private IWebHostEnvironment _hostingEnv;

        public ComprasInterController(IComprasInternacionalRepository ciRepository, IWebHostEnvironment webHostEnvironment)
        {
            _ciRepository = ciRepository;
            _hostingEnv = webHostEnvironment;
        }


        [HttpGet("[action]")]
        public IActionResult GetProveedores([FromQuery] ProveedorViewModel FiltroProveedor, int page = 1, int perPage = 10)
        {
            var data = _ciRepository.GetProveedores(FiltroProveedor);

            var paginacion = data.ToPagedList(page, perPage);
            var paginateData = new
            {
                Proveedores = paginacion,
                totalrow = paginacion.TotalItemCount
            };
            return (paginateData != null) ? Ok(paginateData) : BadRequest(paginateData);
        }

        [HttpGet("[action]")]
        public IActionResult GetProveedoresSelect()
        {
            var data = _ciRepository.GetProveedores(new ProveedorViewModel() { Nombre="", Rif="" }).Where(p =>p.N_activo==1).ToList();

            var listselect = data.Select(x => new 
            {
                   Code = x.C_codproveed,
                   Label = x.C_descripcio
            });
            return (listselect != null) ? Ok(listselect) : BadRequest(listselect);
        }

        [HttpGet("[action]")]
        public IActionResult GetFormaPago([FromQuery] string Descripcion)
        {
            var data = _ciRepository.GetFormaPago(Descripcion);

            return (data != null) ? Ok(data) : BadRequest(data);
        }

        [HttpGet("[action]")]
        public IActionResult GetAcreedor([FromQuery] string Descripcion)
        {
            var data = _ciRepository.GetAcreedor(Descripcion);

            return (data != null) ? Ok(data) : BadRequest(data);
        }

        [HttpGet("[action]")]
        public IActionResult GetPagos([FromQuery] FiltroPagoViewModel FiltroPago)
        {
            var data = _ciRepository.GetPagosProformas(FiltroPago);

            return (data != null) ? Ok(data) : BadRequest(data);
        }

        // GET: api/<ComprasInterController>
        [HttpGet]
        public IActionResult Get([FromQuery] FiltroProformaViewModel FiltroProforma, int page = 1, int perPage = 10)
        {
            var data = _ciRepository.GetProformas(FiltroProforma);

            if (data == null)
            {
                return BadRequest("No hay datos");
            }
                var paginacion = data.ToPagedList(page, perPage);

                var paginateData = new
                {
                    Proformas = paginacion,
                    totalrow = paginacion.TotalItemCount
                };
            
            return (paginateData != null) ? Ok(paginateData) : BadRequest(paginateData);
        }

        // GET api/<ComprasInterController>/5 Factura_Proforma
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var dataFactura = _ciRepository.Getfacturas(id);
            return (dataFactura != null) ? ((dataFactura.Count>0)?Ok(dataFactura): BadRequest(dataFactura)) : BadRequest(dataFactura);
        }

        // POST api/<ComprasInterController>
        [HttpPost("[action]")]
        public IActionResult SaveFactura([FromBody] FacturaProformaViewModel facturaProformaViewModel)
        {
            int resp = _ciRepository.SaveFacturas(facturaProformaViewModel);
            ResponseData rd = new ResponseData() { Data = null, Message = "", Success = 0 };
            if (resp >= 1)
            {
                rd.Message = "Factura Almacenada correctamente";
                rd.Success = 1;
                return Ok(rd);
            }
            else
            {
                rd.Message = "Ha ocurrido un error: en la Proforma a guardar";
                return BadRequest(rd);
            }
        }

        // POST api/<ComprasInterController>
        [HttpPost]
        public IActionResult Post([FromBody] ProformaViewModel proformaViewModel)
        {
            int resp = _ciRepository.SaveProforma(proformaViewModel);
            ResponseData rd = new ResponseData() { Data = null, Message = "", Success = 0 };
            if (resp >= 1)
            {
                rd.Message = "Proforma Almacenada correctamente";
                rd.Success = 1;
                return Ok(rd);
            }
            else
            {
                rd.Message = "Ha ocurrido un error: en la Proforma a guardar";
                return BadRequest(rd);
            }
        }

        // POST api/<ComprasInterController>
        [HttpPost("[action]")]
        public IActionResult SaveAcreedor([FromBody] AcreedorViewModel acreedorViewModel)
        {
            int resp = _ciRepository.SaveAcreedor(acreedorViewModel);
            ResponseData rd = new ResponseData() { Data = null, Message = "", Success = 0 };
            if (resp >= 1)
            {
                rd.Message = "Proforma Almacenada correctamente";
                rd.Success = 1;
                return Ok(rd);
            }
            else
            {
                rd.Message = "Ha ocurrido un error: en la Proforma a guardar";
                return BadRequest(rd);
            }
        }

        // PUT api/<ComprasInterController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProformaViewModel proformaViewModel)
        {
            int resp = _ciRepository.SaveProforma(proformaViewModel);
            ResponseData rd = new ResponseData() { Data = null, Message = "", Success = 0 };
            if (resp >= 1)
            {
                rd.Message = "Proforma Almacenada correctamente";
                rd.Success = 1;
                return Ok(rd);
            }
            else
            {
                rd.Message = "Ha ocurrido un error: en la Proforma a guardar";
                return BadRequest(rd);
            }
        }

        // DELETE api/<ComprasInterController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            int resp = _ciRepository.AnularProforma(id);
            ResponseData rd = new ResponseData() { Data = null, Message = "", Success = 0 };
            if (resp >= 1)
            {
                rd.Message = "Proforma Anulada correctamente";
                rd.Success = 1;
                return Ok(rd);
            }
            else
            {
                rd.Message = "Ha ocurrido un error: en la Proforma: "+id;
                return BadRequest(rd);
            }
        }

        [HttpPost("soportes/upload")]
        public async Task<IActionResult> UploadSoporte(IFormFile file)
        {
            if (file == null) return BadRequest(new
            {
                status = 0,
                message = "Debe subir un archivo"
            });

            if (Path.GetExtension(file.FileName).ToLower() == ".jpg" || Path.GetExtension(file.FileName).ToLower() == ".jpeg" || Path.GetExtension(file.FileName).ToLower() == ".pdf")
            {
                try
                {
                    string contentRootPath = _hostingEnv.ContentRootPath;

                    string pathToFiles = Path.Combine(contentRootPath, "images\\compras-internacionales\\");
                    var path = Path.Combine(pathToFiles, $"{DateTime.Now.ToString("ddMMyyyyfff")}{Path.GetExtension(file.FileName)}");
                    var stream = new FileStream(path, FileMode.Create);
                    await file.CopyToAsync(stream);
                    return Ok(new
                    {
                        status = 1,
                        message = "Archivo cargó con exito"
                    });
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return BadRequest(new
                {
                    status = 0,
                    message = "Las imágenes deben estar en formato JPG o JPEG"
                });
            }


        }
    }
}
