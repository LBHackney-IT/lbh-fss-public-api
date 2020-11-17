using System;
using System.Collections.Generic;

namespace LBHFSSPublicAPI.Tests.TestHelpers
{
    public static class Utility
    {
        public static string SuperSetOfString(string text)
        {
            var surroundText = Randomm.Word();
            var midpoint = (int) Math.Ceiling((decimal) surroundText.Length / 2);
            return surroundText.Insert(midpoint, text);
        }

        public static void AddMany<T>(this List<T> list, params T[] elements)
        {
            list.AddRange(elements);
        }
    }
}
