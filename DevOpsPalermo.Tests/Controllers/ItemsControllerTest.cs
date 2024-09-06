using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevOpsPalermo.Models;
using DevOpsPalermo.Services;
using DevOpsPalermo.Services.Repositories;

public class ItemServiceTests
{
    private readonly ItemService _itemService;
    private readonly Mock<IItemRepository> _itemRepositoryMock;

    public ItemServiceTests()
    {
        _itemRepositoryMock = new Mock<IItemRepository>();
        _itemService = new ItemService(_itemRepositoryMock.Object);
    }

    [Fact]
    public async Task GetItemsAsync_ShouldReturnListOfItems()
    {
        var items = new List<Item>
        {
            new Item { Id = 1, Name = "Item 1", Description = "Description 1" },
            new Item { Id = 2, Name = "Item 2", Description = "Description 2" }
        };
        _itemRepositoryMock.Setup(repo => repo.GetAllItemsAsync()).ReturnsAsync(items);

        var result = await _itemService.GetItemsAsync();

        Assert.Equal(2, result.Count());
        Assert.Equal("Item 1", result.First().Name);
    }

    [Fact]
    public async Task GetItemByIdAsync_ShouldReturnItem_WhenItemExists()
    {
        var item = new Item { Id = 1, Name = "Item 1", Description = "Description 1" };
        _itemRepositoryMock.Setup(repo => repo.GetItemByIdAsync(1)).ReturnsAsync(item);

        var result = await _itemService.GetItemByIdAsync(1);

        Assert.Equal("Item 1", result.Name);
    }

    [Fact]
    public async Task GetItemByIdAsync_ShouldReturnEmptyItem_WhenItemDoesNotExist()
    {
        _itemRepositoryMock.Setup(repo => repo.GetItemByIdAsync(1)).ReturnsAsync((Item?)null);

        var result = await _itemService.GetItemByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(0, result.Id);
    }

    [Fact]
    public async Task CreateItemAsync_ShouldCallRepositoryAddItem()
    {
        var newItem = new Item { Id = 3, Name = "New Item", Description = "New Description" };

        await _itemService.CreateItemAsync(newItem);

        _itemRepositoryMock.Verify(repo => repo.AddItemAsync(newItem), Times.Once);
    }

    [Fact]
    public async Task UpdateItemAsync_ShouldCallRepositoryUpdateItem()
    {
        var existingItem = new Item { Id = 1, Name = "Updated Item", Description = "Updated Description" };

        await _itemService.UpdateItemAsync(existingItem);

        _itemRepositoryMock.Verify(repo => repo.UpdateItemAsync(existingItem), Times.Once);
    }

    [Fact]
    public async Task DeleteItemAsync_ShouldCallRepositoryDeleteItem()
    {
        await _itemService.DeleteItemAsync(1);

        _itemRepositoryMock.Verify(repo => repo.DeleteItemAsync(1), Times.Once);
    }
}
