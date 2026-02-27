using System.ComponentModel.DataAnnotations;

namespace RecipeApi.Models.DTOs
{
    public class UpdateRecipeDto
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [Range(1, 480)]
        public int PrepTimeMinutes { get; set; }

        [Range(0, 480)]
        public int CookTimeMinutes { get; set; }

        [Range(1, 100)]
        public int Servings { get; set; }

        [Required]
        [AllowedValues("Easy", "Medium", "Hard")]
        public string Difficulty { get; set; } = string.Empty;

        [Required]
        [MinLength(1)]
        public List<CreateIngredientDto> Ingredients { get; set; } = [];

        [Required]
        [MinLength(1)]
        public List<string> Instructions { get; set; } = [];
    }
}
