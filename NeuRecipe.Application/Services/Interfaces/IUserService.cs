using NeuRecipe.Application.DTO;

namespace NeuRecipe.Application.Services.Interfaces
{
    public interface IUserService
    {
        public Task<string> CreateUser(UserDTO user);
        public Task<string> UserLogIn(string Email,string Password);
        Task<IEnumerable<UserDTO>> GetUsers();
    }
}

