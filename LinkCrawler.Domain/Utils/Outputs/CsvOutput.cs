
namespace LinkCrawler.Domain.Utils.Outputs;

public class CsvOutput : IOutput, IDisposable
{
    private readonly ISettings _settings;
    public TextWriter _writer;

    public CsvOutput(ISettings settings)
    {
        _settings = settings;

        var fileMode = _settings.CsvOverwrite ? FileMode.Create : FileMode.Append;
        var file = new FileStream(_settings.CsvFilePath, fileMode, FileAccess.Write);
        var streamWriter = new StreamWriter(file) { AutoFlush = true };
        _writer = TextWriter.Synchronized(streamWriter);
        if (fileMode == FileMode.Create)
        {
            _writer.WriteLine("Code{0}Status{0}Url{0}Referer", _settings.CsvDelimiter);
        }
    }

    public async Task WriteErrorAsync(IResponseModel responseModel)
    {
        Write(responseModel);
    }

    public void WriteInfo(IResponseModel responseModel)
    {
        Write(responseModel);
    }

    public void WriteInfo(String[] Info)
    {
        // Do nothing - string info is only for console
    }

    private void Write(IResponseModel responseModel)
    {
        _writer?.WriteLine("{1}{0}{2}{0}{3}{0}{4}",
            _settings.CsvDelimiter,
            responseModel.StatusCodeNumber,
            responseModel.StatusCode,
            responseModel.RequestedUrl,
            responseModel.ReferrerUrl);
    }

    public void Dispose()
    {
        _writer?.Close();
        _writer?.Dispose();
    }
}
