using Chat.Hubs.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
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
            services.AddHttpContextAccessor();
            services.AddSignalR();
            return services;
        }

       /* public static IApplicationBuilder MapSignalR(
         this IApplicationBuilder builder)
        {
            builder.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatHub>("/chatHub");
            });
            return builder;
        }*/
    }
}
