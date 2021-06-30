using System;
using System.Collections.Generic;
using System.Text;

namespace TestTaskWeather
{
        public class Temperatura
        {
            public double day; //avg temp
            public double morn; //morning temp
        }

        public class WeatherNow
        {
            public string main;
            public string description;
        }


        public class DailyData
        {
            public Temperatura temp;
        }

        public class CurrentData
        {
           public double temp;
        }



        public class WeatherResponse
        {
            public DailyData[] daily;
            public string name;
        public CurrentData current;
      
        }
    
}
