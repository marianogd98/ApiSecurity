using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiPosRio.Models.DB
{
    public partial class ComboProducto
    {
        public int Id { get; set; }
        public int ComboId { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }

        public virtual Combo Combo { get; set; }
        public virtual Producto Producto { get; set; }
    }
}
