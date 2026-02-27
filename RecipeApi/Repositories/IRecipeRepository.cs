using RecipeApi.Models;

namespace RecipeApi.Repositories;

public interface IRecipeRepository
{
    Task<List<Recipe>> GetAllAsync();
    Task<Recipe?> GetByIdAsync(int id);
    Task<Recipe> CreateAsync(Recipe recipe);
    Task<bool> UpdateAsync(Recipe recipe);
    Task<bool> DeleteAsync(int id);
}