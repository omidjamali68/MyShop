using MyShop.Domain.SeedWork;
using MyShop.Domain.SharedKernel;

namespace MyShop.Domain.Aggregates.Products.Entities
{
    public sealed class Category : Entity
    {
        private HashSet<Category> _subCategories = new();

        public Name Name { get; private set; }        

        public Category ParentCategory { get; private set; }
        public int? ParentCategoryId { get; private set; }

        public IReadOnlyCollection<Category> SubCategories => _subCategories;

        private Category()
        {            
        }

        public static Result<Category> Create(string name, int? parentCategoryId = null)
        {
            var category = new Category();

            var nameResult = Name.Create(name);

            if (nameResult.IsFailure)
            {
                return Result.Failure<Category>(nameResult.Error);
            }

            category.Name = nameResult.Value;
            category.ParentCategoryId = parentCategoryId;
            return category;
        }

    }
}
