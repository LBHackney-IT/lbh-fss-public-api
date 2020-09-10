using System;
using AutoFixture;
using Bogus;
using LBHFSSPublicAPI.V1.Domain;

namespace LBHFSSPublicAPI.Tests.TestHelpers
{
    public static class Random
    {
        private static Faker _faker = new Faker();                 // Good for single values
        private static Fixture _fixture = new Fixture();           // Good for complex objects

        public static int Id(int minimum = 0, int maximum = 10000)
        {
            return _faker.Random.Int(minimum, maximum);
        }

        public static T Create<T>()
        {
            return _fixture.Create<T>();
        }
    }
}
