using System.Collections.Generic;
using System.Threading.Tasks;
using DevOpsPalermo.Models;
using DevOpsPalermo.Services.Repositories;

namespace DevOpsPalermo.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;

        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await _itemRepository.GetAllItemsAsync();
        }

        public async Task<Item> GetItemByIdAsync(int id)
        {
            Item? item = await _itemRepository.GetItemByIdAsync(id);
            if (item != null) return item;
            return new Item();
        }

        public async Task CreateItemAsync(Item item)
        {
            await _itemRepository.AddItemAsync(item);
        }

        public async Task UpdateItemAsync(Item item)
        {
            await _itemRepository.UpdateItemAsync(item);
        }

        public async Task DeleteItemAsync(int id)
        {
            await _itemRepository.DeleteItemAsync(id);
        }
    }
}
