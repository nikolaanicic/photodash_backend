using Contracts.Logger;
using Contracts.RepoManager;
using Contracts.Services.IServices;
using Entities.Dtos.PostDtos;
using Entities.RequestFeatures;
using Entities.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PhotoDash.ActionFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PhotoDash.Controllers
{
    [Route("api/posts")]
    [ApiController]
    [ServiceFilter(typeof(ValidateModelAttribute))]

    public class PostsController : ControllerBase
    {

        private IPostsService _postsService;


        public PostsController(IPostsService postsService)
        {
            _postsService = postsService;
        }


        [HttpGet("{username}"), Authorize(Roles = RolesHolder.AdminOrUser)]
        public async Task<IActionResult> GetPostsForUser(string username,[FromQuery]PostsRequestParameters postRequestParameters)
        {
            var posts = await _postsService.GetPostsAsync(username,postRequestParameters);

            if (posts == null)
            {
                return NotFound();
            }

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(posts.MetaData));

            return Ok(new { meta = posts.MetaData, data = posts });

        }

        [HttpGet("newest"),Authorize(Roles = RolesHolder.User)]

        public async Task<IActionResult> GetNewestFollowerPosts([FromQuery]PostsRequestParameters postRequestParameters)
        {
            var username = HttpContext.User.Identity.Name;
            var posts = await _postsService.GetNewestAsync(username, postRequestParameters);
            return Ok(new {meta=posts.MetaData, data=posts });
        }

        [HttpPost, Authorize(Roles = RolesHolder.User)]
        public async Task<IActionResult> CreatePost([FromForm] PostForCreationDto newPost)
        {
            var user = HttpContext.User.Identity.Name;
            bool result = await _postsService.CreatePost(newPost, user);

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet("like/{id}"), Authorize(Roles = RolesHolder.User)]
        public async Task<IActionResult> LikePost(Guid id)
        {
            var result = await _postsService.LikePost(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("one/{id}"), Authorize(Roles = RolesHolder.AdminOrUser)]

        public async Task<IActionResult> GetPost(Guid id)
        {

            var post = await _postsService.GetPostAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        [HttpDelete("{id}"), Authorize(Roles = RolesHolder.AdminOrUser)]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            var currentPrincipal = HttpContext.User;
            var result = await _postsService.RemovePost(id, currentPrincipal);

            if (result == null)
                return NoContent();

            return (result.Code == HttpStatusCode.Unauthorized.ToString() ? Unauthorized() : NotFound());
        }
    }
}
