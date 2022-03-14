using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos.UserDtos
{
    public class UserForAuthenticationDto
    {

        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

    }
}
