using MyShop.Domain.SeedWork;
using MyShop.Domain.SharedKernel;

namespace MyShop.Domain.Aggregates.Products.Entities
{
    public class Category : Entity
    {
        public Name Name { get; private set; }

        public virtual Category ParentCategory { get; private set; }
        public int? ParentCategoryId { get; private set; }
        
        public virtual ICollection<Category> SubCategories { get; private set; }

        private Category()
        {            
        }

        public static Category Create(string name, int? parentCategoryId)
        {
            var category = new Category();

            var nameResult = Name.Create(name);

            if (!nameResult.Result.IsSucces)
            {
                category.Result.SetErrors(nameResult.Result.Messeges);
                return category;
            }

            category.Name = nameResult;
            category.ParentCategoryId = parentCategoryId;
            return category;
        }

    }
}
