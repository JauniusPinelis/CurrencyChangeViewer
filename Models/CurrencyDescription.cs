using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExchangeRateViewer.Models
{
    /// <summary>
    /// Contains Currency code and description in both English and Lithuanian
    /// </summary>
    public class CurrencyDescription
    {
        public string Code { get; set; }
        public string EnglishName { get; set; }
        public string LithuanianName { get; set; }

        public CurrencyDescription()
        {
            EnglishName = "";
            LithuanianName = "";
        }

    }
}