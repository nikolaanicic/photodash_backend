using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Threading.Tasks;

namespace Contracts.RepoInterfaces
{
    public interface ICommentsRepository
    {
        Task<PagedList<Comment>> GetCommentsAsync(Guid postID, CommentsRequestParameters commentRequestParams, bool trackChanges);

        Task<Comment> GetComment(Guid postID, Guid commentId, bool trackChanges);

        Task<Comment> GetCommentByIdAsync(Guid commentId,bool trackChanges);

        void CreateComment(string userID, Guid postID, Comment comment);
        void RemoveComment(Comment comment);
        void UpdateComment(Comment comment);

    }
}
