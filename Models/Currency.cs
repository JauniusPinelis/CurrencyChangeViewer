using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExchangeRateViewer.Models
{
    public class Currency
    {
        public string Code { get; set; }
        public double Rate { get; set; }
        public CurrencyDescription Description { get; set; }
    }
}