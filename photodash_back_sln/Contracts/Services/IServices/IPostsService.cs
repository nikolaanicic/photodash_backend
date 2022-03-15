using Entities.Dtos.PostDtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Contracts.Services.IServices
{
    public interface IPostsService
    {
        Task<bool> CreatePost(PostForCreationDto newPost,string username);
        Task<IdentityError> RemovePost(string username,Guid id,ClaimsPrincipal currentPrincipal);
        Task<PostForReplyDto> GetPost(string username,Guid id);
        Task<IEnumerable<PostForReplyDto>> GetPostsForUser(string username);
        Task<bool> LikePost(Guid postId);
    }
}
