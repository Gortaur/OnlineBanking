using System;
using MongoDB.Driver;

namespace Web.Infrastructure.Database
{
    public static class MongoDbInstance
    {
        public static IMongoDatabase Get
            => _mongoDatabase ?? (_mongoDatabase = new MongoClient().GetDatabase(MongoDbName));
        private static IMongoDatabase _mongoDatabase;
        private static readonly String MongoDbName = "OnlineBankingDb";
    }
}
