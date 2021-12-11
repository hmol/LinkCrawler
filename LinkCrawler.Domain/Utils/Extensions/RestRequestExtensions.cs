
namespace LinkCrawler.Domain.Utils.Extensions;

public static class RestRequestExtensions
{
    public static RestRequest SetHeader(this RestRequest restRequest, string name, string value)
    {
        restRequest.AddHeader(name, value);
        return restRequest;
    }
}
