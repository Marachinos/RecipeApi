using Microsoft.AspNetCore.Mvc;

using Moq;

using RecipeApi.Controllers;

using RecipeApi.Models;

using RecipeApi.Models.DTOs;

using RecipeApi.Services;

using Xunit;

namespace RecipeApi.Tests.Recipes;

public class RecipesControllerTests

{

    private readonly Mock<IRecipeService> _mockService;

    private readonly RecipesController _controller;

    public RecipesControllerTests()

    {

        _mockService = new Mock<IRecipeService>();

        _controller = new RecipesController(_mockService.Object);

    }

    [Fact]

    public async Task GetAll_Returns200Ok()

    {

        _mockService.Setup(s => s.GetAllAsync())

            .ReturnsAsync(new List<Recipe> { new() { Id = 1, Name = "A" } });

        var result = await _controller.GetAll();

        var ok = Assert.IsType<OkObjectResult>(result.Result);

        var list = Assert.IsType<List<Recipe>>(ok.Value);

        Assert.Single(list);

    }

    [Fact]

    public async Task GetById_Missing_Returns404()

    {

        _mockService.Setup(s => s.GetByIdAsync(123))

            .ReturnsAsync((Recipe?)null);

        var result = await _controller.GetById(123);

        Assert.IsType<NotFoundResult>(result.Result);

    }

    [Fact]

    public async Task Create_Returns201Created()

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

        _mockService.Setup(s => s.CreateAsync(It.IsAny<CreateRecipeDto>()))

            .ReturnsAsync(new Recipe { Id = 1, Name = "Pannkakor", PrepTimeMinutes = 10, CookTimeMinutes = 20 });

        var result = await _controller.Create(dto);

        var created = Assert.IsType<CreatedAtActionResult>(result.Result);

        var body = Assert.IsType<Recipe>(created.Value);

        Assert.Equal(1, body.Id);

    }

}
