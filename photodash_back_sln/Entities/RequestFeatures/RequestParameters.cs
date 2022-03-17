
namespace Entities.RequestFeatures
{
    public abstract class RequestParameters
    {

        public RequestParameters()
        {

        }
        protected virtual int MaxPageSize { get; } = 5;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        public int PageSize { get => _pageSize; set => _pageSize = value > MaxPageSize ? MaxPageSize : value; }
    }
}
