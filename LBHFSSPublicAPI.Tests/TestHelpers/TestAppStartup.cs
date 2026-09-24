using System;
using LBHFSSPublicAPI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace LBHFSSPublicAPI.Tests.TestHelpers
{
    /// <summary>
    /// Runs the real <see cref="Startup"/> configuration, then optional test service replacements.
    /// </summary>
    public class TestAppStartup
    {
        public static Action<IServiceCollection> ConfigureTestServices { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            Startup.ConfigureServices(services);
            ConfigureTestServices?.Invoke(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Startup.Configure(app, env);
        }
    }
}
