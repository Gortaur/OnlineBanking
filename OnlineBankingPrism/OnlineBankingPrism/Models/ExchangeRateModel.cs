using System;
using OnlineBankingPrism.SharedEntities.Entities;

namespace OnlineBankingPrism.Models
{
    public class ExchangeRateModel
    {
        public ExchangeRateModel(NbuExchangeRates exchangeRate)
        {
            BaseCurrency = "UAH";
            ConvertToCurrency = exchangeRate.Name;
            ExchangeRate = exchangeRate.Rate.Substring(0,5);
        }
        public String BaseCurrency { get; set; }
        public String ConvertToCurrency { get; set; }
        public String ExchangeRate { get; set; }
    }
}
