using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPosRio.Models.ViewModels
{   
    public class RolViewModel
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public bool? Activo { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
    }

    public class RolConfViewModel
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public bool? Activo { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public bool Selected { get; set; }
    }
}
