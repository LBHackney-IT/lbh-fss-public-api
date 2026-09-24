using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace LBHFSSPublicAPI.Tests.TestHelpers
{
    public static class ServiceCollectionExtensions
    {
        public static void ReplaceSingleton<TService>(this IServiceCollection services, TService instance)
            where TService : class
        {
            foreach (var descriptor in services.Where(d => d.ServiceType == typeof(TService)).ToList())
            {
                services.Remove(descriptor);
            }

            services.AddSingleton(instance);
        }
    }
}
