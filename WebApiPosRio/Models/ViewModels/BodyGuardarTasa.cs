using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPosRio.Models.ViewModels
{
    public class BodyGuardarTasa
    {
        public string Fecha { get; set; }
        public double TasaUsd { get; set; }
        public double TasaEur { get; set; }
        public int UsuarioId { get; set; }
    }
}
