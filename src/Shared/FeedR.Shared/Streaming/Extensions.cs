using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace FeedR.Shared.Streaming
{
    public static class Extensions
    {
        public static IServiceCollection AddStreaming(this IServiceCollection services)
        {
            services.AddSingleton<IStreamPublisher, DefaultPublisher>();
            services.AddSingleton<IStreamSubscriber, DefaultSubscriber>();

            return services;
        }
    }
}