using Entities.Models;
using Entities.RepoContext;
using Entities.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Entities.Configuration
{
    public static class Configuration 
    {
        public static void Seed(RepositoryContext context)
        {

            var hasher = new PasswordHasher<User>();
            var allRoles = new List<IdentityRole> { new IdentityRole(RolesHolder.Admin), new IdentityRole(RolesHolder.AdminOrUser), new IdentityRole(RolesHolder.User) };

            foreach(var role in allRoles)
            {
                if(!context.Roles.Contains(role))
                {
                    context.Roles.Add(role);
                }
            }

            var defaultUser = new User();
            defaultUser.UserName = "admin";
            defaultUser.PasswordHash = hasher.HashPassword(null, "admin");

            if(!context.Users.Contains(defaultUser))
            {
                context.Users.Add(defaultUser);
            }

            var adminRole = context.Roles.AsEnumerable().Where(x => x.Name.Equals(RolesHolder.Admin)).Single();

            var identityUserRole = new IdentityUserRole<string>();
            defaultUser = context.Users.Single(x => x.UserName.Equals("admin"));

            identityUserRole.RoleId = adminRole.Id;
            identityUserRole.UserId = defaultUser.Id;

            if(!context.UserRoles.Contains(identityUserRole))
            {
                context.UserRoles.Add(identityUserRole);
            }

            context.SaveChanges();
        }


    }
}
