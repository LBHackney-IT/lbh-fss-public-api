using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace LBHFSSPublicAPI.V1.Helpers
{
    public static class Extensions
    {
        public static bool AnyWord<TSource>(this IEnumerable<TSource> sourceEnumerable, string words)
        {
            if (sourceEnumerable == null)
            {
                throw new Exception("source missing");
            }

            if (words == null)
            {
                throw new Exception("words missing");
            }

            foreach (TSource element in sourceEnumerable)
            {
                if (element == null || element.GetType() != typeof(string))
                    continue;
                if (words.ContainsWord(element.ToString()))
                    return true;
            }

            return false;
        }

        public static bool ContainsWord(this string words, string searchForWord)
        {
            if (string.IsNullOrEmpty(words) || string.IsNullOrEmpty(searchForWord))
                return false;
            string pattern1 = @"(?i)(?<=\s|^|\W)" + searchForWord.Trim() + @"(?=\s|$|\W)";

            RegexOptions options = RegexOptions.Multiline;

            if (Regex.Matches(words, pattern1, options).Count > 0)
                return true;

            return false;
        }
    }
}
