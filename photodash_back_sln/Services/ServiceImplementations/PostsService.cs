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

            _repository.Posts.CreatePost(new Guid(user.Id), postEntity);
            await _repository.SaveAsync();

            return true;
        }

        public async Task<PostForReplyDto> GetPost(string username,Guid id)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {

                _logger.LogError($"USER NOT FOUND USERNAME:{username}");
                return null;
            }


            var postEntity = await _repository.Posts.GetPost(new Guid(user.Id), id, false);
            if (postEntity == null)
            {
                _logger.LogError($"Post doesn't exist. Username:{username} Post ID:{id}");
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
            var posts = await _repository.Posts.GetPostsAsync(new Guid(user.Id), postRequestParameters, false);

            return _mapper.Map<PagedList<PostForReplyDto>>(posts);
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

            var post = await _repository.Posts.GetPost(new Guid(user.Id), id, false);

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


    }
}
