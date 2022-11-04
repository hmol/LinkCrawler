using LinkCrawler.Utils.Extensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkCrawler.Tests.UtilsTests.ExtensionsTests
{
    [TestFixture]
    public class RegexExtensionsTest
    {
        public void IsNotMatch_Should_Return_False()
        {
            
            string regex = "(^http[s]?:\\/{2})|(^www)|(^\\/{1,2})";
            string url = "website.com:///podcast/";
            bool expression = RegexExtensions.IsNotMatch(new System.Text.RegularExpressions.Regex(regex), url);
            Assert.IsFalse(expression);
        }
    }
}
