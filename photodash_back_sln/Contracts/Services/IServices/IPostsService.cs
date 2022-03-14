using Entities.Dtos.PostDtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Contracts.Services.IServices
{
    public interface IPostsService
    {
        Task<IActionResult> CreatePost(PostForCreationDto newPost,Guid ownerId);
        Task<IActionResult> RemovePost(Guid ownerId,Guid postId);
        Task<IActionResult> GetPost(Guid userId, Guid postId);
        Task<IActionResult> GetPostsForUser(Guid userId);
    }
}
