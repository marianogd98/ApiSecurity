using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPosRio.Models.ViewModels
{   
    public class BancoViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool? Estatus { get; set; }
        //public string CreatedAt { get; set; }
        //public string UpdatedAt { get; set; }
    }

    public class BancoConfViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool? Estatus { get; set; }
        /*public bool? Activo { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public bool Selected { get; set; }*/
    }
}
