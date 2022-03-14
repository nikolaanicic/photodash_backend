using Contracts.RepoInterfaces;
using Entities.Models;
using Entities.RepoContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.ModelRepos
{
    public class CommentsRepository : RepositoryBase<Comment>, ICommentsRepository
    {
        public CommentsRepository(RepositoryContext dbContext) : base(dbContext)
        {
        }

        public void CreateComment(Guid userID, Guid postID, Comment comment)
        {
            comment.OwnerPostId = postID;
            comment.OwnerUserId = userID;
            Create(comment);
        }

        public async Task<IEnumerable<Comment>> GetAllPostComments(Guid postID, bool trackChanges) =>
            await FindByCondition(x => x.OwnerPostId.Equals(postID), trackChanges).ToListAsync();

        public async Task<Comment> GetComment(Guid postID, Guid commentId, bool trackChanges) =>
            await FindByCondition(x => x.OwnerPostId.Equals(postID) && x.Id.Equals(commentId), trackChanges).SingleOrDefaultAsync();

        public void RemoveComment(Comment comment) => Delete(comment);

        public void UpdateComment(Comment comment) => Update(comment);
    }
}
