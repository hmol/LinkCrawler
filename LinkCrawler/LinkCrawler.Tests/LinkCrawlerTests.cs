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

            LinkCrawler = new LinkCrawler(MockSlackClient.Object, MockValidUrlParser.Object, MockSettings.Object);
        }

        [Test]
        public void WriteOutputAndNotifySlack_SucessResponse_NotifySlack()
        {
            var mockResponseModel = new Mock<IResponseModel>();
            mockResponseModel.Setup(x => x.IsSucess).Returns(false);

            LinkCrawler.WriteOutputAndNotifySlack(mockResponseModel.Object);
            MockSlackClient.Verify(m => m.NotifySlack(mockResponseModel.Object));
        }
    }
}
