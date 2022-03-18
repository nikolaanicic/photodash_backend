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
        Task<IEnumerable<Post>> GetAllPosts(string userId,bool trackChanges);
        Task<PagedList<Post>> GetPostsAsync(string id, PostsRequestParameters postRequestParameters,bool trackChanges);
        Task<Post> GetPost(string userID, Guid postId, bool trackChanges);
        void CreatePost(string userID,Post post);
        void DeletePost(Post post);
        Task<Post> GetPostById(Guid id,bool trackChanges);

    }
}
