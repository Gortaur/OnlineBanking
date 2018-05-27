using System;
using Newtonsoft.Json;

namespace OnlineBankingPrism.SharedEntities.Entities.ExchangeRates
{
   public class CurrencyExchangeRate
    {
        [JsonProperty("baseCurrency")]
        public String BaseCurrency { get; set; }

        [JsonProperty("currency")]
        public String Currency { get; set; }

        [JsonProperty("saleRateNB")]
        public String SaleRateNb { get; set; }

        [JsonProperty("purchaseRateNB")]
        public String PurchaseRateNb { get; set; }

        [JsonProperty("saleRate")]
        public String SaleRatePb { get; set; }

        [JsonProperty("purchaseRate")]
        public String PurchaseRatePb { get; set; }
    }
}