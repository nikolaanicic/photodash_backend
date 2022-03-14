using Contracts.Logger;
using Contracts.RepoManager;
using Contracts.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoDash.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {

        private IPostsService _postsService;
        
        
        public PostsController(IPostsService postsService)
        {
            _postsService = postsService;
        }



        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var mockUserId = new Guid("378AE164-FFEE-46F9-9322-F87F9119F94C");

            return await _postsService.GetPostsForUser(mockUserId);
        }

    }
}
