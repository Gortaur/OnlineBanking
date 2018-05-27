using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using OnlineBankingPrism.SharedEntities.Entities.Abstract;

namespace OnlineBankingPrism.SharedEntities.Entities
{
    public class User : DbEntityBase
    {
        [JsonProperty("password")]
        public String Password { get; set; }

        [JsonProperty("cardNumbers")]
        public List<String> CardNumbers { get; set; }
    }
}