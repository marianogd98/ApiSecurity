using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPosRio.Models.ViewModels
{
    public class PromocionDto
    {
        public int Tipo { get; set; }
        public string Titulo { get; set; }
        public double monto{ get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public IFormFile ProductsCvs { get; set; }

    }
}
