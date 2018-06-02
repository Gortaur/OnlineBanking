using System;
using Newtonsoft.Json;
using OnlineBankingPrism.SharedEntities.Enums;

namespace OnlineBankingPrism.SharedEntities.Entities
{
    public class Transaction
    {
        [JsonProperty("sourceCard")]
        public String SourceCard { get; set; }
        [JsonProperty("destination")]
        public String Destination { get; set; }

        [JsonProperty("transactionSum")]
        public Double TransactionSum { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("transactionType")]
        public TransactionTypes TransactionType { get; set; }

        [JsonProperty("transactionDestinationRole")]
        public TransactionDestinationRole TransactionDestinationRole { get; set; }
    }
}