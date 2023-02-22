using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPosRio.Models.ViewModels
{   
    public class DevolucionViewModel
    {
        public int Id { get; set; }
        public int FacturaId { get; set; }
        public int Tipo { get; set; }
        public double Monto { get; set; }
        public int TurnoId { get; set; }
        public string SerialFiscal { get; set; }
        public int UsuarioId { get; set; }
        public string FormaPago { get; set; }
        public int TiendaId { get; set; }
        public int CajaId { get; set; }
        public DateTime Fecha { get; set; }
        public string NumeroDevolucion { get; set; }
    }

}
