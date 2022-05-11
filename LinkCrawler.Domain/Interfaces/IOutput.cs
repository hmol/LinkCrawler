
namespace LinkCrawler.Domain.Interfaces;

public interface IOutput
{
    Task WriteErrorAsync(IResponseModel responseModel);
    void WriteInfo(IResponseModel responseModel);
    void WriteInfo(string[] InfoString);
}
