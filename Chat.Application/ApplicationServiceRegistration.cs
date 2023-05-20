using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;
namespace Chat.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection CofigurationApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());   
            return services;
        }
    }
}
