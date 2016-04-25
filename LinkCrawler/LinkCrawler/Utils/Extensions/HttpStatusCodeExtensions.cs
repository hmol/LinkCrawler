using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace LinkCrawler.Utils.Extensions
{
    public static class HttpStatusCodeExtensions
    {
        /// <summary>
        /// This method will determine if an HttpStatusCode represents a "success" or not
        /// based on the configuration string you pass in.
        /// You can pass literal codes like 100, 200, 404
        /// Or you can pass in simple patterns using "x"s as wildcards like: 1xx, xx4
        /// </summary>
        /// <param name="this">The HttpStatus code to use</param>
        /// <param name="configuredCodes">CSV of HttpStatus codes</param>
        /// <returns></returns>
        public static bool IsSuccess(this HttpStatusCode @this, string configuredCodes)
        {
            var codeCollection = configuredCodes
                .Split(new string[] {","}, StringSplitOptions.RemoveEmptyEntries)   // split into array
                .Select(c => c.Trim().ToLower())          // trim off any spaces, and make lowercase (this allows for "100,20X" and "100, 20x)"
                .ToList();

            var codeNumberAsString = ((int) @this).ToString();

            // test for simple exact matching
            if (codeCollection.Contains(codeNumberAsString))
                return true;

            // replace X's with regex single character wildcard
            var codeCollectionRegexs = codeCollection
                .Where(c => c.Contains("x"))
                .Select(c => c)
                .ToList();

            // if there aren't any codes with wildcards, just bail now
            if (!codeCollectionRegexs.Any())
                return false;

            return codeCollectionRegexs
                .Select(ToRegex)
                .Any(x => Regex.IsMatch(codeNumberAsString, x));
        }

        private static string ToRegex(string s)
        {
            return s.Replace("x", "[0123456789]");
        }
    }
}