using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiPosRio.Models.DB
{
    public partial class Usuarios
    {
        public int IdUser { get; set; }
        public string Name { get; set; }
        public int IdDepartament { get; set; }
        public string Position { get; set; }
        public int Active { get; set; }

    }
}
