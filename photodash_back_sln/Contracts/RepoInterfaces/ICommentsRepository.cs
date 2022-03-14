using Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.RepoInterfaces
{
    public interface ICommentsRepository
    {
        Task<IEnumerable<Comment>> GetAllPostComments(Guid postID, bool trackChanges);
        Task<Comment> GetComment(Guid postID, Guid commentId, bool trackChanges);

        void CreateComment(Guid userID, Guid postID, Comment comment);
        void RemoveComment(Comment comment);
        void UpdateComment(Comment comment);
    }
}
