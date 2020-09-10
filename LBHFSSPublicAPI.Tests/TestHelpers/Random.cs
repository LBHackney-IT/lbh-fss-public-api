using System;
using System.Collections.Generic;
using AutoFixture;
using Bogus;
using LBHFSSPublicAPI.V1.Domain;

namespace LBHFSSPublicAPI.Tests.TestHelpers
{
    public static class Randomm
    {
        private static Faker _faker = new Faker();                 // Good for single values
        private static Fixture _fixture = new Fixture();           // Good for complex objects

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
    }
}
