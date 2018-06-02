using System;
using Newtonsoft.Json;

namespace OnlineBankingPrism.SharedEntities.Entities
{
    public class NbuExchangeRates
    {
        [JsonProperty("r030")]
        public String Id { get; set; }

        [JsonProperty("txt")]
        public String FullName { get; set; }

        [JsonProperty("rate")]
        public String Rate { get; set; }

        [JsonProperty("cc")]
        public String Name { get; set; }

        [JsonProperty("exchangedate")]
        public String Date { get; set; }
    }
}
