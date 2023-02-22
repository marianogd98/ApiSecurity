using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiPosRio.Models.DB
{
    public partial class Donacion
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int ClienteId { get; set; }
        public int OrganizacionId { get; set; }
        public int Estatus { get; set; }
        public int CajaId { get; set; }
        public int TurnoId { get; set; }
        public int CajeraId { get; set; }
        public int FormaPagoId { get; set; }
        public double MontoDonado { get; set; }
        public double Tasa { get; set; }
        public int TiendaId { get; set; }
        public int? BancoAdquirienteId { get; set; }
        public int? Lote { get; set; }
        public string NumeroTransacion { get; set; }
        public string Nombre { get; set; }
        public string TipoTarjeta { get; set; }
        public string NroAutorizacion { get; set; }
        public string Vposdata { get; set; }
    }
}
