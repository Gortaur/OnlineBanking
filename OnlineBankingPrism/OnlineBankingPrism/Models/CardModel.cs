using System;
using OnlineBankingPrism.SharedEntities.Entities;

namespace OnlineBankingPrism.Models
{
    public class CardModel
    {
        public CardModel(Card card)
        {
            Id = card.Id;
            Balance = Math.Round(card.Balance, 2) + " UAH";
        } 
        public String Id { get; set; }

        public String Balance { get; set; }
    }
}