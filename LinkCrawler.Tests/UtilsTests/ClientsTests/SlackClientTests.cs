using LinkCrawler.Utils.Clients;
using LinkCrawler.Utils.Settings;
using NUnit.Framework;

namespace LinkCrawler.Tests.UtilsTests.ClientsTests
{

    [TestFixture]
    public class SlackClientTests
    {

        //MethodName_StateUnderTest_ExpectedBehaviour
        [Test]
        public void SlackClient_InstantiationWithWebHookUrl_InstantiatedCorrectlyWithWebHookUrl()
        {
            MockSettings settings = new MockSettings(true);
            SlackClient sc = new SlackClient(settings);

            Assert.AreEqual(@"https://hooks.slack.com/services/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", sc.WebHookUrl);
            Assert.AreEqual("Homer Bot", sc.BotName);
            Assert.AreEqual(":homer:", sc.BotIcon);
            Assert.AreEqual("*Doh! There is a link not working* Url: {0} Statuscode: {1} The link is placed on this page: {2}", sc.MessageFormat);
            Assert.IsTrue(sc.HasWebHookUrl);

        }

        [Test]
        public void SlackClient_InstantiationWithoutWebHookUrl_InstantiatedCorrectlyWithoutWebHookUrl()
        {
            MockSettings settings = new MockSettings(false);
            SlackClient sc = new SlackClient(settings);

            Assert.AreEqual(string.Empty, sc.WebHookUrl);
            Assert.AreEqual("Homer Bot", sc.BotName);
            Assert.AreEqual(":homer:", sc.BotIcon);
            Assert.AreEqual("*Doh! There is a link not working* Url: {0} Statuscode: {1} The link is placed on this page: {2}", sc.MessageFormat);
            Assert.IsFalse(sc.HasWebHookUrl);

        }
    }

}
