using LinkCrawler.Utils.Parsers;
using LinkCrawler.Utils.Settings;
using NUnit.Framework;

namespace LinkCrawler.Tests.UtilsTests.HelpersTests
{
    [TestFixture]
    public class ValidUrlParserTests
    {
        public ValidUrlParser ValidUrlParser { get; set; }
        [SetUp]
        public void SetUp()
        {
            ValidUrlParser = new ValidUrlParser(new Settings());
        }

        [Test]
        public void Parse_CompleteValidUrl_True()
        {
            var url = "http://www.github.com";
            string parsed;
            var result = ValidUrlParser.Parse(url, out parsed);
            Assert.That(result, Is.True);
            Assert.That(parsed, Is.EqualTo(url));
        }

        [Test]
        public void Parse_UrlNoScheme_True()
        {
            var url = "//www.github.com";
            string parsed;
            var result = ValidUrlParser.Parse(url, out parsed);
            Assert.That(result, Is.True);
            var validUrl = "http:" + url;
            Assert.That(parsed, Is.EqualTo(validUrl));
        }
    }
}
