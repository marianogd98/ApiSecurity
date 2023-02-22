using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPosRio.Models.ViewModels
{
    public class QqmViewModel
    {
        public int Nivel { get; set; }
        public Premio Premios { get; set; }
        public string P { get; set; }
        public List<string> RS { get; set; }
        public int RC { get; set; }
        public bool PE { get; set; }
        public bool AV { get; set; }
    }

    public class Partida
    {
        public int Id { get; set; }
        public int TimePerQuestions { get; set; } 
        public List<QqmViewModel> Questions { get; set; }
    }

    public class Game
    {
        public int Nivel { get; set; }
        public Partida Partida { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Nivel
    {
        public string P { get; set; }
        public List<string> RS { get; set; }
        public int RC { get; set; }
        public bool PE { get; set; }
        public bool AV { get; set; }
    }

    public class QQM
    {
        public List<Nivel> Questions { get; set; }
    }

    public class QQmForm
    {
        public int Id { get; set; }
        public int Nivel { get; set; }
        //public string PreguntaRespuesta { get; set; }
        public string Created_at { get; set; }
        public string Updated_at { get; set; }       
        public QQM QqmForm { get; set; }
        public int IdPremio { get; set; }
        public Premio Premio { get; set; }
    }

    public class ProductoPremio
    {
        public string Id { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
    }

    public class Premio
    {
        public List<ProductoPremio> ProductoPremio { get; set; }
    }

}
