using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OnlineBankingPrism.SharedEntities.Entities
{
    public class ApplicationUser
    {
        public ApplicationUser()
        {

        }

        public ApplicationUser(User user, List<CardTransactions> cardTransactions, List<Card> cards)
        {
            Login = user.Id;
            CardTransactions = cardTransactions;
            Cards = cards;
        }
        [JsonProperty("login")]
        public String Login { get; set; }

        [JsonProperty("cards")]
        public List<Card> Cards { get; set; }

        [JsonProperty("cardTransactions")]
        public List<CardTransactions> CardTransactions { get; set; }
    }
}
