namespace MyShop.Common.Dto
{
    public record GetListResponse<T>
    {
        protected GetListResponse() 
        { 
            Data = new List<T>();
        }

        public IEnumerable<T> Data { get; set; }
        public int Rows { get; set; }
    }
}
