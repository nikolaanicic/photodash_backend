using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Entities.Models
{
    public class Comment
    {

        public Guid Id { get; set; }

        [Required(ErrorMessage = "Comment Content is required")]
        public string CommentContent { get; set; }
        public int LikeCount { get; set; }

        public IList<Comment> SubComments { get; set; }

        public Guid OwnerPostId { get; set; }
        public Post OwnerPost { get; set; }

        public Guid OwnerUserId { get; set; }
        public User OwnerUser { get; set; }

    }
}
