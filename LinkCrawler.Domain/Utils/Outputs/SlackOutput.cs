
namespace LinkCrawler.Domain.Utils.Outputs;

public class SlackOutput : IOutput
{
    private readonly ISlackClient _slackClient;

    public SlackOutput(ISlackClient slackClient)
    {
        _slackClient = slackClient;
    }

    public async Task WriteErrorAsync(IResponseModel responseModel)
    {
        await _slackClient.NotifySlackAsync(responseModel);
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
