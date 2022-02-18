using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedR.Feeds.Weather.Services
{
    public interface IWeatherFeed
    {
        IAsyncEnumerable<WeatherData> SubscribeAsync(string location);
    }
}