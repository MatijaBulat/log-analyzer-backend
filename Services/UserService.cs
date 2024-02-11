using AutoMapper;
using Microsoft.EntityFrameworkCore;
using zavrsni_backend.Entities;
using zavrsni_backend.ErrorModels;
using zavrsni_backend.Models.DTO;
using zavrsni_backend.Persistence;
using zavrsni_backend.Services.Interfaces;

namespace zavrsni_backend.Services
{
    public class UserService : IUserService
    {
        private ZavrsniRadDBContext _dbContext;
        private IMapper _mapper;
        private ITokenService _tokenService;
        private IConfiguration _configuration;

        private const string TokenKey = "Jwt:Key";

        public UserService(ZavrsniRadDBContext zavrsniRadDBContext, IMapper mapper, ITokenService tokenService, IConfiguration configuration)
        {
            _dbContext = zavrsniRadDBContext;
            _mapper = mapper;
            _tokenService = tokenService;
            _configuration = configuration;
        }

        public async Task<UserTokenDTO> LoginUser(UserDTO userDto, CancellationToken cancellation)
        {
            var user = await UserExist(userDto.Username, cancellation);

            if (user is null || !BCrypt.Net.BCrypt.Verify(userDto.Password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid username or password!");
            }

            return _tokenService.CreateToken(user, _configuration.GetSection(TokenKey).Value);
        }

        public async Task<UserTokenDTO> RegisterUser(UserDTO userDto, CancellationToken cancellation)
        {
            var user = await UserExist(userDto.Username, cancellation);

            if (user is not null)
            {
                throw new AppCustomException("Username already exists!");
            }

            user = _mapper.Map<User>(userDto);

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync(cancellation);

            return _tokenService.CreateToken(user, _configuration.GetSection(TokenKey).Value);
        }

        public async Task<User> UserExist(string username, CancellationToken cancellation)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(user => user.Username == username, cancellation);
        }
    }
}
