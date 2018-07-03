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

        [Test]
        public void Parse_UrlOnlyRelativePath_True()
        {
            var relativeUrl = "/relative/path";
            string parsed;
            var result = ValidUrlParser.Parse(relativeUrl, out parsed);
            Assert.That(result, Is.True);
            var validUrl = string.Format("{0}{1}", ValidUrlParser.BaseUrl, relativeUrl);

            Assert.That(parsed, Is.EqualTo(validUrl));
        }

        [Test]
        public void When_the_url_is_empty_then_it_is_not_a_valid_url()
        {
            Assert.IsFalse(ValidUrlParser.Parse("", out string _));
        }

        [Test]
        public void A_url_that_does_not_match_the_ValidUrlRegex_is_no_valid_url()
        {
            Assert.AreEqual(@"(^http[s]?:\/{2})|(^www)|(^\/{1,2})", new Settings().ValidUrlRegex,
                "This test is coded against this Regex. A change in the config could make it invalid.");
            Assert.IsFalse(ValidUrlParser.Parse("ftp://invalid.url", out string _));
        }

        [Test]
        public void An_absolute_http_url_will_be_parsed_and_not_be_changed()
        {
            Assert.IsTrue(ValidUrlParser.Parse("http://www.google.de", out string url));
            Assert.AreEqual("http://www.google.de", url);
        }

        [Test]
        public void A_relative_url_starting_with_a_slash_will_be_expanded_to_an_absolute_url()
        {
            Assert.AreEqual("https://github.com", new Settings().BaseUrl, "This test is coded against a configuration using this base url and will fail if the configuration is changed.");
            Assert.IsTrue(ValidUrlParser.Parse("/oml", out string url));
            Assert.AreEqual("https://github.com/oml", url);
        }

        [Test]
        public void An_url_without_a_scheme_will_get_http_prepended()
        {
            Assert.IsTrue(ValidUrlParser.Parse("//google.com", out string url));
            Assert.AreEqual("http://google.com", url);
        }

        [Test]
        public void A_relative_url_not_starting_with_a_slash_will_not_be_parsed()
        {
            Assert.IsFalse(ValidUrlParser.Parse("index.html", out string _));
        }
    }
}
