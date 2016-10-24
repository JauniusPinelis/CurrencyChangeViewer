using ExchangeRateViewer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace ExchangeRateViewer.Helpers
{
    /// <summary>
    /// Contains Helper methods for Currency transformation tasks
    /// </summary>
    public class ProcessingHelperLibrary
    {
        public IEnumerable<Currency> ProcessCurrencyRates(XmlNode currencyData, DateTime date, List<CurrencyDescription> currencyDescriptionList)
        {
            var currencyList = new List<Currency>();
            if (currencyData.Name == "error" || currencyData.InnerText.Contains("No data found"))
                return new List<Currency>();

            foreach (XmlNode currency in currencyData.ChildNodes)
            {
                var currencyModel = new Currency();

                foreach (XmlNode item in currency.ChildNodes)
                {
                    if (item.Name == "Currency".ToLower())
                    {
                        currencyModel.Code = item.InnerText;
                        currencyModel.Description = currencyDescriptionList.FirstOrDefault(x => x.Code == currencyModel.Code) ?? new CurrencyDescription();
                    }
                    if (item.Name == "Rate".ToLower())
                    {
                        currencyModel.Rate = double.Parse(item.InnerText);
                    }
                }

                

                currencyList.Add(currencyModel);
            }

            return currencyList;
        }

        public IEnumerable<CurrencyRateChange> GenerateCurrencyChangeList(DateTime date, IEnumerable<Currency> currencyList, IEnumerable<Currency> currencyListModelDayAgo)
        {
            var currencyChangeList = new List<CurrencyRateChange>();

            if (currencyList.Count() != currencyListModelDayAgo.Count())
            {
                return new List<CurrencyRateChange>();
            }

            for (int i = 0; i < currencyList.Count(); i++)
            {
                var currencyDif = currencyList.ElementAt(i).Rate - currencyListModelDayAgo.ElementAt(i).Rate;
                currencyChangeList.Add(new CurrencyRateChange(currencyList.ElementAt(i).Code, currencyList.ElementAt(i).Description.EnglishName,
                    currencyList.ElementAt(i).Rate - currencyListModelDayAgo.ElementAt(i).Rate));
            }

            return currencyChangeList.OrderByDescending(x => x.RateChange); 
        }

        public List<CurrencyDescription> ProcessCurrencyDescriptions(XmlNode currencyData)
        {
            var currencyDescriptionList = new List<CurrencyDescription>();
            foreach (XmlNode currencyObj in currencyData.ChildNodes)
            {
                var currency = new CurrencyDescription();
                foreach (XmlNode item in currencyObj.ChildNodes)
                {
                    if (item.Name == "currency")
                        currency.Code = item.InnerText;
                    if (item.Name == "description" && item.Attributes["lang"].Value == "en")
                        currency.EnglishName = item.InnerText;
                    if (item.Name == "description" && item.Attributes["lang"].Value == "lt")
                        currency.LithuanianName = item.InnerText;
                }

                currencyDescriptionList.Add(currency);
                
            }
            
            return currencyDescriptionList;
        }

    }

   
}