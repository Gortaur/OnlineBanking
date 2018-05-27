using System;
using Newtonsoft.Json;
using OnlineBankingPrism.SharedEntities.Entities.Abstract;

namespace OnlineBankingPrism.SharedEntities.Entities
{
    public class Card : DbEntityBase
    {
        [JsonProperty("ownerId")]
        public String OwnerId { get; set; }

        [JsonProperty("expireDate")]
        public DateTime ExpireDate { get; set; }

        [JsonProperty("balance")]
        public Double Balance { get; set; }
    }
}