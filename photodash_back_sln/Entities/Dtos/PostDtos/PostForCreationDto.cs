using System;
using System.ComponentModel.DataAnnotations;


namespace Entities.Dtos.PostDtos
{
    public class PostForCreationDto
    {

        [Required(ErrorMessage = "Post's image is required")]
        public string Image { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Timestamp is required")]
        public DateTime Posted { get; set; }

        [Required(ErrorMessage = "Post's owner is required")]
        public Guid OwnerId { get; set; }

    }
}
