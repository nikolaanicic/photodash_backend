using Contracts.Services.IServices;
using Entities.Dtos.UserDtos;
using Entities.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoDash.ActionFilters;
using System.Threading.Tasks;

namespace PhotoDash.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        [ServiceFilter(typeof(ValidateModelAttribute))]
        public async Task<IActionResult> Login([FromBody]UserForAuthenticationDto authUser)
        {
            var token = await _userService.AuthenticateUser(authUser);

            if(token == null)
            {
                return NotFound();
            }
            return Ok(token);
        }

        [HttpPost("register")]
        [ServiceFilter(typeof(ValidateModelAttribute))]
        
        public async Task<IActionResult> Register([FromBody] UserForCreationDto newUser)
        {
            var result = await _userService.RegisterUser(newUser);

            if (result == null)
                return Ok();

            return BadRequest(result);
        }

        [HttpDelete("delete"),Authorize(Roles = RolesHolder.AdminOrUser)]
        [ServiceFilter(typeof(ValidateModelAttribute))]

        public async Task<IActionResult> DeleteUser([FromBody] UserForDeletionDto userForDeletion)
        {
            var currentPrincipal = HttpContext.User;

            var result = await _userService.DeleteUser(userForDeletion, currentPrincipal);

            if (result == null)
                return NoContent();

            return BadRequest(result);
        }

        [HttpGet("{username}"),Authorize(Roles = RolesHolder.AdminOrUser)]
        public async Task<IActionResult> GetUserInfo(string username)
        {
            var userResult = await _userService.GetUser(username);

            if (userResult == null)
            {
                return NotFound();
            }

            return Ok(userResult);
        }

    }
}
