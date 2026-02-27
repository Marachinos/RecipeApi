using RecipeApi.Models;
using RecipeApi.Models.DTOs;

namespace RecipeApi.Services;

public interface IRecipeService
{
    Task<List<Recipe>> GetAllAsync();
    Task<Recipe?> GetByIdAsync(int id);
    Task<List<Recipe>> SearchAsync(string term);
    Task<List<Recipe>> GetByDifficultyAsync(string level);
    Task<Recipe> CreateAsync(CreateRecipeDto dto);
    Task<bool> UpdateAsync(int id, UpdateRecipeDto dto);
    Task<bool> DeleteAsync(int id);
}