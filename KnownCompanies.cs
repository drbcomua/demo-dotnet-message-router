namespace Send
{
    public static class KnownCompanies
    {
        readonly static List<(string, string, string)> credentials = new()
        {
            ("AAPL", "US0378331005", "Apple Inc"),
            ("MSFT", "US5949181045", "Microsoft Corp"),
            ("IBM", "US4592001014", "International Business Machines Corp")
        };

        public static (string, string) ByISIN(string isin)
        {
            foreach (var (ticker, i, name) in credentials)
            {
                if (isin == i) return (ticker, name);
            }
            return ("", "");
        }

        public static (string, string) ByTicker(string ticker)
        {
            foreach (var (t, isin, name) in credentials)
            {
                if (ticker == t) return (isin, name);
            }
            return ("", "");
        }

        public static (string, string) ByName(string name)
        {
            foreach (var (ticker, isin, n) in credentials)
            {
                if (name == n) return (ticker, isin);
            }
            return ("", "");
        }
    }
}
