using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPosRio.Models.ViewModels
{
    public class UpdatePromocion
    {
        public int id { get; set; }
        public int tienda { get; set; }
        public string hora { get; set; }
    }
}
