using LinkCrawler.Models;

namespace LinkCrawler.Utils.Outputs
{
    public interface IOutput
    {
        void WriteError(IResponseModel responseModel);
        void WriteInfo(IResponseModel responseModel);
    }
}
