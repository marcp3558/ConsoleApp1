using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp1
{
    public static class Currencies
    {
        public static List<Currency> CurrencyList;

        public static void Initialize()
        {
            var currencyFile = XDocument.Load(".\\Currencies.xml");
            CurrencyList = (from currency in currencyFile.Descendants("Currencies").Elements("Currency")
                select new Currency
                {
                    Denomination = currency.Element("Denomination")?.Value,
                    ValueString = currency.Element("Value")?.Value,
                    DenominationPlural = currency.Element("DenominationPlural")?.Value
                }).ToList();
        }
    }
}
