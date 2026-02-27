using RecipeApi.Models;

namespace RecipeApi.Repositories;


// En enkel in-memory repository. I en riktig applikation skulle vi använda en databas, t.ex. via Entity Framework Core.
public class RecipeRepository : IRecipeRepository
{
    private readonly List<Recipe> _recipes = new();
    private int _nextRecipeId = 1;
    private int _nextIngredientId = 1;
    private readonly object _lock = new();

    // För enkelhetens skull hanterar vi ID
    public Task<List<Recipe>> GetAllAsync()
    {
        lock (_lock)
        {
            // Returnera kopia så ingen kan ändra listan utifrån
            return Task.FromResult(_recipes.Select(CloneRecipe).ToList());
        }
    }

    // Hitta receptet med rätt ID, eller returnera null
    public Task<Recipe?> GetByIdAsync(int id)
    {
        lock (_lock)
        {
            var found = _recipes.FirstOrDefault(r => r.Id == id);
            return Task.FromResult(found is null ? null : CloneRecipe(found));
        }
    }

    // 
    public Task<Recipe> CreateAsync(Recipe recipe)
    {
        lock (_lock)
        {
            var toStore = CloneRecipe(recipe);

            toStore.Id = _nextRecipeId++;
            toStore.CreatedAt = DateTime.UtcNow;

            foreach (var ing in toStore.Ingredients)
            {
                if (ing.Id == 0) ing.Id = _nextIngredientId++;
            }

            _recipes.Add(toStore);

            return Task.FromResult(CloneRecipe(toStore));
        }
    }

    // Uppdatera ett befintligt recept. Behåll CreatedAt och hantera nya ingredienser
    public Task<bool> UpdateAsync(Recipe recipe)
    {
        lock (_lock)
        {
            var existingIndex = _recipes.FindIndex(r => r.Id == recipe.Id);
            if (existingIndex < 0) return Task.FromResult(false);

            var existing = _recipes[existingIndex];

            var updated = CloneRecipe(recipe);
            updated.CreatedAt = existing.CreatedAt; // behåll CreatedAt

            foreach (var ing in updated.Ingredients)
            {
                if (ing.Id == 0) ing.Id = _nextIngredientId++;
            }

            _recipes[existingIndex] = updated;
            return Task.FromResult(true);
        }
    }

    // Ta bort ett recept baserat på ID. Returnera true om det togs bort, false om det inte fanns
    public Task<bool> DeleteAsync(int id)
    {
        lock (_lock)
        {
            var removed = _recipes.RemoveAll(r => r.Id == id);
            return Task.FromResult(removed > 0);
        }
    }

    // En hjälpfunktion för att klona ett recept så att vi inte exponerar interna referenser
    private static Recipe CloneRecipe(Recipe r)
    {
        return new Recipe
        {
            Id = r.Id,
            Name = r.Name,
            Description = r.Description,
            PrepTimeMinutes = r.PrepTimeMinutes,
            CookTimeMinutes = r.CookTimeMinutes,
            Servings = r.Servings,
            Difficulty = r.Difficulty,
            CreatedAt = r.CreatedAt,
            Instructions = r.Instructions.ToList(),
            Ingredients = r.Ingredients.Select(i => new Ingredient
            {
                Id = i.Id,
                Name = i.Name,
                Quantity = i.Quantity,
                Unit = i.Unit
            }).ToList()
        };
    }
}