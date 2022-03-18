using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;


namespace Entities.Dtos.PostDtos
{
    public class PostForCreationDto
    {

        [Required(ErrorMessage = "Post's image is required")]
        public IFormFile Image { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Timestamp is required")]
        public DateTime Posted { get; set; }

    }
}
