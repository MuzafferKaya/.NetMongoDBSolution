using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDBExample.Models;

namespace MongoDBExample.Services
{
    public class MongoDBService
    {
        private readonly IMongoCollection<Customer> _collection;

        public MongoDBService(IOptions<MongoDBSettings> options)
        {
            var client = new MongoClient(options.Value.ConnectionURI);
            var database = client.GetDatabase(options.Value.DatabaseName);
            _collection = database.GetCollection<Customer>(options.Value.CollectionName);
        }

        public async Task CreateAsync(Customer customer)
        {
            await _collection.InsertOneAsync(customer);
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            var filter = Builders<Customer>.Filter.Empty;
            return await _collection.Find(filter).ToListAsync();
        }

        public async Task<Customer> GetByIdAsync(string id)
        {
            var filter = Builders<Customer>.Filter.Eq("Id", id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(string id, Customer customer)
        {
            var filter = Builders<Customer>.Filter.Eq("Id", id);
            var update = Builders<Customer>.Update.Set("Username", customer.Username);
            await _collection.UpdateOneAsync(filter, update);
        }

        public async Task DeleteAsync(string id)
        {
            FilterDefinition<Customer> filter = Builders<Customer>.Filter.Eq("Id", id);
            await _collection.DeleteOneAsync(filter);
        }
    }
}
