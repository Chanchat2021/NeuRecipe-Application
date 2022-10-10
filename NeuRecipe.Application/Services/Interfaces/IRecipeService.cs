using NeuRecipe.Application.DTO;

namespace NeuRecipe.Application.Services.Interfaces
{
    public interface IRecipeService
    {
        public Task<string> CreateRecipe(CreateRecipeDTO recipe);
        Task<IEnumerable<GetRecipeDTO>> GetRecipes();
        public Task<string> UpdateRecipe(UpdateRecipeDTO updateRecipe);
        public Task<string> DeleteRecipe(int id);
        public Task<byte[]> GetImageById(int id);
        Task<GetRecipeDTO> GetRecipeById(int id);
    }
}
