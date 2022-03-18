using Contracts.Services.IServices;
using Entities.Dtos.CommentDtos;
using Entities.RequestFeatures;
using Entities.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PhotoDash.ActionFilters;
using System;
using System.Net;
using System.Threading.Tasks;

namespace PhotoDash.Controllers
{
    [Route("api/post/{postId}/comments")]
    [ApiController]
    [ServiceFilter(typeof(ValidateModelAttribute))]
    public class CommentsController : ControllerBase
    {

        private ICommentsService _commentsService;

        public CommentsController(ICommentsService commentsService)
        {
            _commentsService = commentsService;
        }


        [HttpGet,Authorize(Roles = RolesHolder.AdminOrUser)]
        public async Task<IActionResult> GetComments(Guid postId,[FromQuery]CommentsRequestParameters commentsRequestParameters)
        {
            var result = await _commentsService.GetCommentsForPostAsync(postId,commentsRequestParameters);
            if (result == null)
                return NotFound();

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(result.MetaData));

            return Ok(result);
        }

        [HttpPost,Authorize(Roles =RolesHolder.User)]

        public async Task<IActionResult> PostComment(Guid postId,[FromBody] CommentForCreationDto commentForCreationDto)
        {

            var currentPrincipal = HttpContext.User;
            var result = await _commentsService.CreateComment(postId,commentForCreationDto,currentPrincipal);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete,Authorize(Roles = RolesHolder.AdminOrUser)]
        public async Task<IActionResult> DeleteComment([FromBody] CommentForDeletionDto commentForDeletion)
        {
            var currentPrincipal = HttpContext.User;

            var result = await _commentsService.DeleteComment(commentForDeletion, currentPrincipal);

            if (result == null)
                return NoContent();

            return (result.Code == HttpStatusCode.Unauthorized.ToString() ? Unauthorized() : NotFound());
        }

        [HttpPut("{commentId}"),Authorize(Roles =RolesHolder.User)]
        public async Task<IActionResult> LikeComment(Guid commentId)
        {
            var result = await _commentsService.LikeComment(commentId);

            if (result == null)
                return NoContent();

            return NotFound();
        }
    }
}
