using zavrsni_backend.Entities;
using zavrsni_backend.Models.DTO;

namespace zavrsni_backend.Services.Interfaces
{
    public interface ITokenService
    {
        public UserTokenDTO CreateToken(User user, string tokenKey);
    }
}
