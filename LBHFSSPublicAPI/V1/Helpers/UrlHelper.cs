using System.Web;

namespace LBHFSSPublicAPI.V1.Helpers
{
    public static class UrlHelper
    {
        public static string DecodeParams(string term)
        {
            while (true)
            {
                if (term.Contains('%'))
                {
                    term = HttpUtility.UrlDecode(term);
                    continue;
                }
                return term;
            }
        }
    }
}
