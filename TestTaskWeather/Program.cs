using System;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace TestTaskWeather
{
    class Program
    {
        //необходимо выводить среднюю и утреннюю (morn) температуру 
        //за сегодняшний + предстоящие 5 дней
        //524901 - Москва; широта и долгота - 55.751244, 37.618423
        //использован запрос "OneCall Api" 

        public static async Task ConnectAsync()
        {
            WebRequest request = WebRequest.Create("https://api.openweathermap.org/data/2.5/onecall?lat=55.751244&lon=37.618423&exclude=minutely,hourly&units=metric&appid=5ca2a7dabd98b1f61c9c60548f01a55a");
            request.Method = "POST";
            WebResponse response = await request.GetResponseAsync();

            string answer = string.Empty;
            using (Stream s = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(s))
                {
                    answer = await reader.ReadToEndAsync();
                }
            }
            response.Close();


            WeatherResponse response_global = JsonConvert.DeserializeObject<WeatherResponse>(answer);
            Console.WriteLine("Прогноз температуры в Москве:");
            Console.WriteLine("Температура СЕЙЧАС = " + response_global.current.temp);
            Console.WriteLine();
            for (int i=1;i<6;i++)
            {
                Console.WriteLine("Температура утром за " + i + " предстоящий день недели = " + response_global.daily[i].temp.morn);
                Console.WriteLine("Температура средняя за " + i + " предстоящий день недели = " + response_global.daily[i].temp.day);
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            try
            {
                ConnectAsync().Wait();
                Console.WriteLine("Успешно");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                Console.Write("Город не найден или что-то пошло не так");
            }
            Console.ReadKey();
        }
    }
}
