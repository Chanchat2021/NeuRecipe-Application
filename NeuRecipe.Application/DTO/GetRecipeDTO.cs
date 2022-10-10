﻿
namespace NeuRecipe.Application.DTO
{
    public class GetRecipeDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Ingredients { get; set; }
        public string Directions { get; set; }
        public string RecipeTips { get; set; }
        public byte[]? Image { get; set; }
        public string NutritionFacts { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
