using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {

        public void Configure(EntityTypeBuilder<User> builder)
        {
            var hasher = new PasswordHasher<User>();

            var defaultAdmin = new User
            {
                Id = "4E7E18D2-0208-4F4D-86DC-E86492A69806",
                UserName = "admin",
                LastName = "Prezime",
                FirstName = "Ime",
                PasswordHash = hasher.HashPassword(null, "admin"),
            };

            builder.HasData(defaultAdmin);
        }
    }
}
