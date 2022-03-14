using Contracts.RepoInterfaces;
using Entities.Models;
using Entities.RepoContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.ModelRepos
{
    public class PostsRepository :RepositoryBase<Post>, IPostsRepo
    {
        public PostsRepository(RepositoryContext dbContext) : base(dbContext)
        {
        }

        public void CreatePost(Guid userID, Post post)
        {
            post.OwnerId = userID;
            Create(post);
        }

        public void DeletePost(Post post) => Delete(post);

        public async Task<IEnumerable<Post>> GetAllPosts(Guid userId, bool trackChanges) =>
            await FindByCondition(x => x.OwnerId.Equals(userId), trackChanges).ToListAsync();

        public async Task<Post> GetPost(Guid userID, Guid postId, bool trackChanges) =>
            await FindByCondition(x => x.OwnerId.Equals(userID) && x.Id.Equals(postId), trackChanges)
            .SingleOrDefaultAsync();
    }
}
