using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedR.Feeds.Weather.Services
{
    public class WeatherFeed : IWeatherFeed
    {
        public WeatherFeed()
        {
            
        }
        public IAsyncEnumerable<WeatherData> SubscribeAsync(string location)
        {
            throw new NotImplementedException();
        }
    }
}