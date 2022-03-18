using AutoMapper;
using Contracts.Logger;
using Contracts.RepoManager;
using Contracts.Services.IServices;
using Contracts.Services.ImagesService;
using Entities.Dtos.PostDtos;
using Entities.Models;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using Entities.Roles;
using System.Net;
using Entities.RequestFeatures;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Services.ServiceImplementations
{
    public class PostsService : IPostsService
    {

        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private IMapper _mapper;
        private IImageService _imagesService;
        private UserManager<User> _userManager;

        public PostsService(IRepositoryManager repo, ILoggerManager logger, IMapper mapper, IImageService imagesService, UserManager<User> userManager)
        {
            _repository = repo;
            _logger = logger;
            _mapper = mapper;
            _imagesService = imagesService;
            _userManager = userManager;

        }

        public async Task<bool> CreatePost(PostForCreationDto newPost, string username)
        {

            var imagePath = await _imagesService.SaveImage(newPost.Image);
            var postEntity = _mapper.Map<Post>(newPost);
            postEntity.ImagePath = imagePath;

            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
                return false;

            _repository.Posts.CreatePost(user.Id, postEntity);
            await _repository.SaveAsync();

            return true;
        }

        public async Task<PostForReplyDto> GetPostAsync(Guid id)
        {
            var postEntity = await _repository.Posts.GetPostById(id, false);
            if (postEntity == null)
            {
                _logger.LogError($"Post doesn't exist.Post ID:{id}");
                return null;
            }

            return _mapper.Map<PostForReplyDto>(postEntity);
        }

        public async Task<bool> LikePost(Guid postId)
        {
            var post = await _repository.Posts.GetPostById(postId, true);

            if (post == null)
                return false;

            post.LikeCount += 1;
            await _repository.SaveAsync();
            return true;
        }

        public async Task<PagedList<PostForReplyDto>> GetPostsAsync(string username,PostsRequestParameters postRequestParameters)
        {
            var user = await _userManager.FindByNameAsync(username);
            var posts = await _repository.Posts.GetPostsAsync(user.Id, postRequestParameters, false);

            var mapped = _mapper.Map < List<PostForReplyDto>>(posts);

            return new PagedList<PostForReplyDto>(mapped, posts.MetaData.TotalCount, postRequestParameters.PageNumber, postRequestParameters.PageSize);
        }

        public async Task<IdentityError> RemovePost(string username, Guid id, ClaimsPrincipal currentPrincipal)
        {
            if (!currentPrincipal.IsInRole(RolesHolder.Admin) && !username.Equals(currentPrincipal.Identity.Name))
            {
                return  new IdentityError { Code = HttpStatusCode.Unauthorized.ToString(), Description = HttpStatusCode.Unauthorized.ToString()} ;
            }

            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                _logger.LogError($"User doesn't exist. Username:{username}");
                return  new IdentityError { Code = HttpStatusCode.NotFound.ToString(), Description = HttpStatusCode.NotFound.ToString() } ;
            }

            var post = await _repository.Posts.GetPost(user.Id, id, false);

            if (post == null)
            {
                _logger.LogError($"Post doesn't exist. Username:{username} Post ID:{id}");
                return new IdentityError { Code = HttpStatusCode.NotFound.ToString(), Description = HttpStatusCode.NotFound.ToString() };

            }

            _repository.Posts.DeletePost(post);
            await _imagesService.RemoveImage(post.ImagePath);
            await _repository.SaveAsync();

            return null;
        }

        public async Task<PagedList<PostForReplyDto>> GetNewestAsync(string username,PostsRequestParameters postsRequestParameters)
        {
            var user = await _userManager.Users.Where(x => x.UserName.Equals(username))
                .Include(u => u.Followers).ThenInclude(f=>f.Posts)
                .SingleAsync();


            var followers = user.Followers;

            var followedPosts = new List<Post>();

            foreach(var following in followers)
            {
                followedPosts.AddRange(await _repository.Posts.GetAllPosts(following.Id, false));
            }


            var posts = followedPosts
                .OrderBy(p=>p.Posted)
                .Skip((postsRequestParameters.PageNumber-1)*postsRequestParameters.PageSize)
                .Take(postsRequestParameters.PageSize).ToList();

            var count = followedPosts.Count;
            var mapped = _mapper.Map<IEnumerable<PostForReplyDto>>(posts);

            return new PagedList<PostForReplyDto>(mapped.ToList(),count, postsRequestParameters.PageNumber, postsRequestParameters.PageSize);
        }
    }
}
