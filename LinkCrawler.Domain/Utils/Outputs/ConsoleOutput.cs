
namespace LinkCrawler.Domain.Utils.Outputs;

public class ConsoleOutput : IOutput
{
    public async Task WriteErrorAsync(IResponseModel responseModel)
    {
        ConsoleHelper.WriteError(responseModel.ToString());
    }

    public void WriteInfo(IResponseModel responseModel)
    {
        WriteInfo(new string[] { responseModel.ToString() });
    }

    public void WriteInfo(String[] Info)
    {
        foreach (string line in Info) Console.WriteLine(line);
    }
}
