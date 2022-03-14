using Contracts.RepoInterfaces;
using System.Threading.Tasks;

namespace Contracts.RepoManager
{
    public interface IRepositoryManager
    {

        public ICommentsRepository Comments { get;}
        public IPostsRepo Posts { get; }
        Task SaveAsync();
    }
}
