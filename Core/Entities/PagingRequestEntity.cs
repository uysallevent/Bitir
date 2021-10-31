namespace Core.Entities
{
    public class PagingRequestEntity<T> where T : class
    {
        private int page;

        public int Page
        {
            get
            {
                return (page < 1) ? 1 : page;
            }
            set { page = value; }
        }

        public int PageSize { get; set; }
        public T Entity { get; set; }
    }
}
