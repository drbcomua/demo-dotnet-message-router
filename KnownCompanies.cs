namespace Send
{
    public enum KnownCompany
    {
        [CompanyAttribute("AAPL", "US0378331005", "Apple Inc")] APPLE,
        [CompanyAttribute("MSFT", "US5949181045", "Microsoft Corp")] MICROSOFT,
        [CompanyAttribute("IBM", "US4592001014", "International Business Machines Corp")] IBM
    }

    [AttributeUsage(AttributeTargets.Field)]
    class CompanyAttribute : Attribute
    {
        internal CompanyAttribute(string ticker, string isin, string companyName)
        {
            this.Ticker = ticker;
            this.ISIN = isin;
            this.CompanyName = companyName;
        }
        public string Ticker { get; private set; }
        public string ISIN { get; private set; }
        public string CompanyName { get; private set; }
    }

    public static class KnownCompanies
    {
        public static KnownCompany ByISIN(string isin)
        {
            Console.WriteLine(isin);
            return KnownCompany.APPLE;
        }
    }
}
