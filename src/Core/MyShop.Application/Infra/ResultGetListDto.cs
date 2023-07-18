namespace MyShop.Application.Infra
{
    public abstract class ResultGetListDto<T>
    {
        protected ResultGetListDto() 
        { 
            Data = new List<T>();
        }

        public IList<T> Data { get; set; }
        public int Rows { get; set; }
    }
}
