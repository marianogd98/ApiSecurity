using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPosRio.Models.ViewModels
{
    public class AreaViewModel
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int TiendaId { get; set; }
    }

    public class AreaSelectViewModel
    {
        public int Value { get; set; }
        public string Text { get; set; }
    }
}
