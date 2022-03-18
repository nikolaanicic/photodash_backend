using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Configuration
{

    public class PostsConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasData(new Post
            {
                Id = new Guid("A798B7F7-E78C-4F65-808C-84AB531B0EE0"),
                ImagePath = "Putanja",
                LikeCount = 1,
                Description = "post",
                OwnerId = "378AE164-FFEE-46F9-9322-F87F9119F94C",
                Posted = DateTime.Now,
            }) ;
        }
    }
}
