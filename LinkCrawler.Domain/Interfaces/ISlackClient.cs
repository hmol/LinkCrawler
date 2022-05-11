﻿
namespace LinkCrawler.Domain.Interfaces;

public interface ISlackClient
{
    string WebHookUrl { get; set; }
    string BotName { get; set; }
    string BotIcon { get; set; }
    string MessageFormat { get; set; }
    bool HasWebHookUrl { get; }
    Task NotifySlackAsync(IResponseModel responseModel);
}
