using LinkCrawler.Models;

namespace LinkCrawler.Utils.Clients
{
    public interface ISlackClient
    {
        string WebHookUrl { get; set; }
        string BotName { get; set; }
        string BotIcon { get; set; }
        string MessageFormat { get; set; }
        bool HasWebHookUrl { get; }
        void NotifySlack(IResponseModel responseModel);
    }
}
