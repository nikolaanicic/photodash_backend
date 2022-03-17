using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.RepoInterfaces
{
    public interface IPostsRepo
    {
        Task<IEnumerable<Post>> GetAllPosts(Guid userId,bool trackChanges);
        Task<PagedList<Post>> GetPostsAsync(Guid id, PostsRequestParameters postRequestParameters,bool trackChanges);
        Task<Post> GetPost(Guid userID, Guid postId, bool trackChanges);
        void CreatePost(Guid userID,Post post);
        void DeletePost(Post post);
        Task<Post> GetPostById(Guid id,bool trackChanges);

    }
}
