using System;


namespace Entities.Dtos.PostDtos
{
    public class PostForReplyDto
    {
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public DateTime Posted { get; set; }
        public int LikeCount { get; set; }
        public Guid Id { get; set; }
    }
}
