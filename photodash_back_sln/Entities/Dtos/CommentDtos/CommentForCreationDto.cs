using System;


namespace Entities.Dtos.CommentDtos
{
    public class CommentForCreationDto
    {
        public string CommentContent { get; set; }

        public Guid OwnerPostId { get; set; }

        public string UserName { get; set; }
    }
}
