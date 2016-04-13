using System;
using System.IO;
using LinkCrawler.Models;
using LinkCrawler.Utils.Settings;

namespace LinkCrawler.Utils.Outputs
{
    public class CsvOutput : IOutput, IDisposable
    {
        private readonly StreamWriter _writer;
        private const string Delimiter = ",";

        public CsvOutput(ISettings settings)
        {
            if (!settings.CsvEnabled)
            {
                return;
            }

            var fileMode = settings.CsvOverwrite ? FileMode.Create : FileMode.Append;
            var file = new FileStream(settings.CsvFilePath, fileMode, FileAccess.Write);
            _writer = new StreamWriter(file);

            if (fileMode == FileMode.Create)
            {
                _writer.WriteLine("Code{0}Status{0}Url{0}Referer", Delimiter);
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
                Delimiter,
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
