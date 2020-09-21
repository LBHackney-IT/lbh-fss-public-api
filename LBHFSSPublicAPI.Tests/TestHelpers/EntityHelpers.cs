using AutoFixture;
using LBHFSSPublicAPI.V1.Infrastructure;

namespace LBHFSSPublicAPI.Tests.TestHelpers
{
    public static class EntityHelpers
    {
        public static Service CreateService()
        {
            var service = Randomm.Build<Service>()
                .With(s => s.Organization,
                    Randomm.Build<Organization>()
                        .With(o => o.ReviewerU, Randomm.Create<User>())
                        .Create())
                .With(s => s.Image, Randomm.Create<File>())
                .Create();
            return service;
        }
    }
}
