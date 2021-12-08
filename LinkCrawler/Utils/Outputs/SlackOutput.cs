using LinkCrawler.Models;
using LinkCrawler.Utils.Clients;

namespace LinkCrawler.Utils.Outputs
{
    public class SlackOutput : IOutput
    {
        private readonly ISlackClient _slackClient;

        public SlackOutput(ISlackClient slackClient)
        {
            _slackClient = slackClient;
        }

        public void WriteError(IResponseModel responseModel)
        {
            _slackClient.NotifySlack(responseModel);
        }

        public void WriteInfo(IResponseModel responseModel)
        {
            // Write nothing to Slack
        }

        public void WriteInfo(string[] Info)
        {
            // Write nothing to Slack
        }
    }
}
