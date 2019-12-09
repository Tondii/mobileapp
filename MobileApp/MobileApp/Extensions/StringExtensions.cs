using System.Collections.Generic;
using System.Linq;

namespace MobileApp.Extensions
{
    internal static class StringExtensions
    {
        public static bool ContainsAny(this string text, IEnumerable<string> words)
        {
            return words.Any(text.Contains);
        }
    }
}
