using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeedR.Shared.Streaming;
using Microsoft.Extensions.DependencyInjection;

namespace FeedR.Shared.Redis.Streaming
{
    public static class Extensions
    {
        public static IServiceCollection AddRedisStreaming(this IServiceCollection services)
        {
            services.AddSingleton<IStreamPublisher, RedisStreamPublisher>();
            services.AddSingleton<IStreamSubscriber, RedisStreamSubscriber>();

            return services;
        }
    }
}