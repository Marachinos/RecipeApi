

namespace RecipeApi.Models

{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PrepTimeMinutes { get; set; }
        public int CookTimeMinutes { get; set; }
        public int Servings { get; set; }
        public string Difficulty { get; set; } // Easy, Medium, Hard
        public List<Ingredient> Ingredients { get; set; }
        public List<string> Instructions { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
