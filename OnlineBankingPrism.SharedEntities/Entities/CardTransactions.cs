using System.Collections.Generic;
using Newtonsoft.Json;
using OnlineBankingPrism.SharedEntities.Entities.Abstract;

namespace OnlineBankingPrism.SharedEntities.Entities
{
    public class CardTransactions : DbEntityBase
    {
        [JsonProperty("transactions")]
        public List<Transaction> Transactions { get; set; }
    }
}