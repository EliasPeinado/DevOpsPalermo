using Xunit;
using Moq;
using DevOpsPalermo.Controllers;
using DevOpsPalermo.Models;
using DevOpsPalermo.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ItemsControllerTests
{
    private readonly ItemsController _itemsController;
    private readonly Mock<IItemService> _itemServiceMock;

    public ItemsControllerTests()
    {
        _itemServiceMock = new Mock<IItemService>();
        _itemsController = new ItemsController(_itemServiceMock.Object);
    }

    [Fact]
    public async Task GetItems_ShouldReturnOkResult_WithListOfItems()
    {
        var items = new List<Item>
        {
            new Item { Id = 1, Name = "Item 1", Description = "Description 1" },
            new Item { Id = 2, Name = "Item 2", Description = "Description 2" }
        };

        _itemServiceMock.Setup(service => service.GetItemsAsync()).ReturnsAsync(items);

        var result = await _itemsController.GetItems();

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnItems = Assert.IsType<List<Item>>(okResult.Value);
        Assert.Equal(2, returnItems.Count);
    }

    [Fact]
    public async Task GetItem_ShouldReturnOkResult_WhenItemExists()
    {
        var item = new Item { Id = 1, Name = "Item 1", Description = "Description 1" };

        _itemServiceMock.Setup(service => service.GetItemByIdAsync(1)).ReturnsAsync(item);

        var result = await _itemsController.GetItem(1);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnItem = Assert.IsType<Item>(okResult.Value);
        Assert.Equal(1, returnItem.Id);
    }


    [Fact]
    public async Task PostItem_ShouldReturnCreatedAtAction_WhenItemIsCreated()
    {
        var newItem = new Item { Id = 1, Name = "New Item", Description = "New Description" };

        _itemServiceMock.Setup(service => service.CreateItemAsync(newItem)).Returns(Task.CompletedTask);

        var result = await _itemsController.PostItem(newItem);

        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnItem = Assert.IsType<Item>(createdAtActionResult.Value);
        Assert.Equal(1, returnItem.Id);
    }

    [Fact]
    public async Task PutItem_ShouldReturnNoContent_WhenItemIsUpdated()
    {
        var updatedItem = new Item { Id = 1, Name = "Updated Item", Description = "Updated Description" };

        _itemServiceMock.Setup(service => service.UpdateItemAsync(updatedItem)).Returns(Task.CompletedTask);

        var result = await _itemsController.PutItem(1, updatedItem);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task PutItem_ShouldReturnBadRequest_WhenIdDoesNotMatchItemId()
    {
        var updatedItem = new Item { Id = 1, Name = "Updated Item", Description = "Updated Description" };

        var result = await _itemsController.PutItem(2, updatedItem);

        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task DeleteItem_ShouldReturnNoContent_WhenItemIsDeleted()
    {
        _itemServiceMock.Setup(service => service.DeleteItemAsync(1)).Returns(Task.CompletedTask);

        var result = await _itemsController.DeleteItem(1);

        Assert.IsType<NoContentResult>(result);
    }
}
