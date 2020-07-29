using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkCrawler.Models
{
    public class LinkModel
    {
        public string Address { get; private set; }
        public bool CheckingFinished { get; private set; }
        private int _statusCode;

        public int StatusCode
        {
            get
            {
                return _statusCode;
            }
            set
            {
                _statusCode = value;
                CheckingFinished = true;
            }
        }

        public LinkModel (string address)
        {
            Address = address;
            CheckingFinished = false;
        }

    }
}
