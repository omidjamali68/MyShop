namespace MyShop.Common.Dto
{
    public class ServiceResultDto<T>
    {
        private ServiceResultDto(T data)
        {
            Message = new List<string>();
            Data = data;
        }

        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public IList<string> Message { get; set; }

        public static ServiceResultDto<T> Create(T data)
        {
            return new ServiceResultDto<T>(data);
        }

    }

    public class ServiceResultDto
    {
        private ServiceResultDto()
        {
            Message = new List<string>();  
            IsSuccess = true;
        }

        public bool IsSuccess { get; set; }
        public IList<string> Message { get; set; }

        public static ServiceResultDto Create()
        {
            return new ServiceResultDto();
        }

        public void SetErrors(HashSet<string> messeges)
        {
            Message = messeges.ToList();
            IsSuccess = false;
        }
    }
}
