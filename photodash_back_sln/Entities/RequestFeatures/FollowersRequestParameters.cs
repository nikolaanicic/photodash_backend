
namespace Entities.RequestFeatures
{
    public class FollowersRequestParameters : RequestParameters 
    {

        public FollowersRequestParameters()
        {

        }
        protected override int MaxPageSize => 10;

    }
}
