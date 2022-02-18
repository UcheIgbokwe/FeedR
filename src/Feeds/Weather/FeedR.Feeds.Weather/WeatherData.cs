using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedR.Feeds.Weather
{
    internal record WeatherData(string Location, double Temperature, double Humidity, double Wind, string Condition);
}