using Application.Repositories;
using Infrastructure.RepoImpls;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Infrastructure
{
    public static class DepedencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IPlayerRepository, InMemoryPlayerRepository>();
            services.AddScoped<IWalletRepository, InMemoryWalletRepository>();
            return services;
        }
    }
}
