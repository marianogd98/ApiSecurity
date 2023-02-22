using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebApiPosRio.Models.Repository
{
    public class ApiConectorRepository : IApiConectorRepository
    {

        public string GetUrlTienda(int TiendaId)
        {
            string url = "";

            switch (TiendaId)
            {
                case 1://jb
                    url = "http://172.17.0.14:8302/api/";//"https://localhost:44345/api/";//"http://jbaapp02:8302/api/";//
                    break;
                case 2://Traki
                    url = "http://172.18.0.14:8302/api/";//"http://trkapp01:8302/api/";//
                    break;
                case 3://playa el angel
                    url = "http://172.19.0.14:8302/api/";//"https://localhost:44345/api/";//"http://plyapp01:8302/api/";//
                    break;
                case 4://terranova
                    url = "http://172.54.0.14:8302/api/";//"http://trnapp01:8302/api/";//
                    break;
                case 5://31 Julio
                    url = "http://172.21.0.14:8302/api/";
                    break;
                case 6://JGO
                    url = "http://172.22.0.14:8302/api/";
                    break;
                case 7://cumana
                    url = "http://172.24.0.14:8302/api/";
                    break;
                case 8://maturin
                    url = "https://apiseguridadmat.riomarket.com/api/";
                    break;
                case 9://sambil
                    url = "http://172.23.0.14:8302/api/";
                    break;
                default://local
                    url = "http://insideapp01:8302/api/";//"https://localhost:44345/api/";//
                    break;
            }
            return url;
        }

        public string AjaxRequest(string URL, string Data="", string Token="", string Method = "GET")
        {
            string result = "";

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(URL);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";
            if (Token != "")
            {
                httpWebRequest.Headers.Add("Authorization", "Bearer " + Token);
            }

            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            catch (WebException e)
            {
                return e.Message.ToString();
            }

            return result;
        }

        public string CleanUrl(string urlData)
        {
            var l = urlData.Replace(':', '=');
            l = l.Replace(',', '&');

            string UrlLimpia = string.Join("", l.Split('{', '}', '"', '\''));

            return UrlLimpia;
        }
    }
}
