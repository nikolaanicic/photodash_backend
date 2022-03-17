using Contracts.Services.IServices;
using Entities.Dtos.UserDtos;
using Entities.RequestFeatures;
using Entities.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PhotoDash.ActionFilters;
using System.Net;
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
            return Ok(await _userService.AuthenticateUser(authUser));
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

        [HttpGet("followers"),Authorize(Roles = RolesHolder.User)]
        public async Task<IActionResult> GetFollowers([FromQuery]FollowersRequestParameters queryParams)
        {
            var currentPrincipal = HttpContext.User;
            var followers = await _userService.GetFollowersAsync(currentPrincipal, queryParams);

            if(followers == null)
            {
                return BadRequest();
            }
        
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(followers.MetaData));

            return Ok(followers);
        }


        [HttpPut("follow/{username}"),Authorize]
        public async Task<IActionResult> Follow(string username)
        {
            var currentPrincipal = HttpContext.User;

            var result = await _userService.Follow(username, currentPrincipal);

            if (result == null)
                return NoContent();

            return (result.Code == HttpStatusCode.BadRequest.ToString() ? BadRequest() : NotFound());
        }

        [HttpPut("unfollow/{username}"), Authorize(Roles = RolesHolder.User)]
        public async Task<IActionResult> Unfollow(string username)
        {
            var currentPrincipal = HttpContext.User;

            var result = await _userService.Unfollow(username, currentPrincipal);

            if (result == null)
                return NoContent();

            return (result.Code == HttpStatusCode.BadRequest.ToString() ? BadRequest() : NotFound());

        }
    }
}
