using Microsoft.AspNetCore.Mvc;
using zavrsni_backend.Models.DTO;
using zavrsni_backend.Services.Interfaces;

namespace zavrsni_backend.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser([FromBody] UserDTO userDto, CancellationToken cancellationToken)
        {
            var userToken = await _userService.LoginUser(userDto, cancellationToken);

            return Ok(userToken);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserDTO userDto, CancellationToken cancellationToken)
        {
            var userToken = await _userService.RegisterUser(userDto, cancellationToken);

            return Ok(userToken);
        }
    }
}
