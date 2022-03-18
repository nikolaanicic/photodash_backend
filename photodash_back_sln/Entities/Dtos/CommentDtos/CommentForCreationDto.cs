using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos.CommentDtos
{
    public class CommentForCreationDto
    {
        [Required(ErrorMessage = "Comment content is required")]
        public string CommentContent { get; set; }

    }
}
