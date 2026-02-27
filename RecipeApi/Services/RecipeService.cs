
using RecipeApi.Models;
using RecipeApi.Models.DTOs;
using RecipeApi.Repositories;

namespace RecipeApi.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _repository;

        public RecipeService(IRecipeRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Recipe>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Recipe?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Recipe> CreateAsync(CreateRecipeDto dto)
        {

            var recipe = new Recipe
            {
                Name = dto.Name,
                Description = dto.Description,
                PrepTimeMinutes = dto.PrepTimeMinutes,
                CookTimeMinutes = dto.CookTimeMinutes,
                Servings = dto.Servings,
                Difficulty = "Easy",
                CreatedAt = DateTime.UtcNow,
                Ingredients = dto.Ingredients.Select(i => new Ingredient
                {
                    Name = i.Name,
                    Quantity = i.Quantity,
                    Unit = i.Unit
                }).ToList(),
                Instructions = dto.Instructions
            };

            return await _repository.CreateAsync(recipe);
        }
    }
}

