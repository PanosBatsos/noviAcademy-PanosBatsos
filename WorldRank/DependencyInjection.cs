using Application;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace WorldRank
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWorldRank(this IServiceCollection services)
        {
            services.AddLogging(builder =>
            {

                builder.ClearProviders();
                builder.SetMinimumLevel(LogLevel.Trace);
                builder.AddNLog();
            });

            services.AddApplication();
            services.addInfrastructure();
            return services;
        }
    }
}
