using Cooking.Infrastructure.Test;
using MyShop.Application.Services.Shops;
using MyShop.Persistence.Shops;
using MyShop.Domain.Aggregates.Shops;
using MyShop.Application.Services.Shops.Commands.Update;
using MyShop.Domain.SeedWork;
using FluentAssertions;

namespace MyShop.Application.UnitTests.Shops.Commands
{
    public class EditShopCommandHandlerTests : TestHelp<IShopRepository>
    {        
        public EditShopCommandHandlerTests()
        {
            Repository = new ShopRepository(Context);
        }

        [Fact]
        public async Task Handle_should_return_success_result()
        {
            var shop = Shop.Create("تست", "شیراز");
            Context.SaveEntity(shop.Value);
            var command = new EditShopCommand(shop.Value.Id, shop.Value.Name.Value, "تهران");
            var handler = new EditShopCommandHandler(Repository, UnitOfWork);

            Result result = await handler.Handle(command, default);

            result.IsSuccess.Should().BeTrue();
            var dbExpected = Context.Shops.Single(x => x.Id == shop.Value.Id);
            dbExpected.Name.Value.Should().Be(command.Name);
            dbExpected.Address.Should().Be(command.Address);
        }

        [Theory]
        [InlineData(-1)]
        public async Task Handle_should_return_not_found(int invalidId)
        {            
            var command = new EditShopCommand(invalidId, string.Empty, string.Empty);
            var handler = new EditShopCommandHandler(Repository, UnitOfWork);

            Result result = await handler.Handle(command, default);

            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("Shop.Edit.NotExist");
            var dbExpected = Context.Shops.SingleOrDefault(x => x.Id == invalidId);
            dbExpected.Should().BeNull();
        }

        [Theory]
        [InlineData("")]
        public async Task Handle_should_return_fail_when_name_invalid(string invalidName)
        {
            var shop = Shop.Create("تست", "شیراز");
            Context.SaveEntity(shop.Value);
            var command = new EditShopCommand(shop.Value.Id, invalidName, "تهران");
            var handler = new EditShopCommandHandler(Repository, UnitOfWork);

            Result result = await handler.Handle(command, default);

            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("Shop.Name.Empty");
            var dbExpected = Context.Shops.Single(x => x.Id == shop.Value.Id);
            dbExpected.Name.Value.Should().Be(shop.Value.Name.Value);
            dbExpected.Address.Should().Be(shop.Value.Address);
        }

        [Theory]
        [InlineData("LWM3RnaVLXYQVXEVqjIJoGsURHTICvFmVRVQeM4Jm8GSCkZbqAD2I8Lqf6jr3uIl9L1WsqtoStRilfEdqrYBpW84FEnzLkktzLlZP")]
        public async Task Handle_should_return_fail_when_name_more_than_len(string invalidName)
        {
            var shop = Shop.Create("تست", "شیراز");
            Context.SaveEntity(shop.Value);
            var command = new EditShopCommand(shop.Value.Id, invalidName, "تهران");
            var handler = new EditShopCommandHandler(Repository, UnitOfWork);

            Result result = await handler.Handle(command, default);

            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("Shop.Name.MaxLenException");
            var dbExpected = Context.Shops.Single(x => x.Id == shop.Value.Id);
            dbExpected.Name.Value.Should().Be(shop.Value.Name.Value);
            dbExpected.Address.Should().Be(shop.Value.Address);
        }
    }
}
