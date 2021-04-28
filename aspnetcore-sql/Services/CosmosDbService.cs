using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using aspnetcore_sql.Models;
using Microsoft.Azure.Cosmos;
using Container = Microsoft.Azure.Cosmos.Container;

namespace aspnetcore_sql.Services
{
    public class CosmosDbService : ICosmosDbService
    {
        private Container _container;
        private CosmosClient _client;

        public CosmosDbService(string connectionString)
        {
            _client = new CosmosClient(connectionString);
            _container = _client.GetContainer("coreDB", "MyItem");
        }

        public async Task AddItemAsync(MyItem item)
        {
            await this._container.CreateItemAsync<MyItem>(item, new PartitionKey(item.Id));
        }

        public async Task DeleteItemAsync(string id)
        {
            await this._container.DeleteItemAsync<MyItem>(id, new PartitionKey(id));
        }

        public async Task<MyItem> GetItemAsync(string id)
        {
            try
            {
                ItemResponse<MyItem> response = await this._container.ReadItemAsync<MyItem>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

        }

        public async Task<IEnumerable<MyItem>> GetItemsAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<MyItem>(new QueryDefinition(queryString));
            List<MyItem> results = new List<MyItem>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task UpdateItemAsync(string id, MyItem item)
        {
            await this._container.UpsertItemAsync<MyItem>(item, new PartitionKey(id));
        }
    }
}
