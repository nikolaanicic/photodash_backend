using AutoMapper;
using Contracts.Logger;
using Contracts.RepoManager;
using Contracts.Services.IServices;
using Entities.Dtos.CommentDtos;
using Entities.Models;
using Entities.Roles;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.ServiceImplementations
{
    public class CommentsService : ICommentsService
    {


        private UserManager<User> _userManager;
        private ILoggerManager _logger;
        private IMapper _mapper;
        private IRepositoryManager _repository;

        public CommentsService(UserManager<User> userManager,ILoggerManager logger,IMapper mapper,IRepositoryManager repository)
        {
            _mapper = mapper;
            _logger = logger;
            _userManager = userManager;
            _repository = repository;
        }



        public async Task<CommentForReplyDto> CreateComment(string username, Guid postId, CommentForCreationDto commentForCreation)
        {
            var user = await _userManager.FindByNameAsync(username);

            var post = await _repository.Posts.GetPostById(postId, false);
            if(post == null)
            {
                _logger.LogError($"Post doesn't exist ID:{postId}");
                return null;
            }

            var commentEntity = _mapper.Map<Comment>(commentForCreation);
            _repository.Comments.CreateComment(new Guid(user.Id), postId, commentEntity);
            await _repository.SaveAsync();
            
            var mappedComment = _mapper.Map<CommentForReplyDto>(commentEntity);
            
            return mappedComment;
        
        }

        public async Task<bool> DeleteComment(Guid postId,Guid commentId, ClaimsPrincipal currentPrincpal)
        {
            var user = await _userManager.FindByNameAsync(currentPrincpal.Identity.Name);

            var post = await _repository.Posts.GetPostById(postId,false);

            if(post == null)
            {
                _logger.LogError($"Post doesn't exist.ID:{postId}");
                return false;
            }



            var comment = await _repository.Comments.GetComment(postId, commentId, false);

            if(comment == null)
            {
                _logger.LogError($"Comment doesn't exist.Id:{commentId}");
                return false;
            }

            var commentOwner = await _userManager.FindByIdAsync(comment.OwnerPostId.ToString());

            if(!currentPrincpal.IsInRole(RolesHolder.Admin) && !commentOwner.UserName.Equals(currentPrincpal.Identity.Name))
            {
                return false;
            }


                _repository.Comments.RemoveComment(comment);
            await _repository.SaveAsync();

            return true;


        }

        public async Task<IEnumerable<CommentForReplyDto>> GetCommentsForPost(Guid postId)
        {
            var post = await _repository.Posts.GetPostById(postId,false);
        
            if(post == null)
            {
                _logger.LogError($"Post doesn't exist.Id:{postId}");
                return null;
            }

            var comments = await _repository.Comments.GetAllPostComments(postId, false);
            return _mapper.Map<IEnumerable<CommentForReplyDto>>(comments);
        
        }
    }
}
