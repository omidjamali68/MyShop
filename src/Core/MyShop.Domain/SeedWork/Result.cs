namespace MyShop.Domain.SeedWork
{
    public abstract class Result<T>
    {
        public bool IsSucces { get; set; }
        public HashSet<string>? Messeges { get; set; }
        public T? Data { get; set; }
    }

    public class Result
    {
        public Result()
        {
            Messeges = new HashSet<string>();
            IsSucces = true;
        }

        public bool IsSucces { get; private set; }
        public HashSet<string> Messeges { get; private set; }   
        
        public void WithError(string errorMessage)
        {
            Messeges.Add(errorMessage);
            IsSucces = false;
        }

        public void SetErrors(HashSet<string> messeges)
        {
            Messeges = messeges;
            IsSucces = false;
        }
    }
}
