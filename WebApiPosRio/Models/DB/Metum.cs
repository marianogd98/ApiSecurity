using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiPosRio.Models.DB
{
    public partial class Metum
    {
        public int Id { get; set; }
        public int TiendaId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime? Fin { get; set; }
        public double Monto { get; set; }
        public int Estatus { get; set; }
    }
}
