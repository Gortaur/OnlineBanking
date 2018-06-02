using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using OnlineBankingPrism.SharedEntities.Entities.Abstract;

namespace Web.Infrastructure.Database.Repositories
{
    public class GenericRepository<T> where T : DbEntityBase
    {
        public GenericRepository()
        {
            var collectionName = CollectionNameClassMap.CollectionTypeMap[typeof(T)];
            if (String.IsNullOrEmpty(collectionName))
            {
                throw new ArgumentNullException();
            }

             _collection = MongoDbInstance.Get.GetCollection<T>(collectionName);
           // _collection = collection ?? throw new ArgumentNullException(nameof(collection));
        }

        public async Task<T> GetById(String id)
        {
            var filter = Builders<T>.Filter.Eq(IdFieldName, id);
            var list = (await _collection.FindAsync(filter)).ToList();
            if (list.Count != 1)
            {
                return default(T);
            }
            return list.FirstOrDefault();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var list = await _collection.FindAsync(Builders<T>.Filter.Empty);
            try
            {
                return list.ToList();
            }
            catch (Exception)
            {
                return new List<T>();
            }
        }

        public async Task<Boolean> Update(T itemToUpdate)
        {
            try
            {
                var filter = Builders<T>.Filter.Eq(IdFieldName, itemToUpdate.Id);
                await _collection.ReplaceOneAsync(filter, itemToUpdate);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Boolean> Delete(T itemToDelete)
                                                        => await Delete(itemToDelete.Id);
        public async Task<Boolean> Delete(String login)
        {
            try
            {
                var result = await _collection.DeleteOneAsync(item =>
                     item.Id.Equals(login));
                return result.DeletedCount != 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<Boolean> Insert(T itemToInsert)
        {
            try
            {
                var filter = Builders<T>.Filter.Eq(IdFieldName, itemToInsert.Id);
                T result = (await _collection.FindAsync(filter)).FirstOrDefault();
                if (result == null)
                {
                    await _collection.InsertOneAsync(itemToInsert);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private readonly IMongoCollection<T> _collection;
        private const String IdFieldName = "_id";
    }
}
