using ExchangeRateViewer.Helpers;
using ExchangeRateViewer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ExchangeRateViewer.Controllers
{
   
    public class ExchangeRateController : Controller
    {

        private ExchangeRateService.ExchangeRatesSoapClient serviceClient;
        private ProcessingHelperLibrary processingHelper;
        private List<CurrencyDescription> currencyDescriptionList;

        public ExchangeRateController()
        {
            serviceClient = new ExchangeRateService.ExchangeRatesSoapClient();
            processingHelper = new ProcessingHelperLibrary();

            var currencyRates = serviceClient.getListOfCurrencies();
            currencyDescriptionList = processingHelper.ProcessCurrencyDescriptions(currencyRates); 

        }      
        
        [HttpPost]
        public JsonResult GetExchangeRates(string selectedDate)
        {
            var date = Convert.ToDateTime(selectedDate);

            try
            {
                var currencyDataUnprocessed = serviceClient.getExchangeRatesByDate(date.ToString());
                var previousDayCurrencyDataUnprocessed = serviceClient.getExchangeRatesByDate(date.AddDays(-1).ToString());

                var currencyData = processingHelper.ProcessCurrencyRates(currencyDataUnprocessed, date, currencyDescriptionList);
                var previousDayCurrencyData = processingHelper.ProcessCurrencyRates(previousDayCurrencyDataUnprocessed, date, currencyDescriptionList);

                var currencyChangeList = processingHelper.GenerateCurrencyChangeList(date, currencyData, previousDayCurrencyData);
                var result = new CurrencyView(currencyChangeList.ToList(), date);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                // If something goes wrong just return an empty list and pretend that nothing has happened
                return Json(new CurrencyView(date), JsonRequestBehavior.AllowGet);
            }
        }

    }
}