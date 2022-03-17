
namespace Entities.RequestFeatures
{
    public class PostsRequestParameters : RequestParameters
    {

        public PostsRequestParameters()
        {

        }
        protected override int MaxPageSize => 10;
    }
}
