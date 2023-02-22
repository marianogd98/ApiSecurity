using System.Collections.Generic;
using WebApiPosRio.Models.DB;
using WebApiPosRio.Models.ViewModels;

namespace WebApiPosRio.Models.Repository
{
    public interface IQqmRepository
    {
        int Aleatorio(int cota);
        List<QQmForm> LoadData();
        QQM GetJson();
        Partida ProcesarPreguntas();
        List<SpListadoConstante> GetConfig();
        int SaveConfig(List<SpListadoConstante> spListadoConstantes);
        int SaveQuestions(int Id, QQmForm qQmForm);
        int SaveGifts(int Id, QQmForm qQmForm);
        int SaveGame(Game game);        
    }
}