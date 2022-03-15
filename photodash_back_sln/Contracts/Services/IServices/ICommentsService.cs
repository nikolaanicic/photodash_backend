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

        Task<CommentForReplyDto> CreateComment(string username, Guid postId, CommentForCreationDto commentForCreation);
        Task<bool> DeleteComment(Guid postId,Guid commentId, ClaimsPrincipal currentPrincpal);
        Task<IEnumerable<CommentForReplyDto>> GetCommentsForPost(Guid postId);

    }
}
