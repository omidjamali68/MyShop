namespace MyShop.Domain.SeedWork
{
    public abstract class Entity
    {
        public Entity()
        {
            Result = new Result();
        }
        public int Id { get; set; }
        public Result Result { get; }
    }
}
