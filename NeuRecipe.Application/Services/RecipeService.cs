using NeuRecipe.Application.DTO;
using NeuRecipe.Application.Services.Interfaces;
using NeuRecipe.Domain.Entity;
using NeuRecipe.Infrastructure.Repositories.Interfaces;

namespace NeuRecipe.Application.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IGenericRepository<Recipe> _genericRepositoryRecipe;
        private readonly IGenericRepository<User> _genericRepositoryUser;
        public RecipeService(IGenericRepository<Recipe> genericRepositoryRecipe, IGenericRepository<User> genericRepositoryUser)
        {
            _genericRepositoryRecipe = genericRepositoryRecipe;
            _genericRepositoryUser = genericRepositoryUser;

        }
        public async Task<string> CreateRecipe(CreateRecipeDTO createRecipe)
        {
            var query = _genericRepositoryUser.GetQuery();
            var response = query.Where(e=>e.Email==createRecipe.CreatedBy).FirstOrDefault();
            if (response != null)
            {
                var result = new Recipe
                {
                    Title = createRecipe.Title,
                    Description = createRecipe.Description,
                    Ingredients = createRecipe.Ingredients,
                    Directions = createRecipe.Directions,
                    RecipeTips = createRecipe.RecipeTips,
                    Image = createRecipe.Image,
                    NutritionFacts = createRecipe.NutritionFacts,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = createRecipe.CreatedBy,
                };
                await _genericRepositoryRecipe.Create(result);
                return ($"Recipe Created Successfully");
            }
            else
            {
                throw new UnauthorizedAccessException($"Unauthorize User");
            }       
        }
        public async Task<IEnumerable<GetRecipeDTO>> GetRecipes()
        {
            var result = await _genericRepositoryRecipe.GetAll();
            var response = new List<GetRecipeDTO>();
            foreach (var item in result)
            {
                var data = new GetRecipeDTO();
                data.Id = item.Id;
                data.Title = item.Title;
                data.Description = item.Description;
                data.Ingredients = item.Ingredients;
                data.Directions = item.Directions;
                data.RecipeTips = item.RecipeTips;
                data.Image = item.Image;
                data.NutritionFacts = item.NutritionFacts;
                data.CreatedDate = item.CreatedDate;
                data.CreatedBy = item.CreatedBy;
                data.LastUpdatedDate = item.LastUpdatedDate;
                response.Add(data);
            }
            return response;
        }
        public async Task<string> UpdateRecipe(UpdateRecipeDTO updateRecipe)
        {
            var query = _genericRepositoryRecipe.GetQuery();
            var response =  query.Where(e => e.Id == updateRecipe.Id).FirstOrDefault();
            if(response != null)
            {
                if (updateRecipe.CreatedBy == response.CreatedBy)
                {
                    response.Id = updateRecipe.Id;
                    response.Title = updateRecipe.Title;
                    response.Description = updateRecipe.Description;
                    response.Ingredients = updateRecipe.Ingredients;
                    response.Directions = updateRecipe.Directions;
                    response.RecipeTips = updateRecipe.RecipeTips;
                    response.Image = updateRecipe.Image;
                    response.NutritionFacts = updateRecipe.NutritionFacts;
                    response.CreatedBy = updateRecipe.CreatedBy;
                    response.LastUpdatedDate = DateTime.UtcNow;
                    await _genericRepositoryRecipe.Update(response);
                    return "Recipe Updated Successfully";
                }
                else
                {
                    throw new UnauthorizedAccessException($"Unauthorize User");
                }
            }
            else
            {
                throw new KeyNotFoundException($"Recipe Id: {updateRecipe.Id} does not exist");
            }
        }
        public async Task<string> DeleteRecipe(int id)
        {
            var response = await _genericRepositoryRecipe.Delete(id);
            if (response)
                return ($"Recipe Id: {id} successfully deleted");
            else
                throw new KeyNotFoundException($"Id: {id} does not exist");
        }
        public async Task<byte[]> GetImageById(int id)
        {
            var query = _genericRepositoryRecipe.GetQuery();
            var result =  query.Where(e => e.Id == id).FirstOrDefault();
            if (result == null)
            {
                throw new KeyNotFoundException($"Recipe Id: {id} does not exist");
            }
            return result.Image;
        }
        public async Task<GetRecipeDTO> GetRecipeById(int id)
        {
            var query = _genericRepositoryRecipe.GetQuery();
            var result = query.Where(e => e.Id == id).FirstOrDefault();
            if (result == null)
            {
                throw new KeyNotFoundException($"Recipe Id: {id} does not exist");
            }
            var response = new GetRecipeDTO();
            response.Id = result.Id;
            response.Title = result.Title;
            response.Description = result.Description;
            response.Directions = result.Description;
            response.Ingredients = result.Ingredients;
            response.RecipeTips = result.RecipeTips;
            response.Image = result.Image;
            response.NutritionFacts = result.NutritionFacts;
            response.CreatedDate = result.CreatedDate;
            response.CreatedBy = result.CreatedBy;
            response.LastUpdatedDate = result.LastUpdatedDate;
            return response;
        }
    }
}
