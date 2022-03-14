using AutoMapper;
using Contracts.Logger;
using Contracts.RepoManager;
using Contracts.Services.IServices;
using Contracts.Services.ImagesService;
using Entities.Dtos.PostDtos;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Services.ServiceImplementations
{
    public class PostsService : IPostsService
    {

        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private IMapper _mapper;
        private IImageService _imagesService;
        private UserManager<User> _userManager;

        public PostsService(IRepositoryManager repo,ILoggerManager logger,IMapper mapper,IImageService imagesService,UserManager<User> userManager)
        {
            _repository = repo;
            _logger = logger;
            _mapper = mapper;
            _imagesService = imagesService;
            _userManager = userManager;

        }

        public async Task<IActionResult> CreatePost(PostForCreationDto newPost, Guid ownerId)
        {
            var imagePath = await _imagesService.SaveImage(newPost.Image);
            var postEntity = _mapper.Map<Post>(newPost);
            postEntity.ImagePath = imagePath;
            _repository.Posts.CreatePost(ownerId, postEntity);

            return new NoContentResult();
        }

        public async Task<IActionResult> GetPost(Guid userId, Guid postId)
        {
            var postEntity = await _repository.Posts.GetPost(userId, postId, false);
            if (postEntity == null)
            {
                LogPostNotExists(userId,postId);
                return new NotFoundResult();
            }

            var postToReturn = _mapper.Map<PostForReplyDto>(postEntity);

            return new OkObjectResult(postToReturn);
        }

        public async Task<IActionResult> GetPostsForUser(Guid userId)
        {

            //deo za proveru postojanja korisnika izvuci u filtere. kako primeniti filtere unutar servisa?
            var user = await _userManager.FindByIdAsync(userId.ToString());
            
            /*
            if (user == null)
            {
                _logger.LogError($"USER NOT FOUND ID:{userId}");
                return new NotFoundObjectResult(new { userId = userId });
            }
            */

            var posts = await _repository.Posts.GetAllPosts(userId, false);
            var mappedPosts = _mapper.Map<IEnumerable<PostForReplyDto>>(posts);
            return new OkObjectResult(mappedPosts);
        
        
        }

        public async Task<IActionResult> RemovePost(Guid ownerId, Guid postId)
        {

            var user = await _userManager.FindByIdAsync(ownerId.ToString());

            if (user == null)
            {
                LogPostNotExists(ownerId, postId);
                return new NotFoundResult();
            }

            var post = await _repository.Posts.GetPost(ownerId, postId, false);

            if (post == null)
            {
                LogPostNotExists(ownerId,postId);
                return new NotFoundResult();
            }

            _repository.Posts.DeletePost(post);
            return new NoContentResult();
        }

        private void LogPostNotExists(Guid ownerId,Guid postId)
        {
            var message = $"POST DOESN'T EXIST!USER:{ownerId} POST:{postId}";
            _logger.LogError(message);
        }
    }
}
