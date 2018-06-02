using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OnlineBankingPrism.SharedEntities.Entities.ExchangeRates
{
    public class ExchangeRates
    {
        [JsonProperty("date")]
        public DateTime Date { get; set; }
        [JsonProperty("bank")]
        public String Bank { get; set; }

        [JsonProperty("baseCurrency")]
        public String BaseCurrencyCode { get; set; }
        [JsonProperty("baseCurrencyLit")]
        public String BaseCurrencyName { get; set; }
        [JsonProperty("exchangeRate")]
        public List<CurrencyExchangeRate> ExchangeRate { get; set; }
    }
}
