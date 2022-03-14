﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        
        [Required(ErrorMessage = "Image is required in a post")]
        public string ImagePath { get; set; }

        public IList<Comment> Comments { get; set; } 
        public int LikeCount { get; set; }

        [Required(ErrorMessage = "Timestamp is required")]
        public DateTime Posted { get; set; }

        public Guid OwnerId { get; set; }
        public User Owner { get; set; }

    }
}
