using System;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace OnlineBankingPrism.SharedEntities.Entities.Abstract
{
    public abstract class DbEntityBase
    {
        [JsonProperty("id")]
        [BsonId, BsonElement("_id")]
        public virtual String Id { get; set; }
    }
}
