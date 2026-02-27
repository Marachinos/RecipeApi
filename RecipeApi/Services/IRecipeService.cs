using RecipeApi.Models;
using RecipeApi.Models.DTOs;


namespace RecipeApi.Services
    {
        public interface IRecipeService
        {
            Task<List<Recipe>> GetAllAsync();
            Task<Recipe?> GetByIdAsync(int id);
            Task<Recipe> CreateAsync(CreateRecipeDto dto);
        }

    }
