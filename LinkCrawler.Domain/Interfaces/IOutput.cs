
namespace LinkCrawler.Domain.Interfaces;

public interface IOutput
{
    void WriteError(IResponseModel responseModel);
    void WriteInfo(IResponseModel responseModel);
    void WriteInfo(string[] InfoString);
}
