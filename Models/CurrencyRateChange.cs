using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExchangeRateViewer.Models
{
    /// <summary>
    /// Model to to hold the information on pecific currency and its change between days
    /// </summary>
    public class CurrencyRateChange
    {
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
        public double RateChange { get; set; }

        public CurrencyRateChange(string currencyCode, string currencyName, double rateChange)
        {
            CurrencyCode = currencyCode;
            CurrencyName = currencyName;
            RateChange = rateChange;
        }
    }
}