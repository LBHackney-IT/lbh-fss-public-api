using LBHFSSPublicAPI.V1.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace LBHFSSPublicAPI.Tests.TestHelpers
{
    public static class DbCleardown
    {
        public static void ClearAll(DatabaseContext context)
        {
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE taxonomies CASCADE");
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE users CASCADE");
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE files CASCADE");
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE organizations CASCADE");
        }
    }
}
