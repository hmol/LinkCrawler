using AutoFixture;
using NUnit.Framework;
using LinkCrawler.Utils.Outputs;
using AutoFixture.AutoMoq;
using LinkCrawler.Utils.Clients;
using LinkCrawler.Models;
using Moq;

namespace LinkCrawler.Tests.UtilsTests.OutputsTests
{
    [TestFixture]
    public class SlackOutputTests
    {
        private Fixture _fixture;

        [SetUp]
        public void SetUup()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());
        }

        [Test]
        public void WriteError()
        {
            
            var slackClient = _fixture.Freeze<Mock<ISlackClient>>();
            var slackOutput = _fixture.Freeze<SlackOutput>();
            var responseModel = _fixture.Create<IResponseModel>();

            slackClient.Setup(x => x.NotifySlack(responseModel));

            slackOutput.WriteError(responseModel);

            slackClient.VerifyAll();

        }
    }
}
