using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiPosRio.Models.DB
{
    public partial class Serial
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public string Serial1 { get; set; }
    }
}
