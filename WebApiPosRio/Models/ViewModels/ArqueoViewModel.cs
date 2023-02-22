using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPosRio.Models.ViewModels
{
    public class ArqueoViewModel
    {
        public int TiendaId { get; set; }
        public int TurnoId { get; set; }
        public int UsuarioId { get; set; }
        public string Observacion { get; set; }
        public DateTime Fecha { get; set; }
        public int Estatus { get; set; }
        public List<DetallesArqueo> DetalleArqueo { get; set; }
        public List<DesgloseDolar> DesgloseEfectivoD { get; set; }
        public List<DesgloseDolar> DesgloseEfectivoE { get; set; }
        public List<DesglosePuntoExterno> DesglosePuntoExterno { get; set; }
    }

    public class DetallesArqueo
    {
        public int CajaId { get; set; }
        public string CodigoCaja { get; set; }
        public string Descripcion { get; set; }
        public double TotalFormaPago { get; set; }                    
        public double TotalPagoSegunCaja { get; set; }
        public int FormaPagoId { get; set; }
        public int Turno { get; set; }
    }

    public class DesgloseDolar
    {
        public int Denominacion { get; set; }
        public int Cantidad { get; set; }
        public double Monto { get; set; } 
        public int Moneda { get; set; }

    }

    public class DesglosePuntoExterno    {
        public string Banco { get; set; }
        public int BancoId { get; set; }
        public string Idbt { get; set; }
        public string Tipo { get; set; }
        public double Monto { get; set; }
    }

    public class DetalleArqueoRevisado
    {
        public double Monto { get; set; }
        public int FormaPagoId { get; set; }
        public int Tipo { get; set; }
    }

    public class ArqueoData
    {
        public int Id { get; set; }
        public string Observacion { get; set; }
        public DateTime Fecha { get; set; }
        public int Estatus { get; set; }
        public List<object> DetalleArqueo { get; set; }
    }

}
