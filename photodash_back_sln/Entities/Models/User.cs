using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;


namespace Entities.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public IList<Post> Posts { get; set; }
        public IList<Comment> Comments { get; set; }

        public IList<User> Followers { get; set; }

    }
}
