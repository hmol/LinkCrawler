using System.Net;

namespace LinkCrawler.Models
{
    public class ResponseItem
    {
        public string Markup { get; set; }
        public System.Net.HttpStatusCode StatusCode { get; set; }
        public int StatusCodeInteger { get { return (int)StatusCode; } }
        public bool IsSucess { get { return StatusCode == HttpStatusCode.OK; } }
        public string Url { get; set; }

        public ResponseItem(string url)
        {
            Url = url;
        }

        public override string ToString()
        {
            return string.Format("{0}    {1}    {2}", StatusCodeInteger, StatusCode, Url);
        }
    }
}
