using System;
using System.IO;
using System.Net;

namespace LinkCrawler.Models
{
    public class LinkModel
    {
        public string Markup { get; set; }
        public System.Net.HttpStatusCode StatusCodeEnum { get; set; }
        public int StatusCode { get { return (int) StatusCodeEnum; } }
        public bool IsSucess { get { return StatusCodeEnum == HttpStatusCode.OK; } }
        public string Url;
        public LinkModel(string url)
        {
            Url = url;
        }

        public void SendRequestAndGetMarkup()
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(Url);
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    StatusCodeEnum = response.StatusCode;
                    if (response.StatusCode != HttpStatusCode.OK)
                        return;

                    using (var stream = response.GetResponseStream())
                    using (var reader = new StreamReader(stream))
                    {
                        Markup = reader.ReadToEnd();
                    }
                }
            }
            catch (WebException ex)
            {
                var errorStatus = (ex.Response as HttpWebResponse).StatusCode;
                StatusCodeEnum = errorStatus;
            }
        }

        public override string ToString()
        {
            return string.Format("{0}   {1}   {2}",Url, StatusCode, StatusCodeEnum);
        }
    }
}