using System.Collections.Generic;
using OnlineBankingPrism.SharedEntities.Entities;

namespace OnlineBankingPrism.Constants
{
    public  static class SharedApplicationData
    {
        public static Card SelectedCard { get; set; }
        public static ApplicationUser CurrentUser { get; set; }
        public static List<NbuExchangeRates> ExchangeRates { get; set; }
    }
}
