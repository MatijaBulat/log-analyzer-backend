using zavrsni_backend.Entities;
using zavrsni_backend.Models.DTO;

namespace zavrsni_backend.Services.Interfaces
{
    public interface IUserService
    {
        //public Task<>

        public Task<string> RegisterUser(UserDTO userDto, CancellationToken cancellation);
        public Task<string> LoginUser(UserDTO userDto, CancellationToken cancellation);
        public Task<User> UserExist(string username, CancellationToken token);
    }
}
