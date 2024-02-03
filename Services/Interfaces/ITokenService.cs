using zavrsni_backend.Entities;

namespace zavrsni_backend.Services.Interfaces
{
    public interface ITokenService
    {
        public string CreateToken(User user, string tokenKey);
    }
}
