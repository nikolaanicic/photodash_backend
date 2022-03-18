using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.CommentDtos
{
    public class CommentForReplyDto
    {
        public Guid Id { get; set; }
        public string CommentContent { get; set; }
        public int LikeCount { get; set; }
    }
}
