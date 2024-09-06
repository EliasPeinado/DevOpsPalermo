using System.Collections.Generic;
using System.Threading.Tasks;
using DevOpsPalermo.Models;

namespace DevOpsPalermo.Services
{
    public interface IItemService
    {
        Task<IEnumerable<Item>> GetItemsAsync();
        Task<Item> GetItemByIdAsync(int id);
        Task CreateItemAsync(Item item);
        Task UpdateItemAsync(Item item);
        Task DeleteItemAsync(int id);
    }
}
