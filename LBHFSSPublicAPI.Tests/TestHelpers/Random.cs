using System;
using Bogus;

namespace LBHFSSPublicAPI.Tests.TestHelpers
{
    public static class Random
    {
        private readonly static Faker _faker = new Faker();

        public static int Id(int minimum = 1, int maximum = 10000)
        {
            return _faker.Random.Int(minimum, maximum);
        }
    }
}
