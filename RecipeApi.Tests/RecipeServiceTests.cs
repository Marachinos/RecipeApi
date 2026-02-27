using Moq;
using RecipeApi.Models;
using RecipeApi.Models.DTOs;
using RecipeApi.Repositories;
using RecipeApi.Services;
using Xunit;

namespace RecipeApi.Tests.Recipes;

public class RecipeServiceTests
{
    private readonly Mock<IRecipeRepository> _mockRepo;
    private readonly RecipeService _service;

    public RecipeServiceTests()
    {
        _mockRepo = new Mock<IRecipeRepository>();
        _service = new RecipeService(_mockRepo.Object);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsList()
    {
        _mockRepo.Setup(r => r.GetAllAsync())
            .ReturnsAsync(new List<Recipe> { new() { Id = 1, Name = "A" } });

        var result = await _service.GetAllAsync();

        Assert.Single(result);
    }

    [Fact]
    public async Task GetByIdAsync_ExistingId_ReturnsRecipe()
    {
        _mockRepo.Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(new Recipe { Id = 1, Name = "Test" });

        var result = await _service.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal("Test", result!.Name);
    }

    [Fact]
    public async Task GetByIdAsync_NonExistingId_ReturnsNull()
    {
        _mockRepo.Setup(r => r.GetByIdAsync(999))
            .ReturnsAsync((Recipe?)null);

        var result = await _service.GetByIdAsync(999);

        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAsync_CreatesAndReturnsRecipe()
    {
        var dto = new CreateRecipeDto
        {
            Name = "Pannkakor",
            PrepTimeMinutes = 10,
            CookTimeMinutes = 20,
            Servings = 4,
            Difficulty = "Easy",
            Ingredients = new()
            {
                new CreateIngredientDto { Name = "Mjöl", Quantity = 3, Unit = "dl" }
            },
            Instructions = new() { "Blanda" }
        };

        _mockRepo.Setup(r => r.CreateAsync(It.IsAny<Recipe>()))
            .ReturnsAsync((Recipe r) =>
            {
                r.Id = 1;
                r.CreatedAt = DateTime.UtcNow;
                r.TotalTimeMinutes = GetTotalTimeAsyncEnumerable(30); // Ensure this property is set
                return r;
            });

        var created = await _service.CreateAsync(dto);

        Assert.Equal(1, created.Id);
        Assert.NotNull(created.TotalTimeMinutes);
        Assert.Equal(30, await created.TotalTimeMinutes.SingleAsync());
    }

    [Fact]
    public async Task SearchAsync_FiltersCorrectly()
    {
        _mockRepo.Setup(r => r.GetAllAsync())
            .ReturnsAsync(new List<Recipe>
            {
                new() { Id = 1, Name = "Pannkakor", Description = "Svenska" },
                new() { Id = 2, Name = "Lasagne", Description = "Italiensk" }
            });

        var result = await _service.SearchAsync("pann");

        Assert.Single(result);
        Assert.Equal(1, result[0].Id);
    }

    private static async IAsyncEnumerable<object> GetTotalTimeAsyncEnumerable(int value)
    {
        yield return value;
    }
}
