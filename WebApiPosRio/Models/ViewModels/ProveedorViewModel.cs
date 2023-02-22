using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPosRio.Models.ViewModels
{
    public class ProveedorViewModel
    {
        public string Rif { get; set; }
        public string Nombre { get; set; }
    }

    public class SelectProveedorViewModel
    {
        public string Code { get; set; }
        public string Label { get; set; }
    }
}
