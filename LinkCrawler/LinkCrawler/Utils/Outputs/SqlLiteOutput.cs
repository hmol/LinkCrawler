using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Net;
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

            string createStatusCodesTable = String.Format("CREATE TABLE status_codes (code SMALLINT NOT NULL PRIMARY KEY, status VARCHAR({0}))", GetMaxStatusCodeWordLength());
            new SQLiteCommand(createStatusCodesTable, _sqLiteConnection).ExecuteNonQuery();

            foreach (var code in Enum.GetValues(typeof(HttpStatusCode)))
            {
                string insertStatusCode = String.Format("INSERT OR REPLACE INTO status_codes (code, status) VALUES({0}, '{1}')", (int)code, code);
                new SQLiteCommand(insertStatusCode, _sqLiteConnection).ExecuteNonQuery();
            }

            string createLinksTable = "CREATE TABLE links (code SMALLINT, url TEXT NOT NULL PRIMARY KEY, referrer TEXT)";
            new SQLiteCommand(createLinksTable, _sqLiteConnection).ExecuteNonQuery();
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
            string insertLink = String.Format("INSERT OR UPDATE INTO links (code, url, referrer) values ({0}, '{1}', '{2}')", responseModel.StatusCodeNumber, responseModel.RequestedUrl, responseModel.ReferrerUrl);
            new SQLiteCommand(insertLink, _sqLiteConnection).ExecuteNonQuery();
        }

        public void Dispose()
        {
            _sqLiteConnection.Close();
        }

        private static int GetMaxStatusCodeWordLength()
        {
            int result = 0;
            foreach (var code in Enum.GetValues(typeof(HttpStatusCode)))
            {
                if (result < code.ToString().Length)
                {
                    result = code.ToString().Length;
                }

            }
            return result;
        }
    }
}
