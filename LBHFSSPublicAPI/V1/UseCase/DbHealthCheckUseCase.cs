using System.Linq;
using LBHFSSPublicAPI.V1.Boundary;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace LBHFSSPublicAPI.V1.UseCase
{
    public class DbHealthCheckUseCase
    {
        private readonly HealthCheckService _healthCheckService;

        public DbHealthCheckUseCase(HealthCheckService healthCheckService)
        {
            _healthCheckService = healthCheckService;
        }

        public HealthCheckResponse Execute()
        {
            var result = _healthCheckService.CheckHealthAsync().Result;

            var success = result.Status == HealthStatus.Healthy;
            var message = string.Join(", ", result.Entries.Select(e => $"{e.Key}: {e.Value.Description}"));
            return new HealthCheckResponse(success, message);
        }
    }

}
