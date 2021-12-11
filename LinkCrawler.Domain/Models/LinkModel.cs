namespace LinkCrawler.Domain.Models
{
    public class LinkModel
    {
        public string Address { get; private set; }
        public string Referrer { get; private set; }
        public bool CheckingFinished { get; private set; }
        private int _StatusCode;

        public int StatusCode
        {
            get { return _StatusCode; }
            set
            {
                _StatusCode = value;
                this.CheckingFinished = true;
            }
        }

        public LinkModel(string Address, string? Referrer = null)
        {
            this.Address = Address;
            this.Referrer = Referrer??Address;
            this.CheckingFinished = false;
        }

    }
}
