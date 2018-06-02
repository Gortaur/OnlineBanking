using System;
using System.Collections.Generic;
using OnlineBankingPrism.SharedEntities.Entities;

namespace Web.Infrastructure.Database
{
    public static class CollectionNameClassMap
    {
        public static Dictionary<Type, String> CollectionTypeMap = new Dictionary<Type, string>
        {
            {typeof(User), "users"},
            {typeof(Card), "cards"},
            {typeof(CardTransactions), "cardTransactions"}
        };
    }
}