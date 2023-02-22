using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebApiPosRio.Models.DB;
using WebApiPosRio.Models.ViewModels;

namespace WebApiPosRio.Models.Repository
{
    public class QqmRepository : RIOPOSContext, IQqmRepository
    {
        private readonly ISpRepository _SpRepo;
        public QqmRepository(ISpRepository spRepo)
        {
            _SpRepo = spRepo;
        }

        public Nivel RandomQuestions(QQM preguntas, int ind)
        {

            try
            {
                var itemQ = preguntas.Questions[ind];
                int newPos = Aleatorio(4);

                //colocola respuesta a cambiar
                string RTemp = itemQ.RS[newPos];

                itemQ.RS[newPos] = itemQ.RS[itemQ.RC];

                itemQ.RS[itemQ.RC] = RTemp;
                itemQ.RC = newPos;                
            }
            catch (Exception e)
            {
                var m = e.Message.ToString();
            }
            return preguntas.Questions[ind];
        }

        public List<SpMostrarQQM> GetQuestions()
        {
            return _SpRepo.GetQQM(0);//todos los Niveles
        }

        public List<SpListadoConstante> GetConfig()
        {
            return _SpRepo.GetConstantes();
        }

        public Partida ProcesarPreguntas()
        {            
            Partida Quien = new Partida();
            Quien.Questions = new List<QqmViewModel>();
            var lPreguntas = GetQuestions();
            var lconstantes = GetConfig();
            bool faciles = lconstantes[0].Valor==0?false:true;
            Quien.TimePerQuestions = lconstantes[1].Valor;
            Quien.Id = _SpRepo.SetGame().SorteoId;


            foreach (var item in lPreguntas)
            {
                if (item.PreguntaRespuesta != "null")
                {
                    var JsonPreguntas = JsonConvert.DeserializeObject<QQM>(item.PreguntaRespuesta);

                    if (faciles)
                    {
                        var pf = JsonPreguntas.Questions.Where(q => q.PE == faciles).ToList();
                        if (pf.Count > 0)
                            JsonPreguntas.Questions = pf;
                    }
                    var JsonPremios = JsonConvert.DeserializeObject<Premio>(item.Premio);
                    int al = Aleatorio(JsonPreguntas.Questions.Count);
                    var QuestionAl = RandomQuestions(JsonPreguntas, al);
                    Quien.Questions.Add(new QqmViewModel
                    {
                        /* P  = JsonPreguntas.Questions[al].P,
                         RS = JsonPreguntas.Questions[al].RS,
                         RC = JsonPreguntas.Questions[al].RC,
                         PE = JsonPreguntas.Questions[al].PE,
                         AV = JsonPreguntas.Questions[al].AV,*/
                        P  = QuestionAl.P,
                        RS = QuestionAl.RS,
                        RC = QuestionAl.RC,
                        PE = QuestionAl.PE,
                        AV = QuestionAl.AV,
                        Premios = JsonPremios,
                        Nivel = item.Nivel
                    });
                }
            }

            return Quien;
        }

        public int SaveQuestions(int Id, QQmForm qQmForm)
        {

            //var d = qQmForm.QqmForm.Questions.ForEach(q => q.RS.ForEach(rs => rs.ToUpper()) );

            SpMostrarQQM mostrarQQM = new SpMostrarQQM
            {
                Nivel = qQmForm.Nivel,
                PreguntaRespuesta = JsonConvert.SerializeObject(qQmForm.QqmForm)
            };
            return _SpRepo.SaveQuestionsQQM(Id, mostrarQQM);
        }

        public int SaveGifts(int Id, QQmForm qQmForm)
        {
            SpMostrarQQM mostrarQQM = new SpMostrarQQM
            {
                Nivel = qQmForm.Nivel,
                PreguntaRespuesta = JsonConvert.SerializeObject(qQmForm.QqmForm),
                Premio = JsonConvert.SerializeObject(qQmForm.Premio)
            };
            return _SpRepo.SaveGiftsQQM(Id, mostrarQQM);
        }

        public int Aleatorio(int cota)
        {
            Random rnd = new Random();
            // Obtiene un número natural (incluye el 0) aleatorio entre 0 e int.MaxValue
            //int numeroSinCotaArbitraria = rnd.Next();
            //Console.WriteLine("Número sin cotas: {0}", numeroSinCotaArbitraria);
            // Obtiene, en este ejemplo, un número natural (incluye el 0)
            // aleatorio entre 0 y 5.
            int numeroConCotaSuperior = rnd.Next(cota);
            //Console.WriteLine("Número entre 0 y 5: {0}", numeroConCotaSuperior);
            // Obtiene, en este ejemplo, un número natural (incluye el 0)
            // aleatorio entre 4 (inclusive ) y 10.
            //int numeroConDosCotas = rnd.Next(4, 10);
            //Console.WriteLine("Número entre 4 y 10: {0}", numeroConDosCotas);
            return numeroConCotaSuperior;
        }

        public List<QQmForm> LoadData()
        {
            List<SpMostrarQQM> listaQ = GetQuestions();
            List<QQmForm> Quien = new List<QQmForm>();

            try
            {
                foreach (var item in listaQ)
                {
                    string premios = (string)item.Premio == null ? "{\"ProductoPremio\":[]}" : item.Premio;
                    string preguntas = ((string)item.PreguntaRespuesta == null || item.PreguntaRespuesta=="null") ? "{\"Questions\":[]}" : item.PreguntaRespuesta;
                    Quien.Add(new QQmForm
                    {
                        Id = item.Id,
                        Created_at = item.Created_at,
                        Updated_at = item.Updated_at,
                        Nivel = item.Nivel,
                        QqmForm = JsonConvert.DeserializeObject<QQM>(preguntas),
                        IdPremio = item.IdPremio == null ? 0 : (int)item.IdPremio,
                        Premio = JsonConvert.DeserializeObject<Premio>(premios)
                    });
                }
            }
            catch (Exception e)
            {
                var m = e.Message.ToString();
            }

            return Quien;
        }

        public QQM GetJson()
        {
            QQM questions = new QQM();
            string url = @"http://172.50.3.15:8400/data/data.json";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                 questions = JsonConvert.DeserializeObject<QQM>(json);
            }

            return questions;
        }


        public int SaveGame(Game game)
        {
            try
            {
                return _SpRepo.SetGame(game.Partida.Id, game.Nivel, JsonConvert.SerializeObject(game.Partida.Questions)).SorteoId;
            }
            catch
            {
                return 0;
            }
        }

        public int SaveConfig(List<SpListadoConstante> spListadoConstantes)
        {
            try
            {
                int reg = 0;
                foreach (var item in spListadoConstantes)
                {
                    reg += _SpRepo.SetConstantes(item.Id, item.Valor);                    
                }
                return reg > 1 ? 1 : 0;
            }
            catch
            {
                return 0;
            }
        }
    }
}
