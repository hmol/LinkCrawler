using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkCrawler.Models;
using LinkCrawler.Utils.Settings;

namespace LinkCrawler.Utils.Outputs
{
    class SqlLiteOutput : IOutput, IDisposable
    {
        private readonly ISettings _settings;
        private SQLiteConnection _sqLiteConnection;

        public SqlLiteOutput(ISettings settings)
        {
            _settings = settings;
            SetUp();
        }

        private void SetUp()
        {
            SQLiteConnection.CreateFile(_settings.SqlLiteFilePath);

            _sqLiteConnection = new SQLiteConnection(String.Format("Data Source={0}Version=3;", _settings.SqlLiteFilePath));
            string sql = "CREATE TABLE links (code SMALLINT, status VARCHAR(27), url TEXT NOT NULL UNIQUE, referrer TEXT)";
            SQLiteCommand command = new SQLiteCommand(sql, _sqLiteConnection);
            command.ExecuteNonQuery();
        }

        public void WriteError(IResponseModel responseModel)
        {
            Write(responseModel);
        }

        public void WriteInfo(IResponseModel responseModel)
        {
            Write(responseModel);
        }

        public void WriteInfo(string[] Info)
        {

        }

        public void Write(IResponseModel responseModel)
        {
            string sql = String.Format("insert into links (code, status, url, referrer) values ({0}, '{1}', '{2}', '{3}')", responseModel.StatusCodeNumber, responseModel.StatusCode, responseModel.RequestedUrl, responseModel.ReferrerUrl);
            SQLiteCommand command = new SQLiteCommand(sql, _sqLiteConnection);
            command.ExecuteNonQuery();
        }

        public void Dispose()
        {
            _sqLiteConnection.Close();
        }
    }
}
