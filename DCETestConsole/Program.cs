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
            var client = new RestClient();
            client.BaseUrl = new Uri("https://www.okcoin.com/api/v1");

            var request = new RestRequest();
            request.AddHeader("contentType", "application/x-www-form-urlencoded");
            request.Resource = "ticker.do?symbol=btc_usd";

            IRestResponse response = client.Execute(request);

            Console.WriteLine(response.Content);

            Console.ReadKey();
        }
    }
}
