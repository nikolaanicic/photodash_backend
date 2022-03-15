using Contracts.Services.IServices;
using Entities.Dtos.CommentDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoDash.ActionFilters;
using System;
using System.Collections.Generic;
using System.Linq;
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


        [HttpGet]
        public async Task<IActionResult> GetComments(Guid postId)
        {
            var result = await _commentsService.GetCommentsForPost(postId);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostComment([FromBody] CommentForCreationDto commentForCreationDto)
        {
            var result = await _commentsService.CreateComment(commentForCreationDto);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteComment([FromBody] CommentForDeletionDto commentForDeletion)
        {
            var currentPrincipal = HttpContext.User;

            var result = await _commentsService.DeleteComment(commentForDeletion, currentPrincipal);

            if (result == null)
                return NoContent();

            return (result.Code == HttpStatusCode.Unauthorized.ToString() ? Unauthorized() : NotFound());
        }
    }
}
