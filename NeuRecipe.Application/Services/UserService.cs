using NeuRecipe.Application.DTO;
using NeuRecipe.Application.Exceptions;
using NeuRecipe.Application.Services.Interfaces;
using NeuRecipe.Domain.Entity;
using NeuRecipe.Infrastructure.Repositories.Interfaces;

namespace NeuRecipe.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _genericRepositoryUser;

        public UserService(IGenericRepository<User> genericRepositoryUser)
        {
            _genericRepositoryUser = genericRepositoryUser;
        }
        public async Task <string> CreateUser(UserDTO createUser)
        {
            var query = _genericRepositoryUser.GetQuery();
            var result = query.Where(e => e.Email == createUser.Email).FirstOrDefault();
            if (result == null)
            {
                var userData = new User
                {
                    Name = createUser.Name,
                    Email = createUser.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(createUser.Password)
                };
                await _genericRepositoryUser.Create(userData);
                return ($"User Created Successfully");
            }
            else
            {
                throw new DuplicateException("User Already Exist");
            }
        }
            public async Task<string> UserLogIn(string Email, string Password)
        {
            var query = _genericRepositoryUser.GetQuery();
            var result = query.Where(e => e.Email == Email).FirstOrDefault();
            if(result != null) { 
            var verified = BCrypt.Net.BCrypt.Verify(Password, result.Password);
            if (verified)
            {
                return result.Email;
            }
            else
            {
                throw new UnauthorizedAccessException("Please log in with valid credentials");
            }
            }
            else
            {
                throw new UnauthorizedAccessException("Please log in with valid credentials");

            }
        }
        public async Task<IEnumerable<UserDTO>> GetUsers()
        {
            var result = await _genericRepositoryUser.GetAll();
            var response = new List<UserDTO>();
            foreach (var item in result)
            {
                var data = new UserDTO();
                data.Name = item.Name;
                data.Email = item.Email;
                data.Password = item.Password;
                response.Add(data);
            }
            return response;
        }
    }
}
