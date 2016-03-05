using LinkCrawler.Models;
using LinkCrawler.Utils.Clients;
using LinkCrawler.Utils.Parsers;
using LinkCrawler.Utils.Settings;
using Moq;
using NUnit.Framework;

namespace LinkCrawler.Tests
{
    //Will test the dependencies LinkCrawler class has upon other classes
    [TestFixture]
    public class LinkCrawlerTests
    {
        public LinkCrawler LinkCrawler { get; set; }
        public Mock<IValidUrlParser> MockValidUrlParser { get; set; }
        public Mock<ISlackClient> MockSlackClient { get; set; }
        public Mock<ISettings> MockSettings { get; set; }

        [SetUp]
        public void SetUp()
        {
            MockValidUrlParser = new Mock<IValidUrlParser>();
            MockSlackClient = new Mock<ISlackClient>();
            MockSettings = new Mock<ISettings>();
            MockSettings.Setup(x => x.ValidUrlRegex).Returns(@"(^http[s]?:\/{2})|(^www)|(^\/{1,2})");
            MockSettings.Setup(x => x.BaseUrl).Returns("http://www.github.com");

            var parser = new ValidUrlParser(MockSettings.Object);

            LinkCrawler = new LinkCrawler(MockSlackClient.Object, parser, MockSettings.Object);
        }

        [Test]
        public void WriteOutputAndNotifySlack_SucessResponse_NotifySlack()
        {
            var mockResponseModel = new Mock<IResponseModel>();
            mockResponseModel.Setup(x => x.IsSucess).Returns(false);

            LinkCrawler.WriteOutputAndNotifySlack(mockResponseModel.Object);
            MockSlackClient.Verify(m => m.NotifySlack(mockResponseModel.Object));
        }

        [Test]
        public void CrawlForLinksInResponse_ResponseModelWithMarkup_ValidUrlFoundInMarkup()
        {
            var url = "http://www.github.com";
            var markup = string.Format("this is html document <a href='{0}'>a valid link</a>", url);
            var mockResponseModel = new Mock<IResponseModel>();
            mockResponseModel.Setup(x => x.Markup).Returns(markup);

            LinkCrawler.CrawlForLinksInResponse(mockResponseModel.Object);
            Assert.That(LinkCrawler.VisitedUrlList.Contains(url));
        }
    }
}
