using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace FeedR.Shared.Serialization
{
    public static partial class Extensions
    {
        public static IServiceCollection AddSerialization(this IServiceCollection services)
        {
            services.AddSingleton<ISerializer, SystemTextJsonSerializer>();

            return services;
        }
    }
}