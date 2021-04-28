using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aspnetcore_sql.Models;

namespace aspnetcore_sql.Services
{
    public interface ICosmosDbService
    {
        Task<IEnumerable<MyItem>> GetItemsAsync(string query);
        Task<MyItem> GetItemAsync(string id);
        Task AddItemAsync(MyItem item);
        Task UpdateItemAsync(string id, MyItem item);
        Task DeleteItemAsync(string id);
    }
}
