
namespace Entities.RequestFeatures
{
    public class CommentsRequestParameters : RequestParameters
    {

        public CommentsRequestParameters()
        {

        }
        protected override int MaxPageSize => 10;
    }
}
