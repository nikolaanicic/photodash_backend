using Entities.Dtos.CommentDtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Services.IServices
{
    public interface ICommentsService
    {

        Task<CommentForReplyDto> CreateComment(CommentForCreationDto commentForCreation);
        Task<IdentityError> DeleteComment(CommentForDeletionDto commentForDeletion,ClaimsPrincipal currentPrincpal);
        Task<IEnumerable<CommentForReplyDto>> GetCommentsForPost(Guid postId);

    }
}
