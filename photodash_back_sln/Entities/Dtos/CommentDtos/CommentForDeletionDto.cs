using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.CommentDtos
{
    public class CommentForDeletionDto
    {
        [Required(ErrorMessage = "Post Id is required")]
        public Guid PostId { get; set; }
        
        
        [Required(ErrorMessage = "Comment Id is required")]
        public Guid commentId { get; set; }
    }
}
