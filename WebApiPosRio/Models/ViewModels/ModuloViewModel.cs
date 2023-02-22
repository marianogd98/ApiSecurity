using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPosRio.Models.ViewModels
{   
    public class ModuloViewModel
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
    }

    public class ModuloConfViewModel
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public bool Selected { get; set; }
        public bool Expand { get; set; }
        public List<AccionConfViewModel> Acciones { get; set; } = new List<AccionConfViewModel>();
    }
}
