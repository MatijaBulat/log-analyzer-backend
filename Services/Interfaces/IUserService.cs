using zavrsni_backend.Entities;
using zavrsni_backend.Models.DTO;

namespace zavrsni_backend.Services.Interfaces
{
    public interface IUserService
    {
        public Task<UserTokenDTO> RegisterUser(UserDTO userDto, CancellationToken cancellation);
        public Task<UserTokenDTO> LoginUser(UserDTO userDto, CancellationToken cancellation);
        public Task<User> UserExist(string username, CancellationToken token);
    }
}
