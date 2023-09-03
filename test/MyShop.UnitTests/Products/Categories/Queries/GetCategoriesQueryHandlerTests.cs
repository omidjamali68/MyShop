using FluentAssertions;
using MyShop.Application.Services.Products.Categories;
using MyShop.Application.Services.Products.Categories.Queries;
using MyShop.Domain.Aggregates.Products.Entities;
using MyShop.Persistence.Products;
using MyShop.Common.Tests;

namespace MyShop.Application.UnitTests.Products.Categories.Queries
{
    public class GetCategoriesQueryHandlerTests : TestHelp<ICategoryRepository>
    {
        public GetCategoriesQueryHandlerTests()
        {
            Repository = new CategoryRepository(Context);
        }

        [Fact]
        public async Task Handle_should_return_categories_successful()
        {
            var categoryCloths = Category.Create("Cloths").Value;
            Context.SaveEntity(categoryCloths);
            var categoryDairy = Category.Create("Dairy").Value;
            Context.SaveEntity(categoryDairy);
            var query = new GetCategoriesQuery(null, 1);
            var handler = new GetCategoriesQueryHandler(Repository);

            var result = await handler.Handle(query, default);

            result.IsSuccess.Should().BeTrue();
            result.Error.Code.Should().BeEmpty();
            result.Value.Rows.Should().Be(2);
            result.Value.Data.Should().Contain(
                x => x.Id == categoryCloths.Id &&
                x.Name == categoryCloths.Name.Value);
            result.Value.Data.Should().Contain(
                x => x.Id == categoryDairy.Id &&
                x.Name == categoryDairy.Name.Value);
        }

        [Fact]
        public async Task Handle_should_return_categories_with_searchKey_successful()
        {
            var categoryCloths = Category.Create("Cloths").Value;
            Context.SaveEntity(categoryCloths);
            var categoryDairy = Category.Create("Dairy").Value;
            Context.SaveEntity(categoryDairy);
            var query = new GetCategoriesQuery("dai", 1);
            var handler = new GetCategoriesQueryHandler(Repository);

            var result = await handler.Handle(query, default);

            result.IsSuccess.Should().BeTrue();
            result.Error.Code.Should().BeEmpty();
            result.Value.Rows.Should().Be(1);            
            result.Value.Data.Should().Contain(
                x => x.Id == categoryDairy.Id &&
                x.Name == categoryDairy.Name.Value);
        }

        [Fact]
        public async Task Handle_should_return_categories_with_persion_searchKey_successful()
        {
            var categoryCloths = Category.Create("پوشاک").Value;
            Context.SaveEntity(categoryCloths);
            var categoryDairy = Category.Create("لبنیات").Value;
            Context.SaveEntity(categoryDairy);
            var query = new GetCategoriesQuery("لبن", 1);
            var handler = new GetCategoriesQueryHandler(Repository);

            var result = await handler.Handle(query, default);

            result.IsSuccess.Should().BeTrue();
            result.Error.Code.Should().BeEmpty();
            result.Value.Rows.Should().Be(1);
            result.Value.Data.Should().Contain(
                x => x.Id == categoryDairy.Id &&
                x.Name == categoryDairy.Name.Value);
        }
    }
}
