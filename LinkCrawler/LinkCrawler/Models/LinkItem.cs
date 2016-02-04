using System;
using System.IO;
using System.Net;

namespace LinkCrawler.Models
{
    public class LinkItem
    {
        public string Url;
        public LinkItem(string url)
        {
            Url = url;
        }

        public ResponseItem SendRequestAndGetMarkup()
        {
            var responseItem = new ResponseItem(Url);

            try
            {
                var request = (HttpWebRequest)WebRequest.Create(Url);
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    responseItem.StatusCode = response.StatusCode;
                    if (response.StatusCode != HttpStatusCode.OK)
                        return responseItem;

                    using (var stream = response.GetResponseStream())
                    using (var reader = new StreamReader(stream))
                    {
                        responseItem.Markup = reader.ReadToEnd();
                    }
                }
            }
            catch (WebException ex)
            {
                var errorStatus = (ex.Response as HttpWebResponse).StatusCode;
                responseItem.StatusCode = errorStatus;
            }
            return responseItem;
        }
    }
}