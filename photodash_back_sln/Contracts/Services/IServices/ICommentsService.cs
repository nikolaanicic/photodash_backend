using Entities.Dtos.CommentDtos;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Contracts.Services.IServices
{
    public interface ICommentsService
    {

        Task<CommentForReplyDto> CreateComment(CommentForCreationDto commentForCreation);
        Task<IdentityError> DeleteComment(CommentForDeletionDto commentForDeletion,ClaimsPrincipal currentPrincpal);
        Task<PagedList<CommentForReplyDto>> GetCommentsForPostAsync(Guid postId,CommentsRequestParameters commentsRequestParameters);
        Task<IdentityError> LikeComment(Guid commentId);

    }
}
