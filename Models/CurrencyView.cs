using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExchangeRateViewer.Models
{
    /// <summary>
    /// Model of currency changes to be displayed on a view
    /// </summary>
    public class CurrencyView
    {
        public IEnumerable<CurrencyRateChange> CurrencyChangeList { get; set; }
        public DateTime Date { get; set; }

        public CurrencyView (IEnumerable<CurrencyRateChange> currencyChangeList, DateTime date)
        {
            CurrencyChangeList = currencyChangeList;
            Date = date;
        }

        public CurrencyView (DateTime date) : this (new List<CurrencyRateChange>(), date)
        {

        }
    }


}