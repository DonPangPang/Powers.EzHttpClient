using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Powers.EzHttpClient.Extensions
{
    public static class EzHttpClientExtensions
    {
        public static IServiceCollection AddEzHttpClient(this IServiceCollection services)
        {
            services.AddScoped<IHttpClientFactory>();

            return services;
        }
    }
}
