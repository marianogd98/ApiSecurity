using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiPosRio.Models.DB
{
    public partial class DetallesxTurnos
    {
        public int Id { get; set; }
        public int IdTurno { get; set; }
        public string Descripcion { get; set; }

    }
}
