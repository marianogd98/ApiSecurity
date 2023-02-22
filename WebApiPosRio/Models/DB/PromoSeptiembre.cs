using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiPosRio.Models.DB
{
    public partial class PromoSeptiembre
    {
        public int Id { get; set; }
        public DateTime? Fecha { get; set; }
        public int TiendaId { get; set; }
        public string Hora { get; set; }
        public bool Estatus { get; set; }
        public int CajaId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
