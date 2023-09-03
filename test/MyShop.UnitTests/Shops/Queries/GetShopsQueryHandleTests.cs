using MyShop.Application.Services.Shops;
using MyShop.Persistence.Shops;
using MyShop.Domain.Aggregates.Shops;
using MyShop.Application.Services.Shops.Queries.GetShops;
using FluentAssertions;
using MyShop.Common.Tests;

namespace MyShop.Application.UnitTests.Shops.Queries
{
    public class GetShopsQueryHandleTests : TestHelp<IShopRepository>
    {

        public GetShopsQueryHandleTests()
        {            
            Repository = new ShopRepository(Context);
        }

        [Fact]
        public async Task Handle_should_return_success()
        {
            var shopAmin = Shop.Create("Amin", "Shiraz");
            Context.SaveEntity(shopAmin.Value);
            var shopOmid = Shop.Create("Omid", "Tehran");
            Context.SaveEntity(shopOmid.Value);
            var query = new GetShopsQuery(null, 1);
            var handler = new GetShopsQueryHandler(Repository);

            var expected = await handler.Handle(query, default);

            expected.Value.Rows.Should().Be(2);
            expected.Value.Data.Should().Contain(
                x => x.Name == (string)shopAmin.Value.Name && 
                x.Address == shopAmin.Value.Address &&
                x.IsActive == shopAmin.Value.IsActive);
            expected.Value.Data.Should().Contain(
                x => x.Name == (string)shopOmid.Value.Name &&
                x.Address == shopOmid.Value.Address &&
                x.IsActive == shopOmid.Value.IsActive);
        }

        [Fact]
        public async Task Handle_should_return_success_with_english_searchKey()
        {
            var shopAmin = Shop.Create("Amin", "Shiraz");
            Context.SaveEntity(shopAmin.Value);
            var shopOmid = Shop.Create("Omid", "Tehran");
            Context.SaveEntity(shopOmid.Value);
            var query = new GetShopsQuery("om", 1);
            var handler = new GetShopsQueryHandler(Repository);

            var expected = await handler.Handle(query, default);

            expected.Value.Rows.Should().Be(1);            
            expected.Value.Data.Should().Contain(
                x => x.Name == (string)shopOmid.Value.Name &&
                x.Address == shopOmid.Value.Address &&
                x.IsActive == shopOmid.Value.IsActive);
        }

        [Fact]
        public async Task Handle_should_return_success_with_persion_searchKey()
        {
            var shopAmin = Shop.Create("امین", "شیراز");
            Context.SaveEntity(shopAmin.Value);
            var shopOmid = Shop.Create("امید", "تهران");
            Context.SaveEntity(shopOmid.Value);
            var query = new GetShopsQuery("امید", 1);
            var handler = new GetShopsQueryHandler(Repository);

            var expected = await handler.Handle(query, default);

            expected.Value.Rows.Should().Be(1);
            expected.Value.Data.Should().Contain(
                x => x.Name == (string)shopOmid.Value.Name &&
                x.Address == shopOmid.Value.Address &&
                x.IsActive == shopOmid.Value.IsActive);
        }
    }
}
