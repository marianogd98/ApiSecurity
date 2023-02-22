using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiPosRio.Models.DB
{
    public partial class CuentasBancaria
    {
        public CuentasBancaria()
        {
            DesglosePuntos = new HashSet<DesglosePunto>();
        }

        public int Id { get; set; }
        public int BancoId { get; set; }
        public string NumeroCuenta { get; set; }
        public int TiendaId { get; set; }

        public virtual ICollection<DesglosePunto> DesglosePuntos { get; set; }
    }
}
