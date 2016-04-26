using System.Net;
using LinkCrawler.Utils.Extensions;
using NUnit.Framework;

namespace LinkCrawler.Tests.UtilsTests.ExtensionsTests
{
    [TestFixture]
    public class HttpStatusCodeExtensionsTests
    {
        [TestCase(HttpStatusCode.OK,"200", true, Description = "Simple case of exact match")]
        [TestCase(HttpStatusCode.OK,"404", false, Description = "Simple case of no exact match")]
        [TestCase(HttpStatusCode.OK,"200,404", true, Description = "More complex: two codes in config")]
        [TestCase(HttpStatusCode.OK,"200, 404", true, Description = "Space after comma is okay")]
        [TestCase(HttpStatusCode.OK,"2xx", true, Description = "Use wildcards in code number")]
        [TestCase(HttpStatusCode.OK,"2Xx", true, Description = "X wildcard is not case sensitive")]
        [TestCase(HttpStatusCode.NotFound ,"xX4", true, Description = "Wildcard can be used for any digit")]
        [TestCase(HttpStatusCode.OK ,"2xx,xX0", true, Description = "Multiple wildcard codes allowed")]
        public void Will_match_a_single_code_exactly(HttpStatusCode givenCode, string givenConfig, bool expectedOutcome)
        {
            var result = givenCode.IsSuccess(givenConfig);

            Assert.That(result, Is.EqualTo(expectedOutcome));
        }
    }
}