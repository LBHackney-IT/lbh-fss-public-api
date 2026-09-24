using System;
using Amazon.Lambda.AspNetCoreServer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace LBHFSSPublicAPI.Tests.TestHelpers
{
    /// <summary>
    /// Same hosting path as <see cref="LambdaEntryPoint"/>, with optional test service overrides
    /// applied after <see cref="Startup.ConfigureServices"/>.
    /// Uses <see cref="StartupMode.FirstRequest"/> so overrides can be registered before the host starts
    /// (the default constructor mode starts the host before the derived constructor body runs).
    /// </summary>
    public class TestableLambdaEntryPoint : APIGatewayProxyFunction
    {
        public TestableLambdaEntryPoint(Action<IServiceCollection> configureServices = null)
            : base(StartupMode.FirstRequest)
        {
            TestAppStartup.ConfigureTestServices = configureServices;
        }

        protected override void Init(IWebHostBuilder builder)
        {
            builder.UseStartup<TestAppStartup>();
        }
    }
}
