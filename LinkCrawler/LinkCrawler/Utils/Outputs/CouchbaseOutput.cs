using System;
using System.Collections.Generic;
using Couchbase;
using Couchbase.Configuration.Client;
using Couchbase.Core;
using LinkCrawler.Models;
using LinkCrawler.Utils.Settings;

namespace LinkCrawler.Utils.Outputs
{
    public class CouchbaseOutput : IOutput, IDisposable
    {
        private readonly ISettings _settings;
        private IBucket _bucket;

        public CouchbaseOutput(ISettings settings)
        {
            _settings = settings;
            Setup();
        }

        private void Setup()
        {
            ClusterHelper.Initialize(new ClientConfiguration
            {
                Servers = new List<Uri> { new Uri(_settings.CouchbaseConnectionString) }
            });
            _bucket = ClusterHelper.GetBucket(_settings.CouchbaseBucketName, _settings.CouchbaseBucketPassword);
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
            _bucket.Insert(new Document<dynamic>
            {
                Id = Guid.NewGuid().ToString(),
                Content = new {
                    responseModel.StatusCodeNumber,
                    responseModel.StatusCode,
                    responseModel.RequestedUrl,
                    responseModel.ReferrerUrl,
                    responseModel.IsSuccess,
                    responseModel.ShouldCrawl
                }
            });
        }

        public void Dispose()
        {
            _bucket.Dispose();
            ClusterHelper.Close();
        }
    }
}