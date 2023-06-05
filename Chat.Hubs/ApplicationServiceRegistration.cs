using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Hubs
{
    public static class HubsServiceRegistration
    {
        public static IServiceCollection CofigurationHubsServices(this IServiceCollection services)
        {
            services.AddSignalR();
            return services;
        }
    }
}
