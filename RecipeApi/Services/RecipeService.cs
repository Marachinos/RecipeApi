using RecipeApi.Models;
using RecipeApi.Models.DTOs;
using RecipeApi.Repositories;

namespace RecipeApi.Services;

public class RecipeService : IRecipeService
{
    private static readonly HashSet<string> AllowedDifficulties =
        new(StringComparer.OrdinalIgnoreCase) { "Easy", "Medium", "Hard" };

    private readonly IRecipeRepository _repo;

    public RecipeService(IRecipeRepository repo)
    {
        _repo = repo;
    }

    public Task<List<Recipe>> GetAllAsync() => _repo.GetAllAsync();

    public Task<Recipe?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);

    public async Task<List<Recipe>> SearchAsync(string term)
    {
        term = (term ?? string.Empty).Trim();
        if (term.Length == 0) return new List<Recipe>();

        var all = await _repo.GetAllAsync();

        return all
            .Where(r =>
                r.Name.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                (r.Description?.Contains(term, StringComparison.OrdinalIgnoreCase) ?? false))
            .ToList();
    }

    public async Task<List<Recipe>> GetByDifficultyAsync(string level)
    {
        level = (level ?? string.Empty).Trim();
        if (!IsValidDifficulty(level)) return new List<Recipe>();

        var all = await _repo.GetAllAsync();
        return all
            .Where(r => string.Equals(r.Difficulty, level, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public async Task<Recipe> CreateAsync(CreateRecipeDto dto)
    {
        if (!IsValidDifficulty(dto.Difficulty))
            throw new ArgumentException("Invalid difficulty. Use Easy, Medium, or Hard.");

        var recipe = new Recipe
        {
            Name = dto.Name.Trim(),
            Description = dto.Description?.Trim(),
            PrepTimeMinutes = dto.PrepTimeMinutes,
            CookTimeMinutes = dto.CookTimeMinutes,
            Servings = dto.Servings,
            Difficulty = dto.Difficulty.Trim(),
            Ingredients = dto.Ingredients.Select(i => new Ingredient
            {
                Name = i.Name.Trim(),
                Quantity = i.Quantity,
                Unit = i.Unit.Trim()
            }).ToList(),
            Instructions = dto.Instructions.Select(s => s.Trim()).ToList()
        };

        return await _repo.CreateAsync(recipe);
    }

    public async Task<bool> UpdateAsync(int id, UpdateRecipeDto dto)
    {
        if (!IsValidDifficulty(dto.Difficulty))
            throw new ArgumentException("Invalid difficulty. Use Easy, Medium, or Hard.");

        var updated = new Recipe
        {
            Id = id,
            Name = dto.Name.Trim(),
            Description = dto.Description?.Trim(),
            PrepTimeMinutes = dto.PrepTimeMinutes,
            CookTimeMinutes = dto.CookTimeMinutes,
            Servings = dto.Servings,
            Difficulty = dto.Difficulty.Trim(),
            Ingredients = dto.Ingredients.Select(i => new Ingredient
            {
                Name = i.Name.Trim(),
                Quantity = i.Quantity,
                Unit = i.Unit.Trim()
            }).ToList(),
            Instructions = dto.Instructions.Select(s => s.Trim()).ToList()
        };

        return await _repo.UpdateAsync(updated);
    }

    public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);

    private static bool IsValidDifficulty(string? level)
        => level is not null && AllowedDifficulties.Contains(level.Trim());
}