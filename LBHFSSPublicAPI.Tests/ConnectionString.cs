using System;

namespace LBHFSSPublicAPI.Tests
{
    public static class ConnectionString
    {
        private const string LocalDefault =
            "Host=127.0.0.1;Port=6543;Username=postgres;Password=mypassword;Database=testdb";

        public static string TestDatabase()
        {
            return Environment.GetEnvironmentVariable("CONNECTION_STRING") ?? LocalDefault;
        }
    }
}
