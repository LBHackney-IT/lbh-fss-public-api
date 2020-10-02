using System.Text.RegularExpressions;
using System.Web;

namespace LBHFSSPublicAPI.V1.Helpers
{
    public static class UrlHelper
    {
        public static string DecodeParams(string term)
        {
            while (true)
            {
                var regex = new Regex(@"%[a-fA-F0-9a-fA-F]{2}");
                Match m = regex.Match(term);
                if (m.Success)
                {
                    term = HttpUtility.UrlDecode(term);
                    continue;
                }
                return term;
            }
        }
    }
}
