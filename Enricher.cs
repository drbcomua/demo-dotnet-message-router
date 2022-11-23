namespace Send
{
    static class Enricher
    {
        public static void Enrich(ref Message m)
        {
            if (m.Ticker != null)
            {
                var (isin, name) = KnownCompanies.ByTicker(m.Ticker);
                m.ISIN ??= isin;
                m.CompanyName ??= name;
            }
            else
            {
                if (m.CompanyName != null)
                {
                    var (ticker, isin) = KnownCompanies.ByName(m.CompanyName);
                    m.ISIN ??= isin;
                    m.Ticker ??= ticker;
                }
                else
                {
                    if (m.ISIN != null)
                    {
                        var (ticker, name) = KnownCompanies.ByISIN(m.ISIN);
                        m.CompanyName ??= name;
                        m.Ticker ??= ticker;
                    }
                    else
                    {
                        m.CompanyName = "";
                        m.Ticker = "";
                        m.ISIN = "";
                    }
                }
            }


        }
    }
}
