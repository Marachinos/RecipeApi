using System.ComponentModel.DataAnnotations;

namespace RecipeApi.Models.DTOs
{
    public class CreateRecipeDto
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Range(1, 480)]
        public int PrepTimeMinutes { get; set; }

        [Range(0, 480)]
        public int CookTimeMinutes { get; set; }

        [Range(1, 100)]
        public int Servings { get; set; }

        [Required]
        public List<CreateIngredientDto> Ingredients { get; set; }

        [Required]
        [MinLength(1)]
        public List<string> Instructions { get; set; }
    }

    public class CreateIngredientDto
    {
    }
}
