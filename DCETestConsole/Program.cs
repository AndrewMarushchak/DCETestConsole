using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace DCETestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //var client = new RestClient();
            //client.BaseUrl = new Uri("https://www.okcoin.com/api/v1");

            //var request = new RestRequest();
            //request.AddHeader("contentType", "application/x-www-form-urlencoded");
            //request.Resource = "ticker.do?symbol=btc_usd";

            //IRestResponse response = client.Execute(request);

            //Console.WriteLine(response.Content);

            string url = "wss://real.okcoin.com:10440/websocket/okcoinapi";

            WebSocketManager socketManager = new WebSocketManager(url, new BuissnesProcessor());

            socketManager.Start();
            socketManager.Send("{'event':'addChannel','channel':'ok_sub_spotcny_btc_ticker'}");
            socketManager.Stop();

            Console.ReadKey();
        }
    }

    public class BuissnesProcessor : IWebSocketDataProcessor
    {
        public void ProcessData(string data)
        {
            Console.WriteLine(data);
        }
    }
}
