// using System;
// using System.Collections.Generic;
// using System.Linq;
// using AutoFixture;
// using Bogus;
// using LBHFSSPublicAPI.V1.Domain;
//
// namespace LBHFSSPublicAPI.Tests.TestHelpers
// {
//     public static class Randomm
//     {
//         private static Faker _faker = new Faker();                 // Good for single values
//         private static Fixture _fixture = new Fixture();           // Good for complex objects
//
//         public static int Id(int minimum = 0, int maximum = 10000)
//         {
//             return _faker.Random.Int(minimum, maximum);
//         }
//
//         public static string Text()
//         {
//             return string.Join(" ", _faker.Random.Words(5));
//         }
//
//         public static T Create<T>()
//         {
//             _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
//                 .ForEach(b => _fixture.Behaviors.Remove(b));
//             _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
//             return _fixture.Create<T>();
//         }
//
//         public static IEnumerable<T> CreateMany<T>(int quantity = 3)
//         {
//             _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
//                 .ForEach(b => _fixture.Behaviors.Remove(b));
//             _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
//             return _fixture.CreateMany<T>(quantity);
//         }
//     }
// }

using System;
using System.Collections.Generic;
using System.Reflection;
using AutoFixture;
using AutoFixture.Kernel;
using Bogus;
using LBHFSSPublicAPI.V1.Domain;
namespace LBHFSSPublicAPI.Tests.TestHelpers
{
    public static class Randomm
    {
        private static Faker _faker = new Faker();                 // Good for single values
        private static Fixture _fixture = new Fixture();           // Good for complex objects
        static Randomm()                                           // Gets called automatically by common language runtime (CLR)
        {
            IgnoreVirtualMethods();
        }
        public static int Id(int minimum = 0, int maximum = 10000)
        {
            return _faker.Random.Int(minimum, maximum);
        }
        public static string Text()
        {
            return string.Join(" ", _faker.Random.Words(5));
        }
        public static T Create<T>()
        {
            return _fixture.Create<T>();
        }
        public static IEnumerable<T> CreateMany<T>(int quantity = 3)
        {
            return _fixture.CreateMany<T>(quantity);
        }
        #region Options
        public static void IgnoreVirtualMethods()
        {
            _fixture.Customize(new IgnoreVirtualMembersCustomisation());
        }
        #endregion
    }
    #region Autofixture Customization
    public class IgnoreVirtualMembers : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            var pi = request as PropertyInfo;
            if (pi == null)
                return new NoSpecimen();
            if (pi.GetGetMethod().IsVirtual)
                return null;
            return new NoSpecimen();
        }
    }
    public class IgnoreVirtualMembersCustomisation : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customizations.Add(new IgnoreVirtualMembers());
        }
    }
    #endregion
}
