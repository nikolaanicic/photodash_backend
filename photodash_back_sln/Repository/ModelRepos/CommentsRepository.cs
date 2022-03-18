using Contracts.RepoInterfaces;
using Entities.Models;
using Entities.RepoContext;
using Entities.RequestFeatures;
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

        public void CreateComment(string userID, Guid postID, Comment comment)
        {
            comment.OwnerPostId = postID;
            comment.OwnerUserId = userID;
            Create(comment);
        }

        public async Task<PagedList<Comment>> GetCommentsAsync(Guid postID,CommentsRequestParameters commentRequestParams, bool trackChanges)
        {
            var comments = await FindByConditionPaged(x => x.OwnerPostId.Equals(postID), commentRequestParams, trackChanges).ToListAsync();
            var count = await FindByCondition(x => x.OwnerPostId.Equals(postID), trackChanges).CountAsync();

            return new PagedList<Comment>(comments, count, commentRequestParams.PageNumber, commentRequestParams.PageSize);

        }

        public async Task<Comment> GetComment(Guid postID, Guid commentId, bool trackChanges) =>
            await FindByCondition(x => x.OwnerPostId.Equals(postID) && x.Id.Equals(commentId), trackChanges).SingleOrDefaultAsync();

        public void RemoveComment(Comment comment) => Delete(comment);

        public void UpdateComment(Comment comment) => Update(comment);

        public async Task<Comment> GetCommentByIdAsync(Guid commentId, bool trackChanges) =>
            await FindByCondition(x => x.Id.Equals(commentId), trackChanges).SingleOrDefaultAsync();
    }
}
