using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPosRio.Models.ViewModels
{   
    public class AccionViewModel
    {
        public int Id { get; set; }
        public int ModuloId { get; set; }
        public int RolId { get; set; }
        public string Descripcion { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
    }

    public class AccionConfViewModel
    {
        public int Id { get; set; }
        public int ModuloId { get; set; }
        public string Descripcion { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public bool Selected { get; set; }
        public bool Enable { get; set; }
    }

    public class AccionRolVieModel
    {
       public bool Enable { get; set; }
       public int IdAccion { get; set; }
    }
}
