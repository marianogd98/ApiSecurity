using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiPosRio.Models.DB
{
    public partial class CodigoBarra
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public string CodigoBarra1 { get; set; }
        public bool? Activo { get; set; }
        public bool Predeterminado { get; set; }

        public virtual Producto Producto { get; set; }
    }
}
