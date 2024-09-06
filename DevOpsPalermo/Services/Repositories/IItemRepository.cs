using System.Collections.Generic;
using System.Threading.Tasks;
using DevOpsPalermo.Models;

namespace DevOpsPalermo.Services.Repositories
{
    public interface IItemRepository
    {
        Task<IEnumerable<Item>> GetAllItemsAsync();
        Task<Item?> GetItemByIdAsync(int id);
        Task AddItemAsync(Item item);
        Task UpdateItemAsync(Item item);
        Task DeleteItemAsync(int id);
    }
}
