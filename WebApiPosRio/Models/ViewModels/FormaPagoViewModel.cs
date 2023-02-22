using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPosRio.Models.ViewModels
{   
    public class FormaPagoViewModel
    {
        public int Id { get; set; }
        public string CodigoMoneda { get; set; }
        public string Descripcion { get; set; }
        public int Orden { get; set; }
        //public bool? Estatus { get; set; }
    }

    public class FormaPagoConfViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        //public bool? Estatus { get; set; }
    }
}
