using AutoMapper;
using Contracts.Logger;
using Contracts.RepoManager;
using Contracts.Services.IServices;
using Entities.Dtos.CommentDtos;
using Entities.Models;
using Entities.RequestFeatures;
using Entities.Roles;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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



        public async Task<CommentForReplyDto> CreateComment(CommentForCreationDto commentForCreation)
        {
            var user = await _userManager.FindByNameAsync(commentForCreation.UserName);

            var post = await _repository.Posts.GetPostById(commentForCreation.OwnerPostId, false);
            if(post == null)
            {
                _logger.LogError($"Post doesn't exist ID:{commentForCreation.OwnerPostId}");
                return null;
            }

            var commentEntity = _mapper.Map<Comment>(commentForCreation);
            _repository.Comments.CreateComment(user.Id, commentForCreation.OwnerPostId, commentEntity);
            await _repository.SaveAsync();
            
            var mappedComment = _mapper.Map<CommentForReplyDto>(commentEntity);
            
            return mappedComment;
        
        }

        public async Task<IdentityError> DeleteComment(CommentForDeletionDto commentForDeletion, ClaimsPrincipal currentPrincpal)
        {
            var user = await _userManager.FindByNameAsync(currentPrincpal.Identity.Name);

            var post = await _repository.Posts.GetPostById(commentForDeletion.PostId,false);

            if(post == null)
            {
                _logger.LogError($"Post doesn't exist.ID:{commentForDeletion.PostId}");
                return new IdentityError { Code = HttpStatusCode.NotFound.ToString() };
            }



            var comment = await _repository.Comments.GetComment(commentForDeletion.PostId, commentForDeletion.commentId, false);

            if(comment == null)
            {
                _logger.LogError($"Comment doesn't exist.Id:{commentForDeletion.commentId}");
                return new IdentityError { Code = HttpStatusCode.NotFound.ToString() };

            }

            var commentOwner = await _userManager.FindByIdAsync(comment.OwnerPostId.ToString());

            if(!currentPrincpal.IsInRole(RolesHolder.Admin) && !commentOwner.UserName.Equals(currentPrincpal.Identity.Name))
            {
                return new IdentityError { Code = HttpStatusCode.NotFound.ToString() };

            }


            _repository.Comments.RemoveComment(comment);
            await _repository.SaveAsync();

            return null;


        }

        public async Task<PagedList<CommentForReplyDto>> GetCommentsForPostAsync(Guid postId, CommentsRequestParameters commentsRequestParameters)
        {

            var post = await _repository.Posts.GetPostById(postId, false);

            if (post == null)
            {
                _logger.LogError($"Post doesn't exist.ID:{postId}");
                return null;
            }

            var comments = await _repository.Comments.GetCommentsAsync(postId, commentsRequestParameters, false);

            return _mapper.Map<PagedList<CommentForReplyDto>>(comments);
        }

        public async Task<IdentityError> LikeComment(Guid commentId)
        {

            var comment = await _repository.Comments.GetCommentByIdAsync(commentId,true);

            if(comment == null)
            {
                _logger.LogError($"Comment doesn't exist:{commentId}");
                return new IdentityError { Code = HttpStatusCode.NotFound.ToString() };
            }

            comment.LikeCount += 1;
            await _repository.SaveAsync();

            return null;
        }
    }
}
