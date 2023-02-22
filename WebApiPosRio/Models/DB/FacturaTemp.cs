using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiPosRio.Models.DB
{
    public partial class FacturaTemp
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int CajaId { get; set; }
        public int CajeraId { get; set; }
        public int TiendaId { get; set; }
        public string Productos { get; set; }
        public DateTime Fecha { get; set; }
    }
}
