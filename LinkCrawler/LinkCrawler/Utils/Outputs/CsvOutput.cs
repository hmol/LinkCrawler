using LinkCrawler.Models;
using LinkCrawler.Utils.Settings;
using System;
using System.IO;

namespace LinkCrawler.Utils.Outputs
{
    public class CsvOutput : IOutput, IDisposable
    {
        private readonly ISettings _settings;
        private TextWriter _writer;

        public CsvOutput(ISettings settings)
        {
            _settings = settings;
            Setup();
        }

        private void Setup()
        {
            var fileMode = _settings.CsvOverwrite ? FileMode.Create : FileMode.Append;
            var file = new FileStream(_settings.CsvFilePath, fileMode, FileAccess.Write);

            var streamWriter = new StreamWriter(file);
            _writer = TextWriter.Synchronized(streamWriter);

            if (fileMode == FileMode.Create)
            {
                _writer.WriteLine("Code{0}Status{0}Url{0}Referer", _settings.CsvDelimiter);
            }
        }

        public void WriteError(IResponseModel responseModel)
        {
            Write(responseModel);
        }

        public void WriteInfo(IResponseModel responseModel)
        {
            Write(responseModel);
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
}
