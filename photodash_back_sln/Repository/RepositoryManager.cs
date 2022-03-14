using Contracts.RepoInterfaces;
using Contracts.RepoManager;
using Entities.RepoContext;
using Repository.ModelRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {

        private RepositoryContext _context;

        private object _commentsRepoLock = new object();
        private object _postsRepoLock = new object();
        
        
        private ICommentsRepository _comments;
        private IPostsRepo _posts;

        public RepositoryManager(RepositoryContext dbContext)
        {
            _context = dbContext;
        }


        public ICommentsRepository Comments 
        {
            get
            {
                if(_comments == null)
                {
                    lock(_commentsRepoLock)
                    {
                        if(_comments == null)
                        {
                            _comments = new CommentsRepository(_context);
                        }
                    }
                }

                return _comments;
            }
        }

        public IPostsRepo Posts 
        { 
            get
            {
                if (_posts == null)
                {
                    lock (_postsRepoLock)
                    {
                        if (_posts == null)
                        {
                            _posts = new PostsRepository(_context);
                        }
                    }
                }

                return _posts;
            }
        }

        public Task SaveAsync() => _context.SaveChangesAsync();
    }
}
