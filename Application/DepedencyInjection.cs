using Application.Strategies;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public static class DepedencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddSingleton<IFundsStrategy, AddFundsStrategy>();
            services.AddSingleton<IFundsStrategy, SubtractFundsStrategy>();
            services.AddSingleton<IFundsStrategy, ForceSubtractStrategy>();
            return services;
        }
    }
}
