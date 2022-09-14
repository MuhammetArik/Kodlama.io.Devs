using Application.Features.Users.Commands.UserRegister;
using Application.Features.Users.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginQuery userLoginQuery)
        {
            var result = await Mediator.Send(userLoginQuery);
            return Ok(result);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterCommand userRegisterCommand)
        {
            var result = await Mediator.Send(userRegisterCommand);
            return Ok(result);
        }
    }
}
