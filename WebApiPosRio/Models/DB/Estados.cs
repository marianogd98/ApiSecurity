using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiPosRio.Models.DB
{
    public partial class Estados
    {
        public int Id { get; set; }
        public string Accion { get; set; }
        public string TipoAccion { get; set; }
    }
}
